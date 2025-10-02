namespace AubgEMS.Core.Models.News;

public class NewsListItemDto
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateTime CreatedAtUtc { get; init; }
    public string? ImageUrl { get; init; }
}