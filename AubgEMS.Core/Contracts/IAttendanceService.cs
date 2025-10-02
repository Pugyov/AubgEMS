using AubgEMS.Core.Models.Common;
using AubgEMS.Core.Models.Events;

namespace AubgEMS.Core.Contracts;

public interface IAttendanceService
{
    Task<bool> JoinAsync(int eventId, string userId, CancellationToken ct = default);
    Task<bool> LeaveAsync(int eventId, string userId, CancellationToken ct = default);

    // My Events = (joined ∪ created), upcoming first
    Task<PageResult<EventListItemDto>> GetMyEventsAsync(string userId, PageQuery page, CancellationToken ct = default);
}