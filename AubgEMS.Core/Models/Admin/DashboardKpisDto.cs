namespace AubgEMS.Core.Models.Admin
{
    public class DashboardKpisDto
    {
        public int TotalEvents { get; set; }

        // placeholders for upcoming KPIs (we’ll fill these in next steps)
        public int TotalJoins { get; set; }
        public int UniqueAttendees { get; set; }
        public double AvgFillRate { get; set; }
        public int UpcomingWeekLoad { get; set; }
    }
}