namespace Services.Extensions;

public static class FlexibleOrderByExtension
{
    public static IEnumerable<T> FlexibleOrderBy<T>(this IEnumerable<T> source, Func<T, object> selector,
        bool ascending)
    {
        return ascending
            ? source.OrderBy(selector)
            : source.OrderByDescending(selector);
    }
}