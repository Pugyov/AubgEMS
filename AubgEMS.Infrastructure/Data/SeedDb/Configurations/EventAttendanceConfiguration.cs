using AubgEMS.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AubgEMS.Infrastructure.Data.SeedDb.Configurations
{
    internal class EventAttendanceConfiguration : IEntityTypeConfiguration<EventAttendance>
    {
        public void Configure(EntityTypeBuilder<EventAttendance> builder)
        {
            builder.HasKey(a => new { a.EventId, a.UserId });

            builder.HasOne(a => a.Event)
                .WithMany() // no navigation needed on Event for now
                .HasForeignKey(a => a.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}