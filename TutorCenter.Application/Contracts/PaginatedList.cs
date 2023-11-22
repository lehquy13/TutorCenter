namespace TutorCenter.Application.Contracts;

public class PaginatedList<T> : List<T>
{
    private PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }

    public PaginatedList()
    {
    }

    public int PageIndex { get; }
    public int TotalPages { get; }
    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;

    public static PaginatedList<T> CreateAsync(IEnumerable<T> source, int pageIndex, int pageSize, int count = 0)
    {
        var enumerable = source as T[] ?? source.ToArray();
        if (count == 0)
            count = enumerable.Count();
        //var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return new PaginatedList<T>(enumerable.ToList(), count, pageIndex, pageSize);
    }
}