namespace AubgEMS.Core.Models.Clubs;

public class ClubDetailsDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public int DepartmentId { get; init; }
    public string DepartmentName { get; init; } = string.Empty;
}