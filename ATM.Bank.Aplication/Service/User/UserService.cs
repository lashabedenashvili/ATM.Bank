using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Domein.Data.Domein;
using Microsoft.EntityFrameworkCore;

namespace ATM.Bank.Aplication.Service
{
    public class UserService : IUserService
    {
        private readonly IContext _context;

        public UserService(IContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponce<string>> Registration(User request,string password)
        {
            var response=new ServiceResponce<string>();
            if (await UserExist(request.PersonalNumber)==false)
            {
                response.Success = false;
                response.Message = "User is already exist.";
            }
            else
            {
                CreatePasswordHash(password,out byte[]passwordHash, out byte[] passworSalt);
                _context.user.Add(request);
                _context.SaveChanges();
                response.Success = true;
                response.Message = "Registration is Successful";
            }
            return response;

        }
        private async Task<bool> UserExist(string personalNumber)
        {
           
            return await _context.user.AnyAsync(x => x.PersonalNumber == personalNumber);
            
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