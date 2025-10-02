using AubgEMS.Core.Models.Common;

namespace AubgEMS.Core.Models.Clubs;

public class ClubQuery : PageQuery
{
    public int? DepartmentId { get; init; }
    public string? Search { get; init; }

    public ClubQuery() { }
    public ClubQuery(int page, int pageSize) : base(page, pageSize) { }
}