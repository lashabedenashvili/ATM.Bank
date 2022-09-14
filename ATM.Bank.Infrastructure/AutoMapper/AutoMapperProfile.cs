using ATM.Bank.Infrastructure.AutoMapper.UserAutoMaperConfiguration;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Infrastructure.AutoMapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            new UserAutoMapperConfiguration();
        }
    }
}
