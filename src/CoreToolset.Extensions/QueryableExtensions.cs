using CoreToolset.Extensions.Adapters.AsyncEnumerable;
using CoreToolset.Extensions.Models.DynamicQuery;
using CoreToolset.Extensions.Models.Pagination;
using CoreToolset.Extensions.ValidationHelpers;

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

        public static async Task<IPaginate<T>> ToPaginateAsync<T>(this IQueryable<T> source, int index, int size,
                                                                  int from = 0,
                                                                  CancellationToken cancellationToken = default)
        {
            PaginationValidationHelper.ValidateParameters(index, size, from);

            int count = source.Count();
            List<T> items = await source.Skip((index - from) * size).Take(size).ToListAsyncCT(cancellationToken)
                                        .ConfigureAwait(false);
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


        public static IQueryable<T> ToDynamic<T>(
                this IQueryable<T> query, Dynamic dynamic)
        {
            if (dynamic.Filter is not null) 
                query = DynamicQueryHelper.Filter(query, dynamic.Filter);
            if (dynamic.Sort is not null && dynamic.Sort.Any()) 
                query = DynamicQueryHelper.Sort(query, dynamic.Sort);

            return query;
        }

        public static async Task<List<T>> ToListAsyncCT<T>(this IQueryable<T> source, CancellationToken cancellationToken = default)
        {
            var list = new List<T>();

            await foreach (var item in new AsyncEnumerableAdapter<T>(source).WithCancellation(cancellationToken))
            {
                list.Add(item);
            }

            return list;
        }

    }
}
