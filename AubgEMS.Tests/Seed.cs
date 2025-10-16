using System;
using System.Threading.Tasks;
using AubgEMS.Infrastructure.Data;
using AubgEMS.Infrastructure.Data.Models;

namespace AubgEMS.Tests
{
    public static class Seed
    {
        public static async Task<(Club club, EventType type)> BasicsAsync(ApplicationDbContext db)
        {
            // unique suffix to avoid collisions with HasData unique indexes
            var tag = Guid.NewGuid().ToString("N")[..6];

            var dept = new Department { Name = $"Dept_{tag}" };
            var club = new Club { Name = $"Club_{tag}", Department = dept };
            var type = new EventType { Name = $"Type_{tag}" };

            db.AddRange(dept, club, type);
            await db.SaveChangesAsync();
            return (club, type);
        }
    }
}