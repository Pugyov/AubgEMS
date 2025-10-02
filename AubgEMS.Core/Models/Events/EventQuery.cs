using AubgEMS.Core.Models.Common;

namespace AubgEMS.Core.Models.Events;

public class EventQuery : PageQuery
{
    public int? EventTypeId  { get; init; }
    public int? ClubId       { get; init; }
    public int? DepartmentId { get; init; }
    public string? Search    { get; init; }
    public DateTime? FromUtc { get; init; }
    public DateTime? ToUtc   { get; init; }

    public EventQuery() { }
    public EventQuery(int page, int pageSize) : base(page, pageSize) { }
}