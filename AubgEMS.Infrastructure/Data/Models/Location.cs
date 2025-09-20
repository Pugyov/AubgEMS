using System.ComponentModel.DataAnnotations;

namespace AubgEMS.Infrastructure.Data.Models;

public class Location
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public int Capacity { get; set; }

    public ICollection<Event> Events { get; set; } = new List<Event>();
}