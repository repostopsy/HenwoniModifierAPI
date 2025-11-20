using Microsoft.EntityFrameworkCore;

namespace HenwoniDataModifierAPI.Common
{
    public class PaginatedList<T>
    {
        public List<T> Content { get; set; }
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalItems { get; private set; }
        public int MinCount { get; private set; }
        public int MaxCount { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            Content = new List<T>();
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalItems = count;
            MaxCount = pageIndex * pageSize;
            MinCount = MaxCount - pageSize;
            if (MaxCount > count) MaxCount = count;
            this.Content.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var r = source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
