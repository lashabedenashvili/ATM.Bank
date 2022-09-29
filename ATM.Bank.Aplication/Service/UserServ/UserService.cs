using ATM.Bank.Aplication.AddressServ;
using ATM.Bank.Aplication.ContactInformationServ;
using ATM.Bank.Aplication.PrivateInformationServ;
using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Domein.Data.Domein;
using ATM.Bank.Infrastructure.Dto.UserRegistration;
using ATM.Bank.Infrastructure.Dto.UserRegistration.UserUpdate;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ATM.Bank.Aplication.Service
{
    public class UserService : IUserService
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;
        private readonly IAddressService _addressService;
        private readonly IContactInformationService _contactInformationService;
        private readonly IPrivateInformationService _privateInformationService;

        public UserService
            (
            IContext context,
            IMapper mapper,
            IAddressService addressService,
            IContactInformationService contactInformationService,
            IPrivateInformationService privateInformationService
            )
        {
            _context = context;
            _mapper = mapper;
            _addressService = addressService;
            _contactInformationService = contactInformationService;
            _privateInformationService = privateInformationService;
        }
        private async Task<User> GetUser(int userId)
        {
            var responce=new ServiceResponce<User>();
            responce.Data= await _context.user.FirstOrDefaultAsync(x=>x.ID==userId);
            return responce.Data;
        }

        private async Task<bool> UserExistDb(string privateNumber)
        {
            return await _context.privateInformation.AnyAsync(x => x.PrivateNumber == privateNumber);
        }

        public async Task<ServiceResponce<string>> UserRegistratiion(UserRegistrationDto request)
        {
            var responce = new ServiceResponce<string>();
            var addedUser = _mapper.Map<User>(request.UserDto);
            _context.user.Add(addedUser);
            await _addressService.AddAddress(request.AddressDto, addedUser);
            await _contactInformationService.AddContactInformation(request.ContactInformationDto, addedUser);
            await _privateInformationService.AddPrivateInformation(request.PrivateInformationDto, addedUser);


            if (await UserExistDb(request.PrivateInformationDto.PrivateNumber))
            {
                responce.Success = false;
                responce.Message = "User is alredy exist";
                return responce;
            }
            responce.Success = true;
            responce.Message = "User is register";
            await _context.SaveChangesAsync();

            return responce;
        }

     

        public async Task<ServiceResponce<string>> UserDelete(int userId)
        {
            var responce = new ServiceResponce<string>();
            var userDb = await _context.user.Where(x => x.ID == userId).FirstOrDefaultAsync();
            if (userDb == null)
            {
                responce.Success = false;
                responce.Message = "User does not exist";
                return responce;
            }
            _context.user.Remove(userDb);
            await _context.SaveChangesAsync();
            responce.Success = true;
            responce.Message = $"User Id={userId} is already deleted";

            return responce;
        }

        public async Task<ServiceResponce<string>> UserUpdate(UserUpdateDto request)
        {   var responce=new ServiceResponce<string>();
            var _user = await GetUser(request.User.Id);

            if (_user != null)
            {
                var userId = _mapper.Map<UpdateUserDto,User>(request.User,_user);
                _context.user.Update(userId);
                await _addressService.UpdateAddress(request.Address, userId);
                await _contactInformationService.UpdateContactInformation(request.ContactInformation, userId);
                await _privateInformationService.UpdatePrivateInformation(request.PrivateInformation, userId);
                responce.Success=true;
                responce.Message = "User is already updated";               
                await _context.SaveChangesAsync();

            }
            else
            {
                responce.Success=false;
                responce.Message = $"User with this Id{request.User.Id} does not exist";
            }
            return responce;
           
        }
    }
}