using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Domein.Data.Domein;
using ATM.Bank.Infrastructure.Dto.UserRegistration;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Aplication.AddressServ
{
    public class AddressService : IAddressService
    {
        private readonly IMapper _mapper;
        private readonly IContext _context;

        public AddressService(IMapper mapper,IContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task AddAddress(AddressDto request, User userId)
        {
            var addAddress=_mapper.Map<Address>(request);
            addAddress.User = userId;
            await _context.address.AddAsync(addAddress);
        }

        public async Task UpdateAddress(AddressDto request, User userId)
        {
            var _address= await _context.address.Where(x=>x.UserId ==userId.ID).FirstOrDefaultAsync();
            var address=  _mapper.Map<AddressDto,Address>(request, _address);
            _context.address.Update(address);
            await _context.SaveChangesAsync();
        }
    }
}
