using CustomerOrders.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOrders.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //this.Database.Migrate();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<SalesOrders> SalesOrders { get; set; }
    }
}
