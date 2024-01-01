using System.Diagnostics;

namespace CoreToolset.ExtensionsTests.Helpers
{
    public class PerformanceTestHelper
    {
        public static (long averageTime, long maxTime) RunPerformanceTest(Action testAction, int numberOfRuns)
        {
            Stopwatch stopwatch = new();
            List<long> elapsedMillisecondsList = [];

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            for (int i = 0; i < numberOfRuns; i++)
            {
                stopwatch.Restart();
                testAction.Invoke();
                long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                elapsedMillisecondsList.Add(elapsedMilliseconds);
            }

            long averageTime = (long)elapsedMillisecondsList.Average();
            long maxTime = elapsedMillisecondsList.Max();

            return (averageTime, maxTime);
        }

        public static async Task<(long averageTime, long maxTime)> RunPerformanceTestAsync(Func<Task> testAction, int numberOfRuns)
        {
            Stopwatch stopwatch = new Stopwatch();
            List<long> elapsedMillisecondsList = new List<long>();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            for (int i = 0; i < numberOfRuns; i++)
            {
                stopwatch.Restart();
                await testAction.Invoke();
                long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                elapsedMillisecondsList.Add(elapsedMilliseconds);
            }

            long averageTime = (long)elapsedMillisecondsList.Average();
            long maxTime = elapsedMillisecondsList.Max();

            return (averageTime, maxTime);
        }
    }
}
