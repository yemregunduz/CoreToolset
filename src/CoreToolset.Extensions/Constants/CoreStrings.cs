
namespace CoreToolset.Extensions.Constants
{
    public class CoreStrings
    {
        public static string? IQueryableNotAsync(Type type)
            => $"The source IQueryable doesn't implement IAsyncEnumerable<{type}>. Only sources that implement IAsyncEnumerable can be used for asynchronous operations.";

        public static string TypeMustBeEnum(Type type)
            => $"The type {type} must be an enum.";
    }
}
