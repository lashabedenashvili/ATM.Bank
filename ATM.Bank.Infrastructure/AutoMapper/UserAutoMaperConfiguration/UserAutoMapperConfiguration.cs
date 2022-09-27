using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Infrastructure.Dto;
using ATM.Bank.Infrastructure.Dto.UserRegistration;
using ATM.Bank.Infrastructure.Dto.UserRegistration.UserUpdate;
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
            CreateMap<UserDto,User>();
            CreateMap<ContactInformationDto, ContactInformation>();
            CreateMap<PrivateInformationDto, PrivateInformation>();
            CreateMap<AddressDto, Address>();
            

            CreateMap<UpdateUserDto,User >()
            .ForAllMembers(c => c.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ContactInformationDto,ContactInformation >()
                .ForAllMembers(c => c.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PrivateInformationDto,PrivateInformation >()
                .ForAllMembers(c => c.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AddressDto,Address>()
                .ForAllMembers(c => c.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
