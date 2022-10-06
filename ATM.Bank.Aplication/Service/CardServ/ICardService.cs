using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Infrastructure.Dto;

namespace ATM.Bank.Aplication.Service.CardServ
{
    public interface ICardService
    {
        Task<ServiceResponce<string>> AddCard(AddCardDto request);
        Task<ServiceResponce<string>> AttachedExistingCardToBillNumber(string cardNumber, string billNumber);
        Task<ServiceResponce<string>> BlockCard(string cardNumber);
        Task<ServiceResponce<string>> UnBlockCard(string cardNumber);
        Task<ServiceResponce<string>> CardDataExpiryCheck(string cardNuber);
        Task<Card> CardDb(string cardNumber);
        Task<ServiceResponce<string>> ChangeCardPassword(string cardNumber, string oldPassword, string newPassword);
        Task<int> PasswordTryCount(string cardNumber);
        Task PasswordTryCountReset(string cardNumber);


    }
}