using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Model;

namespace DomainModel.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Discount> Discounts { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
