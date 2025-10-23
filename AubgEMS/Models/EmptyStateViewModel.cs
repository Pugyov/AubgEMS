namespace AubgEMS.Models
{
    public class EmptyStateViewModel
    {
        public string Icon { get; set; } = "bi-info-circle"; // Bootstrap Icons key
        public string Title { get; set; } = "Nothing here yet";
        public string? Message { get; set; }

        // Optional CTA
        public string? ActionText { get; set; }
        public string? ActionArea { get; set; }
        public string? ActionController { get; set; }
        public string? ActionAction { get; set; }
        public IDictionary<string, string?>? RouteValues { get; set; }
    }
}