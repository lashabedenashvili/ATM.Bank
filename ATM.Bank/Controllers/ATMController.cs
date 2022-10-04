using ATM.Bank.Aplication.Service;
using ATM.Bank.Aplication.Service.ATMServ;
using ATM.Bank.Domein.Data.Data;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Bank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ATMController: ControllerBase
    {
        private readonly IATMService _atmService;

        public ATMController(IATMService atmService)
        {
            _atmService = atmService;
        }

        [HttpPost("LoggInATM")]
        public async Task<ActionResult<ServiceResponce<decimal>>> LoggInATM(string cardNumber, string password)
        {
            return Ok(await _atmService.LoggInATM(cardNumber, password));
        }
        [HttpPost("ChangePassword")]
        public async Task<ActionResult<ServiceResponce<string>>> ChangePassword(string cardNumber, string oldPassword, string newPassword)
        {
            return Ok(await _atmService.ChangePassword(cardNumber, oldPassword, newPassword));
        }

        [HttpPost("WithdrawMoneyAtm")]
        public async Task<ActionResult<ServiceResponce<decimal>>> WithdrawMoneyAtm(string cardNumber, decimal emountMoney)
        {
            return Ok(await _atmService.WithdrawMoneyAtm(cardNumber, emountMoney));
        }
    }
}
