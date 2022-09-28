using ATM.Bank.Domein.Data.Domein;
using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Infrastructure.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ATM.Bank.Aplication.Service.BillServ
{

    public class BillService : IBillService
    {
        private readonly IContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public BillService(IContext context,IUserService userService,IMapper mapper)
        {
           _context = context;
           _userService = userService;
           _mapper = mapper;
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

        public async Task<ServiceResponce<User>> ChargeMoney(string billNumber,decimal emountMoney)
        {
            var responce=new ServiceResponce<User>();
            var bill = await _context.bill.Where(x => x.BillNumber == billNumber).FirstOrDefaultAsync();
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
                var transaction = new Transaction
                {
                    BillId = _bill.Id,
                    TransactionDate = DateTime.Now,
                    DebitEmount = emountMoney,

                };
                _context.transaction.Add(transaction);
                await _context.SaveChangesAsync();
                responce.Success= true;
                responce.Message = $"your balance on this {billNumber} number is ${bill.Balance}";
            }
            return responce;
        }
    }
}
