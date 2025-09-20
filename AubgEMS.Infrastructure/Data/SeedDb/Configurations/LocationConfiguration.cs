using AubgEMS.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AubgEMS.Infrastructure.Data.SeedDb.Configurations
{
    internal class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();

            var data = new SeedData();
            builder.HasData(data.Locations);
        }
    }
}