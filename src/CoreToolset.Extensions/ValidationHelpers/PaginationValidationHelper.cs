namespace CoreToolset.Extensions.ValidationHelpers
{
    public class PaginationValidationHelper
    {
        public static void ValidateParameters(int index, int size, int from)
        {
            if (from > index)
                throw new ArgumentException($"from: {from} > index: {index}, must from <= index");

            ArgumentOutOfRangeException.ThrowIfNegative(from, nameof(from));
            ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));
            ArgumentOutOfRangeException.ThrowIfNegative(size, nameof(size));
        }
    }
}
