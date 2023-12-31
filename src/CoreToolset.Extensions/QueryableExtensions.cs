using CoreToolset.Extensions.Models.Pagination;
using CoreToolset.Extensions.ValidationHelpers;
using System.Linq.Expressions;

namespace CoreToolset.Extensions
{
    public static class QueryableExtensions
    {
        public static IPaginate<T> ToPaginate<T>(this IQueryable<T> source, int index, int size,
                                                  int from = 0)
        {
            PaginationValidationHelper.ValidateParameters(index, size, from);

            int count = source.Count();
            List<T> items = [.. source.Skip((index - from) * size).Take(size)];

            Paginate<T> list = new()
            {
                Index = index,
                Size = size,
                From = from,
                Count = count,
                Items = items,
                Pages = (int)Math.Ceiling(count / (double)size)
            };
            return list;
        }
    }
}
