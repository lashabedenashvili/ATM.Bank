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
        public async Task<ServiceResponce<string>> AddBill(BillAddDto request)
        {
            var response=new ServiceResponce<string>();
            var userId = await _userService.GetUserIdByPersonalNumber(request.personalNumber);
            if (userId.Success==false)
            {
                response.Message= userId.Message;
            }
            else
            {
                var addbillz = new Bill
                {

                    BillNumber = request.BillNumber,
                    Balance = request.Balance,
                    UserId = userId.Data,
                };
                _context.SaveChanges();
                response.Success = true;
            


            }
            return response;
        }
    }
}
