using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Infrastructure.Dto;
using ATM.Bank.Infrastructure.Dto.UserRegistration;
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
            CreateMap<UserRegistrationDto,User>();
            CreateMap<UserRegistrationDto, ContactInformation>();
            CreateMap<UserRegistrationDto, PrivateInformation>();
            CreateMap<UserRegistrationDto, Address>();
            
        }
    }
}
