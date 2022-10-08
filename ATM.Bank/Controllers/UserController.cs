using ATM.Bank.Aplication.Service;
using ATM.Bank.Aplication.Service.BillServ;
using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Infrastructure.Dto;
using ATM.Bank.Infrastructure.Dto.UserRegistration;
using ATM.Bank.Infrastructure.Dto.UserRegistration.UserUpdate;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Bank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
       
        private readonly IUserService _userService;
        private readonly IBillService _billService;

        public UserController(IUserService userService,IBillService billService)
        {
           
            _userService = userService;
            _billService = billService;
        }
        [HttpPost("UserRegistration")]
        public async Task<ActionResult<ServiceResponce<string>>> UserRegistratiion(UserRegistrationDto request)
        {
            return Ok(await _userService.UserRegistratiion(request));
        }

        [HttpDelete("UserDeleteById")]
        public async Task<ActionResult<ServiceResponce<string>>> UserDeleteById(int userId)
        {
            return Ok(await _userService.UserDelete(userId));
        }

        [HttpPost("UserUpdate")]
        public async Task<ActionResult<ServiceResponce<string>>> UpdateUser(UserUpdateDto request)
        {
            return Ok(await _userService.UserUpdate(request));
        }

        [HttpPost("AddBillNumber")]
        public async Task<ActionResult<ServiceResponce<string>>> AddBillNumber(BillAddDto request)
        {
            return Ok(await _billService.AddBill(request));
        }

        [HttpPost("ChargeMoney")]
        public async Task<ActionResult<ServiceResponce<User>>> ChargeMoney(string billNumber, decimal emountMoney)
        {
            return Ok(await _billService.ChargeMoney(billNumber, emountMoney));
        }

        
    }
}
