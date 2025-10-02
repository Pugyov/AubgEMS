using AubgEMS.Core.Contracts;
using AubgEMS.Core.Models.Common;
using AubgEMS.Core.Models.Events;
using AubgEMS.Infrastructure.Data;
using AubgEMS.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AubgEMS.Core.Services;

public class EventService : IEventService
{
    private readonly ApplicationDbContext _db;
    public EventService(ApplicationDbContext db) => _db = db;

    public async Task<PageResult<EventListItemDto>> GetAllAsync(EventQuery query, CancellationToken ct = default)
    {
        var q = _db.Events
            .AsNoTracking()
            .Include(e => e.Club)
            .Include(e => e.EventType)
            .Include(e => e.Location)
            .AsQueryable();

        // filters
        if (query.EventTypeId is { } etId)
            q = q.Where(e => e.EventTypeId == etId);

        if (query.ClubId is { } clubId)
            q = q.Where(e => e.ClubId == clubId);

        if (query.DepartmentId is { } deptId)
            q = q.Where(e => e.Club.DepartmentId == deptId);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var s = query.Search.Trim();
            q = q.Where(e => e.Title.Contains(s) || e.Description.Contains(s));
        }

        if (query.FromUtc is { } from)
            q = q.Where(e => e.StartTime >= from);

        if (query.ToUtc is { } to)
            q = q.Where(e => e.StartTime <= to);

        var total = await q.CountAsync(ct);

        var items = await q
            .OrderBy(e => e.StartTime)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(e => new EventListItemDto
            {
                Id = e.Id,
                Title = e.Title,
                StartTime = e.StartTime,
                Capacity = e.Capacity,
                ClubName = e.Club.Name,
                EventTypeName = e.EventType.Name,
                LocationName = e.Location != null ? e.Location.Name : null,
                ImageUrl = e.ImageUrl
            })
            .ToListAsync(ct);
        
        return new PageResult<EventListItemDto>(items, total, query.Page, query.PageSize);
    }

    public async Task<EventDetailsDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _db.Events
            .AsNoTracking()
            .Include(e => e.Club)
            .Include(e => e.EventType)
            .Include(e => e.Location)
            .Where(e => e.Id == id)
            .Select(e => new EventDetailsDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                StartTime = e.StartTime,
                Capacity = e.Capacity,
                OrganizerId = e.OrganizerId,

                ClubId = e.ClubId,
                ClubName = e.Club.Name,

                EventTypeId = e.EventTypeId,
                EventTypeName = e.EventType.Name,

                LocationId = e.LocationId,
                LocationName = e.Location != null ? e.Location.Name : null,

                ImageUrl = e.ImageUrl
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<Result<int>> CreateAsync(EventCreateDto model, string currentUserId, CancellationToken ct = default)
    {
        // OrganizerId is taken from current user, ignore inbound value if you prefer
        var entity = new Event
        {
            Title = model.Title.Trim(),
            Description = model.Description.Trim(),
            StartTime = model.StartTime,
            Capacity = model.Capacity,
            OrganizerId = currentUserId,
            ClubId = model.ClubId,
            EventTypeId = model.EventTypeId,
            LocationId = model.LocationId,
            ImageUrl = model.ImageUrl
        };

        _db.Events.Add(entity);
        await _db.SaveChangesAsync(ct);

        return Result<int>.Ok(entity.Id);
    }

    public async Task<Result> UpdateAsync(EventEditDto model, string currentUserId, CancellationToken ct = default)
    {
        var e = await _db.Events.FirstOrDefaultAsync(x => x.Id == model.Id, ct);
        if (e is null)
            return Result.Fail("Event not found.");

        if (e.OrganizerId != currentUserId)
            return Result.Fail("You cannot edit another organizer’s event.");

        e.Title = model.Title.Trim();
        e.Description = model.Description.Trim();
        e.StartTime = model.StartTime;
        e.Capacity = model.Capacity;
        e.ClubId = model.ClubId;
        e.EventTypeId = model.EventTypeId;
        e.LocationId = model.LocationId;
        e.ImageUrl = model.ImageUrl;

        await _db.SaveChangesAsync(ct);
        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(int id, string currentUserId, CancellationToken ct = default)
    {
        var e = await _db.Events.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (e is null)
            return Result.Fail("Event not found.");

        if (e.OrganizerId != currentUserId)
            return Result.Fail("You cannot delete another organizer’s event.");

        _db.Events.Remove(e);
        await _db.SaveChangesAsync(ct);
        return Result.Ok();
    }
}
