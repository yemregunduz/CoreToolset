namespace CoreToolset.Extensions.Adapters.AsyncEnumerable
{
    internal class AsyncEnumeratorAdapter<T>(IEnumerator<T> enumerator) : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator = enumerator
            ?? throw new ArgumentNullException(nameof(enumerator));

        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(_enumerator.MoveNext());
        }

        public T Current => _enumerator.Current;

        public ValueTask DisposeAsync()
        {
            _enumerator.Dispose();
            return new ValueTask();
        }
    }
}
