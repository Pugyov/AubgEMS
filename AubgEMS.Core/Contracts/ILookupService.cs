using AubgEMS.Core.Models.Lookups;

namespace AubgEMS.Core.Contracts;

public interface ILookupService
{
    Task<IReadOnlyList<LookupItemModel>> EventTypesAsync(CancellationToken ct = default);
    Task<IReadOnlyList<LookupItemModel>> DepartmentsAsync(CancellationToken ct = default);
    Task<IReadOnlyList<LookupItemModel>> ClubsAsync(int? departmentId = null, CancellationToken ct = default);
    Task<IReadOnlyList<LookupItemModel>> LocationsAsync(CancellationToken ct = default);
}