using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Infrastructure.Dto.UserRegistration;

namespace ATM.Bank.Aplication.AddressServ
{
    public interface IAddressService
    {
        Task AddAddress(AddressDto request, User userId);
    }
}