using System.ComponentModel.DataAnnotations;

namespace AubgEMS.Core.Models.Clubs;

public class ClubEditDto
{
    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Required]
    public int DepartmentId { get; set; }

    [Required]
    public string OrganizerId { get; set; } = string.Empty;
}