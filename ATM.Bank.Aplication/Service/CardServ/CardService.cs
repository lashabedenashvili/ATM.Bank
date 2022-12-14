using ATM.Bank.Aplication.Service.ATMServ;
using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Domein.Data.Domein;
using ATM.Bank.Infrastructure.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Aplication.Service.CardServ
{
    public class CardService : ICardService
    {
        private readonly IMapper _mapper;
        private readonly IContext _context;
        

        public CardService(IMapper mapper,IContext context)
        {
            _mapper = mapper;
            _context = context;
           
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        public async Task<ServiceResponce<string>>ChangeCardPassword(string cardNumber,string oldPassword,string newPassword)
        {
            var responce=new ServiceResponce<string>();
            var cardDb = await CardDb(cardNumber);
            if (cardDb == null)
            {
                responce.Success=false;
                responce.Message = "The card number is not correct";
                return responce;
            }

            var verifuPasswordHash = VerifyPasswordHash(oldPassword, cardDb.PasswordHash, cardDb.PassworSalt);
            if (!verifuPasswordHash)
            {
                responce.Success = false;
                responce.Message = "The old password is not correct";
                return responce;
            }
            else
            {
                CreatePasswordHash(newPassword, out byte[] PasswordHash, out byte[] PassworSalt);
                cardDb.PasswordHash = PasswordHash;
                cardDb.PassworSalt = PassworSalt;
                await _context.SaveChangesAsync();
                responce.Success=true;
                responce.Message = "The password is updated";
            }
            return responce;
        }
     
        private async Task<Bill> BillDb(string billNumber)
        {
            return await _context.bill.Where(x => x.BillNumber == billNumber).FirstOrDefaultAsync();
        }    
        public async Task<Card>CardDb(string cardNumber)
        {
            return await _context.card.Where(x=>x.CardNumber== cardNumber).FirstOrDefaultAsync();   
        }
        public async Task<BlockCard> BlockCardLastRecord(int cardId)
        {
           
                var ll=await _context.blockCard
                .Where(m => m.CardId == cardId)
                .OrderByDescending(m => m.BlockTime)
                .LastOrDefaultAsync();

            return ll;
        }

        private async Task<BlockCard> BlockCardDb(int cardId)
        {
            return await _context.blockCard.Where(x=>x.CardId==cardId).FirstOrDefaultAsync();
        }
        public async Task<ServiceResponce<string>> AddCard(AddCardDto request)
        {
            var responce=new ServiceResponce<string>();
            var billDb= await BillDb(request.BillNumber);

            if (billDb == null)
            {
                responce.Success = false;
                responce.Message = "Card number already exist";
                return responce;
            }
            else
            {
                var card = _mapper.Map<Card>(request);
                card.Valid = true;
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                card.PasswordHash = passwordHash;
                card.PassworSalt = passwordSalt;                        
                await _context.card.AddAsync(card);
                billDb.Card = card;
                await _context.SaveChangesAsync();
                responce.Success = true;
                responce.Message = "Card is added";

            }
          
            return responce;
        }

        public async Task<ServiceResponce<string>> AttachedExistingCardToBillNumber(string cardNumber, string billNumber)
        {
            var responce = new ServiceResponce<string>();
            var cardDb = await CardDb(cardNumber);
            var billDb=await BillDb(billNumber);
            if(billDb == null)
            {
                responce.Success = false;
                responce.Message = "bill number does not exist";
                return responce;
            }
            else if ( billDb.CardId != null)
            {
                responce.Success = false;
                responce.Message = "another Card is alredy attached on this bill";
                return responce;
            }
            else if (cardDb == null)
            {
                responce.Success=false;
                responce.Message = "This Card does not exist";
                return responce;
            }
            else
            {
                billDb.CardId = cardDb.Id;
                await _context.SaveChangesAsync();
                responce.Success=true;
                responce.Message = "card is attached on the bill";
            }
            return responce;
        }

        public async Task<ServiceResponce<string>> BlockCard(string cardNumber)
        {
            var responce=new ServiceResponce<string>();
            var cardDb = await CardDb(cardNumber);           
            var cardDbDescending = await BlockCardLastRecord(cardDb.Id);
            if (cardDb == null)
            {
                responce.Success = false;
                responce.Message = "The card does not exist";
                return responce;
            }
            else if (cardDbDescending.BlockTime == null)
            {
                
                    var inserBlockCard = new BlockCard
                    {
                        CardId = cardDb.Id,
                        BlockTime = DateTime.Now,
                        UnBlockTime = null,
                    };
                    cardDb.Valid = false;
                    _context.blockCard.Add(inserBlockCard);
                    await _context.SaveChangesAsync();
                    responce.Success = true;
                    responce.Message = "The card is Blocked";
                
            }
           
            else
            {
                responce.Success = false;
                responce.Message = "The card is alredy blocked";
                return responce;
            }
           
            return responce;
        }

        public async Task<ServiceResponce<string>> UnBlockCard(string cardNumber)
        {
            var responce = new ServiceResponce<string>();
            var cardDb = await CardDb(cardNumber);
            var blockCardDb = await BlockCardDb(cardDb.Id);
            if (cardDb == null)
            {
                responce.Success = false;
                responce.Message = "The card does not exist";
                return responce;
            }
            else if (blockCardDb == null)
            {
                responce.Success = false;
                responce.Message = "Card Is not Blocked";
                return responce;
            }
            else
            {
                var insertCardBlock = new BlockCard
                {
                    CardId = cardDb.Id,
                    UnBlockTime = DateTime.Now,
                    BlockTime = null,
                    
                };
                _context.blockCard.Add(insertCardBlock);
                cardDb.Valid = true;
                cardDb.PasswordTryCount = 0;
                await _context.SaveChangesAsync();
                responce.Success = true;
                responce.Message = "The card is unblock";

            }
            return responce;
        }

        public async Task<ServiceResponce<string>> CardDataExpiryCheck(string cardNuber)
        {
            var responce=new ServiceResponce<string>(); 
            var cardDb= await CardDb(cardNuber);
            if (cardDb == null)
            {
                responce.Success = false;
                responce.Message = "The card does not exist";
                return responce;
            }
            else if (cardDb.DateExpiry <= DateTime.Now)
            {
                cardDb.Valid=false;
                responce.Message = "The card has expired";
                responce.Success = false;
                await _context.SaveChangesAsync();
            }
            return responce;
        }
        public async Task<int> PasswordTryCount(string cardNumber)
        {        
            
            var tryCount = await CardDb(cardNumber);
            tryCount.PasswordTryCount += 1;
            await _context.SaveChangesAsync();
            return  tryCount.PasswordTryCount;
            
        }
        public async Task PasswordTryCountReset(string cardNumber)
        {
            var tryCountDb = await CardDb(cardNumber);
            tryCountDb.PasswordTryCount = 0;
            await _context.SaveChangesAsync();
        }
    }
}
