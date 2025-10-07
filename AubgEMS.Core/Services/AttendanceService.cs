using AubgEMS.Core.Contracts;
using AubgEMS.Core.Models.Common;
using AubgEMS.Core.Models.Events;
using AubgEMS.Infrastructure.Data;
using AubgEMS.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AubgEMS.Core.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ApplicationDbContext _db;
        public AttendanceService(ApplicationDbContext db) => _db = db;

        public async Task<bool> JoinAsync(int eventId, string userId, CancellationToken ct = default)
        {
            // event must exist
            var evExists = await _db.Events.AnyAsync(e => e.Id == eventId, ct);
            if (!evExists) return false;

            // idempotent join
            var exists = await _db.EventAttendances
                .AnyAsync(a => a.EventId == eventId && a.UserId == userId, ct);
            if (exists) return false;

            _db.EventAttendances.Add(new EventAttendance { EventId = eventId, UserId = userId });
            await _db.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> LeaveAsync(int eventId, string userId, CancellationToken ct = default)
        {
            var att = await _db.EventAttendances
                .FirstOrDefaultAsync(a => a.EventId == eventId && a.UserId == userId, ct);
            if (att is null) return false;

            _db.EventAttendances.Remove(att);
            await _db.SaveChangesAsync(ct);
            return true;
        }

        public async Task<PageResult<EventListItemDto>> GetMyEventsAsync(
            string userId, PageQuery page, CancellationToken ct = default)
        {
            int p = page?.Page     ?? 1;  if (p < 1) p = 1;
            int s = page?.PageSize ?? 10; if (s < 1) s = 10;

            var nowUtc = DateTime.UtcNow;

            var joinedIds = _db.EventAttendances
                .Where(a => a.UserId == userId)
                .Select(a => a.EventId);

            var createdIds = _db.Events
                .Where(e => e.OrganizerId == userId)
                .Select(e => e.Id);

            // joined ∪ created (no dups)
            var eventIds = joinedIds.Union(createdIds);

            var q = _db.Events
                .AsNoTracking()
                .Where(e => eventIds.Contains(e.Id))
                .Include(e => e.Club)
                .Include(e => e.EventType)
                .Include(e => e.Location);

            var total = await q.CountAsync(ct);

            var items = await q
                // Upcoming first, then by actual start time
                .OrderBy(e => e.StartTime < nowUtc) // false (upcoming) first
                .ThenBy(e => e.StartTime)
                .Skip((p - 1) * s)
                .Take(s)
                .Select(e => new EventListItemDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    StartTime = e.StartTime,
                    Capacity = e.Capacity,
                    ClubName = e.Club.Name,
                    EventTypeName = e.EventType.Name,
                    LocationName = e.Location != null ? e.Location.Name : null,
                    ImageUrl = e.ImageUrl,

                    // NEW: set flags for the UI
                    CreatedByMe = (e.OrganizerId == userId),
                    JoinedByMe  = joinedIds.Contains(e.Id)
                })
                .ToListAsync(ct);

            return new PageResult<EventListItemDto>(items, total, p, s);
        }
        public async Task<bool> IsJoinedAsync(int eventId, string userId, CancellationToken ct = default)
        {
            return await _db.EventAttendances
                .AnyAsync(a => a.EventId == eventId && a.UserId == userId, ct);
        }

    }
}
