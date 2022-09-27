using ATM.Bank.Aplication.Service;
using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Domein.Data.Domein;
using ATM.Bank.Infrastructure.Dto.UserRegistration;
using AutoMapper;

namespace ATM.Bank.Aplication.ContactInformationServ
{
    public interface IContactInformationService
    {

        Task AddContactInformation(ContactInformationDto request, User userId);
        Task UpdateContactInformation(ContactInformationDto request, User userId);

    }
}