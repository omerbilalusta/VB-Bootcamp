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
        public int Amount { get; set; }
        public string PaymentMethod { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public virtual List<InvoiceDetail> InvoiceDetails { get; set; }
    }

    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(x => x.InsertUserId).IsRequired();
            builder.Property(x => x.UpdateUserId).IsRequired(false).HasDefaultValue(0);
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.Amount).IsRequired(true);
            builder.Property(x => x.PaymentMethod).IsRequired(true).HasMaxLength(10);

            builder.Property(x => x.OrderId).IsRequired(true);

            builder.HasOne(x => x.Order)
                .WithOne(x => x.Invoice)
                .HasForeignKey<Order>()
                .IsRequired(false);

            builder.HasMany(x => x.InvoiceDetails)
                .WithOne(x => x.Invoice)
                .HasForeignKey(x => x.InvoiceId)
                .IsRequired(false);
        }
    }
}
