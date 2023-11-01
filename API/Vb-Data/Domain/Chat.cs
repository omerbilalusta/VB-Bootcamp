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
    public class Chat : BaseModel
    {
        public int DealerId { get; set; }
        public virtual Dealer Dealer { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual List<Message> Messages { get; set; }
    }

    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.Property(x => x.InsertUserId).IsRequired();
            builder.Property(x => x.UpdateUserId).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.DealerId).IsRequired();
            builder.Property(x => x.CompanyId).IsRequired();

            builder.HasMany(x => x.Messages)
                .WithOne(x=> x.Chat)
                .HasForeignKey(x=>x.ChatId)
                .IsRequired();
        }
    }
}
