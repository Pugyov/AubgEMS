using System.ComponentModel.DataAnnotations;

namespace AubgEMS.Infrastructure.Data.Models;

public class Club
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    // Admin assigns the organizer (AspNetUsers.Id)
    [Required]
    public string OrganizerId { get; set; } = string.Empty;

    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }

    public ICollection<Event> Events { get; set; } = new List<Event>();
}