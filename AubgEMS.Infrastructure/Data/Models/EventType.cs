using System.ComponentModel.DataAnnotations;

namespace AubgEMS.Infrastructure.Data.Models;

public class EventType
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Event> Events { get; set; } = new List<Event>();
}