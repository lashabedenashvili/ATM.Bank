using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Infrastructure.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Infrastructure.AutoMapper.UserAutoMaperConfiguration
{
    public class UserAutoMapperConfiguration:Profile
    {
        public UserAutoMapperConfiguration()
        {
            CreateMap<UserRegistrationDto, User>()
                .ForMember(x => x.PasswordHash, c => c.Ignore())
                .ForMember(x => x.PasswordSalt, c => c.Ignore())
                .ForMember(x => x.ID, c => c.Ignore())
                .ForSourceMember(x => x.Password, c => c.DoNotValidate());
        }
    }
}
