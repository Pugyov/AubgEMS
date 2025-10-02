using AubgEMS.Core.Contracts;
using AubgEMS.Core.Models.Lookups;
using AubgEMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AubgEMS.Core.Services
{
    public class LookupService : ILookupService
    {
        private readonly ApplicationDbContext _db;
        public LookupService(ApplicationDbContext db) => _db = db;

        public async Task<IReadOnlyList<LookupItemModel>> EventTypesAsync(CancellationToken ct = default)
            => await _db.EventTypes
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .Select(x => new LookupItemModel(x.Id, x.Name))
                .ToListAsync(ct);

        public async Task<IReadOnlyList<LookupItemModel>> DepartmentsAsync(CancellationToken ct = default)
            => await _db.Departments
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .Select(x => new LookupItemModel(x.Id, x.Name))
                .ToListAsync(ct);

        public async Task<IReadOnlyList<LookupItemModel>> ClubsAsync(int? departmentId = null, CancellationToken ct = default)
        {
            var q = _db.Clubs.AsNoTracking();

            if (departmentId.HasValue)
                q = q.Where(c => c.DepartmentId == departmentId.Value);

            return await q.OrderBy(c => c.Name)
                .Select(c => new LookupItemModel(c.Id, c.Name))
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<LookupItemModel>> LocationsAsync(CancellationToken ct = default)
            => await _db.Locations
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .Select(x => new LookupItemModel(x.Id, x.Name))
                .ToListAsync(ct);
    }
}