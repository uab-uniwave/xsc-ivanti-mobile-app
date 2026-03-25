using Application.Common;
using System.Text;

namespace Infrastructure.Utilities;

/// <summary>
/// Builder for constructing OData query strings.
/// Used to build query parameters for Ivanti API requests.
/// </summary>
internal static class ODataQueryBuilder
{
    /// <summary>
    /// Builds an OData query string from pagination and filtering parameters.
    /// </summary>
    public static string Build(PagedQuery query)
    {
        var skip = (query.Page - 1) * query.PageSize;

        var sb = new StringBuilder();
        sb.Append($"?$top={query.PageSize}");
        sb.Append($"&$skip={skip}");
        sb.Append("&$count=true");

        var filters = new List<string>();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            filters.Add($"contains(Subject,'{Escape(query.Search)}')");
        }

        if (query.Filters != null)
        {
            foreach (var f in query.Filters)
            {
                filters.Add($"{f.Key} eq '{Escape(f.Value)}'");
            }
        }

        if (filters.Any())
        {
            sb.Append("&$filter=" + string.Join(" and ", filters));
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            var direction = query.Desc ? "desc" : "asc";
            sb.Append($"&$orderby={query.SortBy} {direction}");
        }

        return sb.ToString();
    }

    private static string Escape(string input)
        => input.Replace("'", "''");
}
