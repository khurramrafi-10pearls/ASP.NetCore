using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOrders.DTO
{
    public class CustomerDTO
    {
        //public int CustomerId { get; set; }
        //public string AccountNumber { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public IList<SalesOrdersDTO> Orders { get; set; }

        public CustomerDTO()
        {
            this.Orders = new List<SalesOrdersDTO>();
        }
    }
}
