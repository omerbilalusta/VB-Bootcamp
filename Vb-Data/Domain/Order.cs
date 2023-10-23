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
    public class Order : BaseModel
    {
        public int OrderNumber { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public bool PaymentSuccess { get; set; }
        public bool CompanyApprove { get; set; }

        public int DealerId { get; set; }
        public virtual Dealer Dealer { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
        public int OrderRejectId { get; set; }
        public virtual OrderReject OrderReject { get; set; }
    }

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.InsertUserId).IsRequired();
            builder.Property(x => x.UpdateUserId).IsRequired(false).HasDefaultValue(0);
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.PaymentMethod).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.PaymentMethod).IsRequired(true).HasDefaultValue(false);
            builder.Property(x => x.PaymentSuccess).IsRequired(false);
            builder.Property(x => x.Amount).IsRequired(true);
            builder.Property(x => x.OrderNumber).IsRequired(true);

            builder.Property(x => x.DealerId).IsRequired(false);
            builder.Property(x => x.CompanyId).IsRequired(false);
            builder.Property(x => x.InvoiceId).IsRequired(false);
            builder.Property(x => x.OrderRejectId).IsRequired(false);

            builder.HasIndex(x => x.OrderNumber).IsUnique(true);

            builder.HasOne(x => x.Invoice)
                .WithOne(x => x.Order)
                .HasForeignKey<Invoice>()
                .IsRequired(false);

            builder.HasOne(x => x.OrderReject)
                .WithOne(x => x.Order)
                .HasForeignKey<OrderReject>()
                .IsRequired(false);
        }
    }
}
