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
    }
}
