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
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
        private async Task<Bill> BillDb(string billNumber)
        {
            return await _context.bill.Where(x => x.BillNumber == billNumber).FirstOrDefaultAsync();
        }

        private async Task<Card>CardDb(string cardNumber)
        {
            return await _context.card.Where(x=>x.CardNumber== cardNumber).FirstOrDefaultAsync();   
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
    }
}
