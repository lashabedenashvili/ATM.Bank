using ATM.Bank.Aplication.Service.CardServ;
using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Domein.Data.Domein;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Aplication.Service.LoggTimeServ
{
    public class LoggTimeService : ILoggTimeService
    {
        private readonly IContext _context;
        private readonly ICardService _cardService;

        public LoggTimeService(IContext context, ICardService cardService)
        {
            _context = context;
            _cardService = cardService;
        }
        private async Task<LoggTime> LoggTimeDb(string cardNumber)
        {
            var cardId = await _cardService.CardDb(cardNumber);
            return await _context.LoggTime.Where(x => x.CardId == cardId.Id).FirstOrDefaultAsync();
        }
        public async Task LoggIn(string cardNumber)
        {
            var cardId = await _cardService.CardDb(cardNumber);
            var loggIn = new LoggTime
            {
                CardId = cardId.Id,
                LogIn = DateTime.Now,
                LogOut = null,
            };
            _context.LoggTime.Add(loggIn);
            await _context.SaveChangesAsync();
        }

        public async Task LoggOut(string cardNumber)
        {
            var loggTimeDb=await LoggTimeDb(cardNumber);
            loggTimeDb.LogOut=DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
