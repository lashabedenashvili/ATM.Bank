using ATM.Bank.Domein.Data.Data;

namespace ATM.Bank.Aplication.Service.ATMServ
{
    public interface IATMService
    {
        Task<ServiceResponce<string>> LoggInATM(string cardNumber, string password);
        Task<ServiceResponce<string>> ChangePassword(string cardNumber, string oldPassword, string newPassword);
        Task<ServiceResponce<decimal>> WithdrawMoneyAtm(string cardNumber, decimal emountMoney);



    }
}