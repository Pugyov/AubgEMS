using AubgEMS.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AubgEMS.Infrastructure.Data.SeedDb.Configurations
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            var data = new SeedData();
            builder.HasData(data.Departments);
        }
    }
}