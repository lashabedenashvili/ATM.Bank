using ATM.Bank.Aplication.Service;
using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Domein.Data.Domein;
using ATM.Bank.Infrastructure.Dto.UserRegistration;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Aplication.ContactInformationServ
{
    public class ContactInformationService : IContactInformationService
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public ContactInformationService(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddContactInformation(ContactInformationDto request, User userId)
        {         
            var contactInfromation=_mapper.Map<ContactInformation>(request);
            contactInfromation.User=userId; 
            await _context.contactInformation.AddAsync(contactInfromation);
        }
    }
}
