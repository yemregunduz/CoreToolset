using System.Diagnostics;

namespace CoreToolset.ExtensionsTests.Helpers
{
    internal class PerformanceTestHelper
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
    }
}
