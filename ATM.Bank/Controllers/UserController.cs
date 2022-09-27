using ATM.Bank.Aplication.Service;
using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Infrastructure.Dto;

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
        //[HttpPost("UserRegistration")]

        //public async Task<ActionResult<ServiceResponce<string>>> Registration(UserRegistrationDto request)
        //{
        //    var map = _mapper.Map<User>(request);
        //    return Ok(await _userService.Registration(map, request.Password));

        //}
        //[HttpPost("LogIn")]
        //public async Task<ActionResult<ServiceResponce<string>>> LogIn(UserLoginDto request)
        //{
        //    return Ok(await _userService.LogIn(request));
        //}
        //[HttpPost("UpdatePassword")]
        //public async Task<ActionResult<ServiceResponce<string>>> UpdatePassword(UserPasswordChangeDto request)
        //{
        //    return Ok(await _userService.UpdatePassword(request));
        //}
    }
}
