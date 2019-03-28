using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOrders.DTO
{
    public class SalesOrdersDTO
    {
        public int SalesOrdersId { get; set; }
        public int RevisionNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ShipDate { get; set; }
        public int Status { get; set; }
        public string SalesOrderNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string AccountNumber { get; set; }
        public string Comment { get; set; }
        public decimal Amount { get; set; }
        public int CustomerId { get; set; }
    }
}
