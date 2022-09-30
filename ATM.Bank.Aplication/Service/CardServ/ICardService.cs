using ATM.Bank.Infrastructure.Dto;

namespace ATM.Bank.Aplication.Service.CardServ
{
    public interface ICardService
    {
        Task<ServiceResponce<string>> AddCard(AddCardDto request);
        Task<ServiceResponce<string>> AttachedExistingCardToBillNumber(string cardNumber, string billNumber);
    }
}