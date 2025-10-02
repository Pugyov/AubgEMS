using System.ComponentModel.DataAnnotations;

namespace AubgEMS.Core.Models.Events;

public class EventCreateDto
{
    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public DateTime StartTime { get; set; }   // ⬅ unify on StartTime

    public int Capacity { get; set; }

    // we ignore inbound OrganizerId and take it from the current user
    [Required]
    public int ClubId { get; set; }

    [Required]
    public int EventTypeId { get; set; }

    public int? LocationId { get; set; }

    [MaxLength(500), Url]
    public string? ImageUrl { get; set; }
}