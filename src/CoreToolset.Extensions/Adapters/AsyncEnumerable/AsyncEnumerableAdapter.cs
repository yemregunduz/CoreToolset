namespace CoreToolset.Extensions.Adapters.AsyncEnumerable
{
    internal class AsyncEnumerableAdapter<T>(IQueryable<T> source) : IAsyncEnumerable<T>
    {
        private readonly IQueryable<T> _source = source ?? throw new ArgumentNullException(nameof(source));

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new AsyncEnumeratorAdapter<T>(_source.GetEnumerator());
        }
    }
}
