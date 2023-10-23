using Vb_Base.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vb_Data.Domain.User
{
    public class Dealer : UserModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string InvoiceAddress { get; set; }
        public decimal Dividend { get; set; }
        public decimal OpenAccountLimit { get; set; }

        public virtual List<Order> Orders { get; set; }
        public virtual List<Chat> Chats { get; set; }
    }

    public class DealerConfiguration : IEntityTypeConfiguration<Dealer>
    {
        public void Configure(EntityTypeBuilder<Dealer> builder)
        {
            builder.Property(x => x.InsertUserId).IsRequired();
            builder.Property(x => x.UpdateUserId).IsRequired(false).HasDefaultValue(0);
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Address).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.InvoiceAddress).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.Dividend).IsRequired(true).HasPrecision(10,2);
            builder.Property(x => x.Role).IsRequired(true).HasMaxLength(10).HasDefaultValue("dealer");

            builder.HasMany(x => x.Chats)
                .WithOne(x => x.Dealer)
                .HasForeignKey(x => x.DealerId)
                .IsRequired(false);

            builder.HasMany(x => x.Orders)
                .WithOne(x => x.Dealer)
                .HasForeignKey(x => x.DealerId)
                .IsRequired(false);
        }
    }
}
