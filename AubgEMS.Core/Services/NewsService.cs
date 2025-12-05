using AubgEMS.Core.Contracts;
using AubgEMS.Core.Models.Common;
using AubgEMS.Core.Models.News;
using AubgEMS.Infrastructure.Data;
using AubgEMS.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AubgEMS.Core.Services
{
    public class NewsService : INewsService
    {
        private readonly ApplicationDbContext _db;
        public NewsService(ApplicationDbContext db) => _db = db;

        public async Task<PageResult<NewsListItemDto>> GetAllAsync(PageQuery page, CancellationToken ct = default)
        {
            var q = _db.NewsPosts.AsNoTracking();

            var total = await q.CountAsync(ct);

            var items = await q
                .OrderByDescending(n => n.CreatedAt)
                .Skip((page.Page - 1) * page.PageSize)
                .Take(page.PageSize)
                .Select(n => new NewsListItemDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    CreatedAtUtc = n.CreatedAt,
                    ImageUrl = n.ImageUrl
                })
                .ToListAsync(ct);

            return new PageResult<NewsListItemDto>(items, total, page.Page, page.PageSize);
        }

        public async Task<NewsDetailsDto?> GetDetailsAsync(int id, CancellationToken ct = default)
        {
            return await _db.NewsPosts
                .AsNoTracking()
                .Where(n => n.Id == id)
                .Select(n => new NewsDetailsDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Body = n.Body,
                    CreatedAtUtc = n.CreatedAt,
                    ImageUrl = n.ImageUrl
                })
                .FirstOrDefaultAsync(ct);
        }

        public async Task<int> CreateAsync(NewsEditDto model, CancellationToken ct = default)
        {
            var entity = new NewsPost
            {
                Title = model.Title.Trim(),
                Body = model.Body.Trim(),
                ImageUrl = model.ImageUrl,
                CreatedAt = DateTime.UtcNow
            };

            _db.NewsPosts.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(int id, NewsEditDto model, CancellationToken ct = default)
        {
            var entity = await _db.NewsPosts.FirstOrDefaultAsync(n => n.Id == id, ct);
            if (entity is null) return false;

            entity.Title = model.Title.Trim();
            entity.Body = model.Body.Trim();
            entity.ImageUrl = model.ImageUrl;

            await _db.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _db.NewsPosts.FirstOrDefaultAsync(n => n.Id == id, ct);
            if (entity is null) return false;

            _db.NewsPosts.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}