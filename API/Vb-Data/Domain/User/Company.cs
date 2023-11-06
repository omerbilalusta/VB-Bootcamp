using Vb_Base.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vb_Data.Domain.User
{
    public class Company : UserModel    //Admin
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int TaxNumber { get; set; }
        public string IBAN { get; set; }

        public virtual List<Product> Products { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<Chat> Chats { get; set; }
    }

    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(x => x.InsertUserId).IsRequired();
            builder.Property(x => x.UpdateUserId).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(100);
            builder.Property(x => x.TaxNumber).IsRequired().HasMaxLength(20);
            builder.Property(x => x.IBAN).IsRequired().HasMaxLength(26).HasDefaultValue("TR000000000000000000000000");
            builder.Property(x => x.Role).IsRequired().HasMaxLength(10).HasDefaultValue("admin");
            builder.Property(x => x.Password).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);

            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasMany(x => x.Chats)
                .WithOne(x => x.Company)
                .HasForeignKey(x => x.CompanyId)
                .IsRequired(false);

            builder.HasMany(x => x.Products)
                .WithOne(x => x.Company)
                .HasForeignKey(x => x.CompanyId)
                .IsRequired(false);

            builder.HasMany(x => x.Orders)
                .WithOne(x => x.Company)
                .HasForeignKey(x => x.CompanyId)
                .IsRequired(false);
        }
    }
}
