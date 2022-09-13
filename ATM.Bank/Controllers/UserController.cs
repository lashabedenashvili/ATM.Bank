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

        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }
       [HttpPost("UserRegistration")]

       Task<ActionResult<ServiceResponce<string>>>Registration(UserRegistrationDto request)
        {
            var map=_mapper.Map<User>(request);

        }
    }
}
