using ATM.Bank.Aplication.Service.BillServ;
using ATM.Bank.Aplication.Service.CardServ;
using ATM.Bank.Aplication.Service.LoggTimeServ;
using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Domein.Data.Domein;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Aplication.Service.ATMServ
{
    public class ATMService : IATMService
    {
        private readonly IContext _context;
        private readonly ICardService _cardService;
        private readonly ILoggTimeService _loggTime;
        private readonly IBillService _billService;
        private readonly IConfiguration _configuration;

        public ATMService
            (
            IContext context,
            ICardService cardService,
            ILoggTimeService loggTime,
            IBillService billService,
            IConfiguration configuration
            )
        {
            _context = context;
            _cardService = cardService;
            _loggTime = loggTime;
            _billService = billService;
            _configuration = configuration;
        }


        public async Task<ServiceResponce<string>> ChangePassword(string cardNumber, string oldPassword, string newPassword)
        {
            return await _cardService.ChangeCardPassword(cardNumber, oldPassword, newPassword);

        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
        public async Task<ServiceResponce<string>> LoggInATM(string cardNumber, string password)
        {
            var responce = new ServiceResponce<string>();
            

            var cardDb = await _cardService.CardDb(cardNumber);
            if (cardDb == null)
            {
                responce.Success = false;
                responce.Message = "Something is wrong";
                return responce;
            }


            var verifyPasswordDb = VerifyPasswordHash(password, cardDb.PasswordHash, cardDb.PassworSalt);

            if (!cardDb.Valid)
            {
                responce.Success = false;
                responce.Message = "The card is blocked or expired";
                return responce;
            }

            else if (!verifyPasswordDb)
            {
                responce.Success = false;
                responce.Message = "The password is not correct";
                var passwordCount = await _cardService.PasswordTryCount(cardNumber);
                if (passwordCount > 2)
                {
                    var blockCard = await _cardService.BlockCard(cardNumber);
                    responce.Message = blockCard.Message;
                    return responce;
                }
            }
            else
            {
                var billDb = await GetBillDb(cardNumber);
                await _loggTime.LoggIn(cardNumber);
                responce.Success = true;
                responce.Message = $"Your Balance is $ {billDb.Balance}";
                await _cardService.PasswordTryCountReset(cardNumber);                
                responce.Data = CreateToken(cardDb);
            }
            return responce;

        }
        private async Task<Bill> GetBillDb(string cardNumber)
        {
            var cardDb = await _cardService.CardDb(cardNumber);
            return await _context.bill.Where(x => x.CardId == cardDb.Id).FirstOrDefaultAsync();
        }

        public async Task<ServiceResponce<decimal>> WithdrawMoneyAtm(string cardNumber, decimal emountMoney)
        {
            return await _billService.WithdrawMoney(cardNumber, emountMoney);
        }
        private string CreateToken(Card card)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,card.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier,card.CardNumber.ToString())
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(1),
                SigningCredentials = cred
            };
            JwtSecurityTokenHandler tokenHendler=new JwtSecurityTokenHandler();
            SecurityToken token = tokenHendler.CreateToken(tokenDescriptor);

            return tokenHendler.WriteToken(token);
        } 
    }
}
