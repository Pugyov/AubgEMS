using AubgEMS.Core.Models.Common;
using AubgEMS.Core.Models.News;

namespace AubgEMS.Core.Contracts;

public interface INewsService
{
    Task<PageResult<NewsListItemDto>> GetAllAsync(PageQuery page, CancellationToken ct = default);
    Task<NewsDetailsDto?> GetDetailsAsync(int id, CancellationToken ct = default);

    // Admin-only
    Task<int> CreateAsync(NewsEditDto model, CancellationToken ct = default);
    Task<bool> UpdateAsync(int id, NewsEditDto model, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}