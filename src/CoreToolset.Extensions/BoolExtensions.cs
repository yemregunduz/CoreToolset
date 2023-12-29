namespace CoreToolset.Extensions
{
    public static class BoolExtensions
    {
        public static bool IsFalse(this bool value) =>
            !value;

        public static bool IsTrue(this bool value) =>
            value;
    }
}
