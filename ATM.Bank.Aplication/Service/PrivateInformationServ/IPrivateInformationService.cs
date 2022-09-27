using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Infrastructure.Dto.UserRegistration;

namespace ATM.Bank.Aplication.PrivateInformationServ
{
    public interface IPrivateInformationService
    {
        Task AddPrivateInformation(PrivateInformationDto request, User userId);
    }
}