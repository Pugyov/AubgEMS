// EventListItemDto.cs
namespace AubgEMS.Core.Models.Events;

public class EventListItemDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public int Capacity { get; set; }
    public string ClubName { get; set; } = string.Empty;
    public string EventTypeName { get; set; } = string.Empty;
    public string? LocationName { get; set; }
    public string? ImageUrl { get; set; }
}