using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOrders.CustomServices
{
    public class OrderService : IOrderService
    {
        public OrderService()
        {
        }

        public string GetLead()
        {
            return "Lead Captured";
        }
    }
}
