using Dapper.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.DAL.Conxtext
{
    public class ProductContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=CYBER-HACKER\\SQLEXPRESS;Initial Catalog=UnitWorkDb;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Product>? Products { get; set; }
        public DbSet<Category>? Categories { get; set; }
    }
}
