using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Bank.Aplication.Service
{
    public class ServiceResponce<T>
    {
       
        public T Data { get; set; } 
        public string Message { get; set; }
        public bool Success { get; set; }
    }


    



}
