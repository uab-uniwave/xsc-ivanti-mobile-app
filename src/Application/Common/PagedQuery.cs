namespace Application.Common;

public class PagedQuery
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;

    public string? Search { get; init; }

    public string? SortBy { get; init; }
    public bool Desc { get; init; }

    public Dictionary<string, string>? Filters { get; init; }
}