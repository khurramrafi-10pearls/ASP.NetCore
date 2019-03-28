using System.Collections.Generic;

namespace CustomerOrders.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string AccountNumber { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public IList<SalesOrders> Orders { get; set; }

        public Customer()
        {
            this.Orders = new List<SalesOrders>();
        }
    }
}
