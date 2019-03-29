using CustomerOrders.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrders.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var md5 = new MD5CryptoServiceProvider();
            byte[] md5data = md5.ComputeHash(Encoding.UTF8.GetBytes("mypassword"));
            string hashPW;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md5data.Length; i++)
            {
                sb.Append(md5data[i].ToString("X2"));
            }

            hashPW = sb.ToString();

            Guid adminID = Guid.NewGuid();

            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser { Id = adminID.ToString(), UserName = "Admin User", NormalizedUserName = "ADMIN", Email = "khurram.rafi@tenpearls.com", NormalizedEmail = "KHURRAM.RAFI@TENPEARLS.COM", EmailConfirmed = false, PasswordHash = hashPW, PhoneNumber = "0092211234567", LockoutEnabled = false });
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<SalesOrders> SalesOrders { get; set; }
    }
}
