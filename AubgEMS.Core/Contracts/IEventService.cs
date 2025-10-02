using AubgEMS.Core.Models.Common;
using AubgEMS.Core.Models.Events;

namespace AubgEMS.Core.Contracts;

public interface IEventService
{
    Task<PageResult<EventListItemDto>> GetAllAsync(EventQuery query, CancellationToken ct = default);
    Task<EventDetailsDto?> GetByIdAsync(int id, CancellationToken ct = default);

    Task<Result<int>> CreateAsync(EventCreateDto model, string currentUserId, CancellationToken ct = default);
    Task<Result> UpdateAsync(EventEditDto model, string currentUserId, CancellationToken ct = default);
    Task<Result> DeleteAsync(int id, string currentUserId, CancellationToken ct = default);
}