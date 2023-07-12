using BusinessLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CustomerDbContext:DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Server = SHRIMAY\\SQLEXPRESS; Database = Orderlytics_Customers; Trusted_Connection = True; TrustServerCertificate = True";

                // Set the connection timeout to 60 seconds

                optionsBuilder.UseSqlServer(connectionString, options => options.CommandTimeout(60));
            }
        }
    }
}
