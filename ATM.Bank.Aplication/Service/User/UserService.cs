using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Domein.Data.Domein;
using ATM.Bank.Infrastructure.Dto.UserRegistration;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ATM.Bank.Aplication.Service
{
    public class UserService : IUserService
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public UserService(IContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<bool>UserExistDb(string documentNumber)
        {            
            return await _context.privateInformation.AnyAsync(x=>x.DocumentNumber == documentNumber);
        }

        public async Task<ServiceResponce<string>> UserRegistratiion(UserRegistrationDto request)
        {
            var responce=new ServiceResponce<string>();
            if ( await UserExistDb(request.PrivateInformationDto.DocumentNumber))
            {
                responce.Success = false;
                responce.Message = "User is alredy exist";
                return responce;
            }
            else
            {
                var addedUser=_mapper.Map<>
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        

      
        
    }
}