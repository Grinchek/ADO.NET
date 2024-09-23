using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _fluent_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace _fluent_api
{
    public class ShopDbContext:DbContext
    {
        public DbSet<Worker> Workers { get; set; }
        public ShopDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-A1447MR\SQLEXPRESS;
                                          Initial Catalog=Shop;
                                          Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Worker>()
                .Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }

}
