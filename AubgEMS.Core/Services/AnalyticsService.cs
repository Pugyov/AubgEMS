using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AubgEMS.Core.Contracts;
using AubgEMS.Core.Models.Admin;
using AubgEMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AubgEMS.Core.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly ApplicationDbContext _db;
        public AnalyticsService(ApplicationDbContext db) => _db = db;

        public async Task<DashboardKpisDto> GetKpisAsync(
            DateTime fromUtc,
            DateTime toUtc,
            int? departmentId,
            int? clubId,
            int? eventTypeId,
            CancellationToken ct = default)
        {
            // Base query: events within [from, to]
            var ev = _db.Events.AsNoTracking()
                .Where(e => e.StartTime >= fromUtc && e.StartTime <= toUtc);

            // Optional filters
            if (departmentId.HasValue)
                ev = ev.Where(e => e.Club.DepartmentId == departmentId.Value);
            if (clubId.HasValue)
                ev = ev.Where(e => e.ClubId == clubId.Value);
            if (eventTypeId.HasValue)
                ev = ev.Where(e => e.EventTypeId == eventTypeId.Value);

            // KPI #1: Total Events
            var totalEvents = await ev.CountAsync(ct);

            // KPI #2: Total Sign-ups (Joins) — join attendances to filtered events
            var totalJoins = await (
                from a in _db.EventAttendances.AsNoTracking()
                join e in ev on a.EventId equals e.Id
                select a
            ).CountAsync(ct);

            // KPI #3: Unique Attendees — distinct users who joined filtered events
            var uniqueAttendees = await (
                from a in _db.EventAttendances.AsNoTracking()
                join e in ev on a.EventId equals e.Id
                select a.UserId
            ).Distinct().CountAsync(ct);

            // KPI #4: Avg. Fill Rate (capacity-normalized)
            // Left-join attendances to filtered events and count joins per event
            var perEvent = await (
                from e in ev
                join a in _db.EventAttendances.AsNoTracking()
                    on e.Id equals a.EventId into g
                select new { e.Capacity, Joins = g.Count() }
            ).ToListAsync(ct);

            var ratios = perEvent
                .Where(x => x.Capacity > 0)
                .Select(x => Math.Min(x.Joins, x.Capacity) / (double)x.Capacity);

            var avgFillRate = ratios.Any() ? ratios.Average() : 0.0;

            // KPI #5: Upcoming Week Load (ignores date filter; applies other filters)
            var nowUtc = DateTime.UtcNow;
            var weekToUtc = nowUtc.AddDays(7);

            var nextWeek = _db.Events.AsNoTracking()
                .Where(e => e.StartTime >= nowUtc && e.StartTime <= weekToUtc);

            if (departmentId.HasValue)
                nextWeek = nextWeek.Where(e => e.Club.DepartmentId == departmentId.Value);
            if (clubId.HasValue)
                nextWeek = nextWeek.Where(e => e.ClubId == clubId.Value);
            if (eventTypeId.HasValue)
                nextWeek = nextWeek.Where(e => e.EventTypeId == eventTypeId.Value);

            var upcomingWeekLoad = await nextWeek.CountAsync(ct);

            return new DashboardKpisDto
            {
                TotalEvents       = totalEvents,
                TotalJoins        = totalJoins,
                UniqueAttendees   = uniqueAttendees,
                AvgFillRate       = avgFillRate,
                UpcomingWeekLoad  = upcomingWeekLoad
            };
        }
    }
}