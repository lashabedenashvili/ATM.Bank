using ATM.Bank.Domein.Data.Data;
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
    }
}
