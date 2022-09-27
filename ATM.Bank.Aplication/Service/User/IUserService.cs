﻿using ATM.Bank.Domein.Data.Data;
using ATM.Bank.Infrastructure.Dto.UserRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ATM.Bank.Aplication.Service
{
    public interface IUserService
    {
      
       Task<ServiceResponce<string>> UserRegistratiion(UserRegistrationDto request);
    }
}
