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
            // Load event capacity (and verify it exists)
            var ev = await _db.Events
                .Where(e => e.Id == eventId)
                .Select(e => new { e.Id, e.Capacity })
                .FirstOrDefaultAsync(ct);

            if (ev is null) return false;

            // Idempotent: already joined by this user?
            var already = await _db.EventAttendances
                .AnyAsync(a => a.EventId == eventId && a.UserId == userId, ct);
            if (already) return false;

            // Capacity check (treat <= 0 as "unlimited")
            var current = await _db.EventAttendances
                .CountAsync(a => a.EventId == eventId, ct);

            if (ev.Capacity > 0 && current >= ev.Capacity)
                return false;

            // Create attendance
            _db.EventAttendances.Add(new EventAttendance { EventId = eventId, UserId = userId });

            try
            {
                await _db.SaveChangesAsync(ct);
                return true;
            }
            catch (DbUpdateException)
            {
                // If a unique index exists on (EventId, UserId), this catches rare races / double clicks
                return false;
            }
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
