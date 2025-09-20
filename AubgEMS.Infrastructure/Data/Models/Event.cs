using System.ComponentModel.DataAnnotations;

namespace AubgEMS.Infrastructure.Data.Models;

public class Event
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    // Stored per spec (no enforcement logic here)
    public int Capacity { get; set; }

    // Organizer (AspNetUsers.Id)
    [Required]
    public string OrganizerId { get; set; } = string.Empty;

    // Optional location
    public int? LocationId { get; set; }
    public Location? Location { get; set; }

    // REQUIRED club (every event belongs to a club)
    public int ClubId { get; set; }
    public Club Club { get; set; } = null!;

    // REQUIRED event type
    public int EventTypeId { get; set; }
    public EventType EventType { get; set; } = null!;
    
    [MaxLength(500)]
    [Url]
    public string? ImageUrl { get; set; }
}