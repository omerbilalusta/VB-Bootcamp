using Microsoft.EntityFrameworkCore;
using Vb_Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_Data.Domain.User;
using System.ComponentModel.DataAnnotations;

namespace Vb_Data.Context
{
    public class VbDbContext : DbContext
    {
        public VbDbContext(DbContextOptions<VbDbContext> options) : base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new DealerConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceDetailConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderRejectConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());

            modelBuilder.Entity<Company>()
                .HasData(
                    new Company() { Id = 1, Name = "Test", Address = "Room 907", TaxNumber =  817553976, Email = "testCompany@mail.com", Password = "2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824" }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
