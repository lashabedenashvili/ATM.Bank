using ATM.Bank.Aplication.Service;
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
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
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
    }
}
