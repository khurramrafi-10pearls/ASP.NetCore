using CustomerOrders.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOrders.CustomServices
{
    public interface IAccountService
    {
        Task<string> Login(LoginDTO form);
    }
}
