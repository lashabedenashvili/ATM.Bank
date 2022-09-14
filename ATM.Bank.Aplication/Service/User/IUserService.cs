using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Infrastructure.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ATM.Bank.Aplication.Service
{
    public interface IUserService
    {
        Task<ServiceResponce<string>> Registration(User request, string password);
        Task<ServiceResponce<string>> LogIn(UserLoginDto request);
        Task<ServiceResponce<string>> UpdatePassword(UserPasswordChangeDto request);
        Task<ServiceResponce<List<GetUserInfoDto>>> GetUserInformation();
        Task<ServiceResponce<int>> GetUserIdByPersonalNumber(string personalNumber);
    }
}
