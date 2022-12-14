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

namespace ATM.Bank.Aplication.PrivateInformationServ
{
    public class PrivateInformationService : IPrivateInformationService
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public PrivateInformationService(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddPrivateInformation(PrivateInformationDto request, User userId)
        {
            var addPrivateInformation = _mapper.Map<PrivateInformation>(request);
            addPrivateInformation.User = userId;
            await _context.privateInformation.AddAsync(addPrivateInformation);
        }

        public async Task UpdatePrivateInformation(PrivateInformationDto request, User userId)
        {
            var _privateInformation = await  _context.privateInformation.Where(x => x.UserId == userId.ID).FirstOrDefaultAsync();
            var privateInformation = _mapper.Map<PrivateInformationDto,PrivateInformation>(request,_privateInformation);
            privateInformation.User = userId;
            _context.privateInformation.Update(privateInformation);
            await _context.SaveChangesAsync();
        }
    }
}
