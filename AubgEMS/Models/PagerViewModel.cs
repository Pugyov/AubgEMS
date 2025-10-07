namespace AubgEMS.Models
{
    public class PagerViewModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public string Controller { get; set; } = "Home";
        public string Action { get; set; } = "Index";
        public IDictionary<string,string?> RouteValues { get; set; } = new Dictionary<string,string?>();
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}