using System.ComponentModel.DataAnnotations;

namespace AubgEMS.Infrastructure.Data.Models;

public class NewsPost
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required] // no max length → LONGTEXT in MySQL via Pomelo
    public string Body { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}