using ATM.Bank.Domein.Data.Domein;
using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Infrastructure.Dto;

namespace ATM.Bank.Aplication.Service.BillServ
{

    public class BillService : IBillService
    {
        private readonly IContext _context;
        private readonly IUserService _userService;

        public BillService(IContext context,IUserService userService)
        {
           _context = context;
           _userService = userService;
        }

        public Task<ServiceResponce<string>> AddBill(BillAddDto request)
        {
            throw new NotImplementedException();
        }
    }
}
