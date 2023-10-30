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
    public class OrderReject : BaseModel
    {
        public string Description { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }

    public class OrderRejectConfiguration : IEntityTypeConfiguration<OrderReject>
    {
        public void Configure(EntityTypeBuilder<OrderReject> builder)
        {
            builder.Property(x => x.InsertUserId).IsRequired();
            builder.Property(x => x.UpdateUserId).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.Description).IsRequired().HasMaxLength(100);

            builder.Property(x => x.OrderId).IsRequired();

            //builder.HasOne(x => x.Order)
            //    .WithOne(x => x.OrderReject)
            //    .HasForeignKey<Order>()
            //    .IsRequired(true);
        }
    }
}
