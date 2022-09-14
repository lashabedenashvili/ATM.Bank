using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Domein.Data.Domein;
using ATM.Bank.Infrastructure.Dto.User;
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
        public async Task<ServiceResponce<string>> Registration(User request, string password)
        {
            var response = new ServiceResponce<string>();
            if (await UserExist(request.PersonalNumber))
            {
                response.Success = false;
                response.Message = "User is already exist.";
            }
            else
            {
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passworSalt);
                request.PasswordHash = passwordHash;
                request.PasswordSalt = passworSalt;
                _context.user.Add(request);
                _context.SaveChanges();
                response.Success = true;
                response.Message = "Registration is Successful";
            }
            return response;

        }
        public async Task<ServiceResponce<string>> LogIn(UserLoginDto request)
        {
            var response = new ServiceResponce<string>();
            var user = await _context.user.FirstOrDefaultAsync(x => x.PersonalNumber == request.PersonalNumber);
            if (user == null)
            {
                response.Success = false;
                response.Message = "User does not exist";
            }
            else if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Password is not Correct";
            }
            response.Data = user.Name;
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

        public async Task<ServiceResponce<string>> UpdatePassword(UserPasswordChangeDto request)
        {
            var response = new ServiceResponce<string>();
            var user = await _context.user.FirstOrDefaultAsync(x => x.PersonalNumber == request.PersonalNumber);
            if (user == null)
            {
                response.Success = false;
                response.Message = "User does not exist.";

            }
            else if (!VerifyPasswordHash(request.OldPassword, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Password is not correct.";
            }
            else
            {
                CreatePasswordHash(request.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                _context.user.Update(user);
                _context.SaveChanges();
                response.Success = true;
                response.Message = "Password change successfuly";
            }
            return response;




        }

        public Task<ServiceResponce<List<GetUserInfoDto>>> GetUserInformation()
        {
            throw new NotImplementedException();
        }
        public async Task<ServiceResponce<int>> GetUserIdByPersonalNumber(string personalNumber)
        {
            var response = new ServiceResponce<int>();

            if (await UserExist(personalNumber) == false)
            {
                response.Success = false;
                response.Message = "This user does not exist.";
            }
            else
            {
                response.Data = await _context.user.Where(x => x.PersonalNumber == personalNumber)
                    .Select(x => x.ID).FirstOrDefaultAsync();
                response.Success=true;
            }
            return response;
        }
    }
}