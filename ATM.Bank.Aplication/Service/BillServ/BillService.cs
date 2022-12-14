using ATM.Bank.Domein.Data.Domein;
using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Infrastructure.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ATM.Bank.Aplication.Service.CardServ;

namespace ATM.Bank.Aplication.Service.BillServ
{

    public class BillService : IBillService
    {
        private readonly IContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ICardService _cardService;

        public BillService(IContext context,IUserService userService,IMapper mapper,ICardService cardService)
        {
           _context = context;
           _userService = userService;
           _mapper = mapper;
           _cardService = cardService;
        }

        private async Task<bool>BillNumberExist(string billNumber)
        {
            return await _context.bill.AnyAsync(x=>x.BillNumber==billNumber); 
        }
        public async Task<ServiceResponce<string>> AddBill(BillAddDto request)
        {
            var responce = new ServiceResponce<string>();
            if(await BillNumberExist(request.BillNumber))
            {
                responce.Success = false;
                responce.Message = "bill number alredy exist";
                return responce;
            }
           var bill=_mapper.Map<Bill>(request);
            _context.bill.Add(bill);
            await  _context.SaveChangesAsync();
            responce.Success=true;
            responce.Message = $" bill number{request.BillNumber} added";
            return responce;
        }
        private async Task<Bill> GetBill(string billNumber)
        {
            return await _context.bill.Where(x => x.BillNumber == billNumber).FirstOrDefaultAsync();
            
        }
        private async Task<Bill>GetBillByCardId(int cardId)
        {
            return await _context.bill.Where(x => x.CardId == cardId).FirstOrDefaultAsync();
        }
        private async Task TransactionDb(int billId,decimal creditEmount,decimal debitEmount)
        {
            var transaction = new Transaction
            {
                BillId = billId,
                CreditEmount = creditEmount,
                DebitEmount = debitEmount,
                TransactionDate = DateTime.Now,
            };
           await _context.transaction.AddAsync(transaction);
        }
        public async Task<ServiceResponce<decimal>> WithdrawMoney(string cardNumber, decimal emountMoney)
        {
            var responce=new ServiceResponce<decimal>();
            var cardDb=await _cardService.CardDb(cardNumber);
            var billDb = await GetBillByCardId(cardDb.Id);
            if (cardDb == null)
            {
                responce.Success = false;
                responce.Message = "The card does not exist";
                return responce;
            }
            else if (!cardDb.Valid)
            {
                responce.Success = false;
                responce.Message = "The card is blocked";
                return responce;
            }
            else if (emountMoney> billDb.Balance)
            {
                responce.Success = false;
                responce.Message = $"You have not enought balance. your balance is ${billDb.Balance}";
                return responce;
            }
            else
            {
                billDb.Balance -= emountMoney;
                responce.Success = true;
                await TransactionDb(billDb.Id, emountMoney,0);
                await _context.SaveChangesAsync();
            }

            return responce;
        }

        public async Task<ServiceResponce<User>> ChargeMoney(string billNumber,decimal emountMoney)
        {
            var responce=new ServiceResponce<User>();
            var bill = await GetBill(billNumber);
            if (bill == null)
            {
                responce.Success=false;
                responce.Message = "bill number is not correct";
                return responce;
            }
            else
            {            
                bill.Balance += emountMoney;
                _context.bill.Update(bill);
                var _bill=await GetBill(billNumber);
                await TransactionDb(_bill.Id, (decimal)0, emountMoney);
                await _context.SaveChangesAsync();
                responce.Success= true;
                responce.Message = $"your balance on this {billNumber} number is ${bill.Balance}";
            }
            return responce;
        }
    }
}
