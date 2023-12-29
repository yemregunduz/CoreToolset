namespace CoreToolset.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNotNull(this object value) =>
          value is not null;

        public static bool IsNull(this object value) =>
            value is null;

        public static bool Is(this object value, Func<bool> func) =>
            value.IsNotNull() && func();

        public static bool IsNot(this object value, Func<bool> func) =>
            value.IsNull() && !func();
    }
}
