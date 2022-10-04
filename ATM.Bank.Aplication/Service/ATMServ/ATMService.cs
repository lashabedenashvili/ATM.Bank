using ATM.Bank.Aplication.Service.CardServ;
using ATM.Bank.Aplication.Service.LoggTimeServ;
using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Domein.Data.Domein;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Aplication.Service.ATMServ
{
    public class ATMService:IATMService
    {
        private readonly IContext _context;
        private readonly ICardService _cardService;
        private readonly ILoggTimeService _loggTime;

        public ATMService
            (
            IContext context,
            ICardService cardService,
            ILoggTimeService loggTime
            )
        {
            _context = context;
            _cardService = cardService;
            _loggTime = loggTime;
        }
        

        public async Task<ServiceResponce<string>> ChangePassword(string cardNumber, string oldPassword,string newPassword)
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
        public async Task<ServiceResponce<decimal>>LoggInATM(string cardNumber, string password)
        {
           var responce=new ServiceResponce<decimal>();
           
           var cardDb= await _cardService.CardDb(cardNumber);
            if (cardDb == null)
            {
                responce.Success = false;
                responce.Message = "Something is wrong";
                return responce;
            }

            var verifyPasswordDb= VerifyPasswordHash( password, cardDb.PasswordHash, cardDb.PassworSalt);
            
            if (!cardDb.Valid)
            {
                responce.Success=false;
                responce.Message = "The card is blocked or expired";
                return responce;
            }
            else if (!verifyPasswordDb)
            {
                responce.Success = false;
                responce.Message = "The password is not correct";
                return responce;
            }
            else
            {
                var billDb = await GetBillDb(cardNumber);
                await _loggTime.LoggIn(cardNumber);
                responce.Success=true;
                responce.Message = $"Your Balance is $ {billDb.Balance}";            
                
               


            }
            return responce;
            
        }
        private async Task<Bill>GetBillDb(string cardNumber)
        {
            var cardDb = await _cardService.CardDb(cardNumber);
            return await _context.bill.Where(x => x.CardId == cardDb.Id).FirstOrDefaultAsync();
        }
    }
}
