using ATM.Bank.Aplication.Service;
using ATM.Bank.Aplication.Service.ATMServ;
using ATM.Bank.Domein.Data.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ATM.Bank.Controllers
{
    [Authorize]   
    [ApiController]
    [Route("api/[controller]")]

    public class ATMController: ControllerBase
    {
        private readonly IATMService _atmService;

        public ATMController(IATMService atmService)
        {
            _atmService = atmService;
        }
        [AllowAnonymous]
        [HttpPost("LoggInATM")]
        public async Task<ActionResult<ServiceResponce<string>>> LoggInATM(string cardNumber, string password)
            => Ok(await _atmService.LoggInATM(cardNumber, password));
      
        [HttpPost("ChangePassword")]
        public async Task<ActionResult<ServiceResponce<string>>> ChangePassword(string cardNumber, string oldPassword, string newPassword)
        {
            return Ok(await _atmService.ChangePassword(cardNumber, oldPassword, newPassword));
        }

        [HttpPost("WithdrawMoneyAtm")]
        public async Task<ActionResult<ServiceResponce<decimal>>> WithdrawMoneyAtm( decimal emountMoney)
        {
            string cardNumber = User.Claims.FirstOrDefault(x => x.Type == "CardNumber").Value;
            return Ok(await _atmService.WithdrawMoneyAtm(cardNumber, emountMoney));
        }
    }
}
