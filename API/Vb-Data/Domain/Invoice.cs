using Vb_Base.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_Data.Domain.User;

namespace Vb_Data.Domain
{
    public class Invoice : BaseModel
    {
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public bool InvoiceExist { get; set; }
        public string Address { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }

    }

    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(x => x.InsertUserId).IsRequired();
            builder.Property(x => x.UpdateUserId).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Address).IsRequired().HasMaxLength(150);
            builder.Property(x => x.InvoiceExist).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.PaymentMethod).IsRequired().HasMaxLength(50);

            builder.Property(x => x.OrderId).IsRequired();
            builder.Property(x => x.PaymentId).IsRequired().HasDefaultValue(0);

            builder.HasOne(x => x.Order)
                .WithOne(x => x.Invoice)
                .HasForeignKey<Order>()
                .IsRequired(false);

            builder.HasOne(x => x.Payment)
                .WithOne(x => x.Invoice)
                .HasForeignKey<Payment>()
                .IsRequired(false);
        }
    }
}
