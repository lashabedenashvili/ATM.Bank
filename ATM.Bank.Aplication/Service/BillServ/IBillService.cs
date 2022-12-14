using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Aplication.Service.BillServ
{
    public interface IBillService
    {
        Task<ServiceResponce<string>> AddBill(BillAddDto request);
        Task<ServiceResponce<User>> ChargeMoney(string billNumber,decimal emountMoney);
        Task<ServiceResponce<decimal>> WithdrawMoney(string cardNumber, decimal emountMoney);

    }
}
