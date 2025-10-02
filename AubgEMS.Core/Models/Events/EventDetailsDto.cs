namespace AubgEMS.Core.Models.Events;

public class EventDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public int Capacity { get; set; }

    public string OrganizerId { get; set; } = string.Empty;

    public int ClubId { get; set; }
    public string ClubName { get; set; } = string.Empty;

    public int EventTypeId { get; set; }
    public string EventTypeName { get; set; } = string.Empty;

    public int? LocationId { get; set; }
    public string? LocationName { get; set; }

    public string? ImageUrl { get; set; }
}