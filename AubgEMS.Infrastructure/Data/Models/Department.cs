using System.ComponentModel.DataAnnotations;

namespace AubgEMS.Infrastructure.Data.Models;

public class Department
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Club> Clubs { get; set; } = new List<Club>();
}