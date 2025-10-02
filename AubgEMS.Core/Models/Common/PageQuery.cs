namespace AubgEMS.Core.Models.Common;

public class PageQuery
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;

    public PageQuery() { }

    public PageQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}