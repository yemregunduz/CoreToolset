namespace CoreToolset.Extensions.ValidationHelpers
{
    public class PaginationValidationHelper
    {
        public static void ValidateParameters(int index, int size, int from)
        {
            if (from > index)
                throw new ArgumentException($"from: {from} > index: {index}, must from <= index");

            if (index < 0 || size < 0 || from < 0) 
                throw new ArgumentException($"From: {from}, index: {index} and size: {size} must be greater than 0.");
        }
    }
}
