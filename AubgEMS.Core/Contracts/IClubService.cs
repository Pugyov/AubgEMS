using AubgEMS.Core.Models.Common;
using AubgEMS.Core.Models.Clubs;

namespace AubgEMS.Core.Contracts;

public interface IClubService
{
    Task<PageResult<ClubListItemDto>> GetAllAsync(ClubQuery query, CancellationToken ct = default);
    Task<ClubDetailsDto?> GetDetailsAsync(int id, CancellationToken ct = default);

    // Admin-only operations (enforce via controller [Authorize])
    Task<int> CreateAsync(ClubEditDto dto, CancellationToken ct = default);
    Task<bool> UpdateAsync(int id, ClubEditDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    Task<ClubEditDto?> GetForEditAsync(int id, CancellationToken ct = default);

}