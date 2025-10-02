using System.ComponentModel.DataAnnotations;

namespace AubgEMS.Core.Models.News;

public class NewsEditDto
{
    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Body { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? ImageUrl { get; set; }
}