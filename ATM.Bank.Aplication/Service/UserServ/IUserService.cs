using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Infrastructure.Dto.UserRegistration;
using ATM.Bank.Infrastructure.Dto.UserRegistration.UserUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ATM.Bank.Aplication.Service
{
    public interface IUserService
    {
      
       Task<ServiceResponce<string>> UserRegistratiion(UserRegistrationDto request);
       Task<ServiceResponce<string>> UserDelete(int userId);
       Task<ServiceResponce<string>> UserUpdate(UserUpdateDto request);
    }
}
