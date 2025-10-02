using AubgEMS.Core.Contracts;
using AubgEMS.Core.Models.Clubs;
using AubgEMS.Core.Models.Common;
using AubgEMS.Infrastructure.Data;
using AubgEMS.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AubgEMS.Core.Services
{
    public class ClubService : IClubService
    {
        private readonly ApplicationDbContext _db;
        public ClubService(ApplicationDbContext db) => _db = db;

        public async Task<PageResult<ClubListItemDto>> GetAllAsync(ClubQuery query, CancellationToken ct = default)
        {
            var q = _db.Clubs
                .AsNoTracking()
                .Include(c => c.Department)
                .AsQueryable();

            if (query.DepartmentId is int deptId)
                q = q.Where(c => c.DepartmentId == deptId);

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                var s = query.Search.Trim();
                q = q.Where(c =>
                    c.Name.Contains(s) ||
                    (c.Description != null && c.Description.Contains(s)));
            }

            var total = await q.CountAsync(ct);

            var items = await q
                .OrderBy(c => c.Name)
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(c => new ClubListItemDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    DepartmentName = c.Department.Name
                })
                .ToListAsync(ct);

            return new PageResult<ClubListItemDto>(items, total, query.Page, query.PageSize);
        }

        public async Task<ClubDetailsDto?> GetDetailsAsync(int id, CancellationToken ct = default)
        {
            return await _db.Clubs
                .AsNoTracking()
                .Include(c => c.Department)
                .Where(c => c.Id == id)
                .Select(c => new ClubDetailsDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    DepartmentId = c.DepartmentId,
                    DepartmentName = c.Department.Name
                })
                .FirstOrDefaultAsync(ct);
        }

        // Admin-only operations

        public async Task<int> CreateAsync(ClubEditDto dto, CancellationToken ct = default)
        {
            var entity = new Club
            {
                Name = dto.Name.Trim(),
                Description = dto.Description?.Trim(),
                DepartmentId = dto.DepartmentId,
                // per your DTO: OrganizerId is required
                OrganizerId = dto.OrganizerId
            };

            _db.Clubs.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(int id, ClubEditDto dto, CancellationToken ct = default)
        {
            var entity = await _db.Clubs.FirstOrDefaultAsync(c => c.Id == id, ct);
            if (entity is null) return false;

            entity.Name = dto.Name.Trim();
            entity.Description = dto.Description?.Trim();
            entity.DepartmentId = dto.DepartmentId;
            entity.OrganizerId = dto.OrganizerId;

            await _db.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            // If a club has events, deleting will violate FK (Events -> ClubId).
            // Guard explicitly so we fail gracefully.
            var hasEvents = await _db.Events.AnyAsync(e => e.ClubId == id, ct);
            if (hasEvents) return false;

            var entity = await _db.Clubs.FirstOrDefaultAsync(c => c.Id == id, ct);
            if (entity is null) return false;

            _db.Clubs.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
