using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_Base.Model;

namespace Vb_Data.Domain
{
    public class Payment : BaseModel
    {
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public int ReferenceNumber { get; set; }

        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
    }

    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(x => x.InsertUserId).IsRequired();
            builder.Property(x => x.UpdateUserId).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.PaymentMethod).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Amount).IsRequired().HasPrecision(10,2);
            builder.Property(x => x.ReferenceNumber).IsRequired();
            builder.Property(x => x.InvoiceId).IsRequired();

            builder.HasOne(x => x.Invoice)
                .WithOne(x => x.Payment)
                .HasForeignKey<Payment>()
                .IsRequired(true);

        }
    }
}
