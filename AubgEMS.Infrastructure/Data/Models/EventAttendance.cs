namespace AubgEMS.Infrastructure.Data.Models;

public class EventAttendance
{
    public int EventId { get; set; }
    public Event Event { get; set; } = null!;

    public string UserId { get; set; } = string.Empty; // AspNetUsers.Id

    // Presence of row means “joined”; keep flag per schema
    public bool IsAttending { get; set; } = true;
}