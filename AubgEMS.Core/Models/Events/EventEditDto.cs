using System.ComponentModel.DataAnnotations;

namespace AubgEMS.Core.Models.Events;

public class EventEditDto
{
    [Required]
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public DateTime StartTime { get; set; }

    [Range(0, 100000)]
    public int Capacity { get; set; }

    [Required]
    public int ClubId { get; set; }

    [Required]
    public int EventTypeId { get; set; }

    public int? LocationId { get; set; }

    [MaxLength(500)]
    public string? ImageUrl { get; set; }
}