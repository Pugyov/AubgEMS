using AubgEMS.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AubgEMS.Infrastructure.Data.SeedDb.Configurations
{
    internal class EventTypeConfiguration : IEntityTypeConfiguration<EventType>
    {
        public void Configure(EntityTypeBuilder<EventType> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            var data = new SeedData();
            builder.HasData(data.EventTypes);
        }
    }
}