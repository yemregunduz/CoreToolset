using CoreToolset.Extensions.Models.Pagination;
using CoreToolset.ExtensionsTests.Helpers;

namespace CoreToolset.ExtensionsTests
{
    [TestFixture]
    public class QueryableExtensionsTests
    {

        #region ToPaginate Tests
        [Test]
        public void ToPaginate_WithNegativeIndex_ThrowsArgumentException()
        {
            IQueryable<int> source = Enumerable.Range(1, 100).AsQueryable();
            int index = -1;
            int size = 10;

            Assert.Throws<ArgumentException>(() => source.ToPaginate(index, size, -2));
        }

        [Test]
        public void ToPaginate_WithNegativeSize_ThrowsArgumentException()
        {
            IQueryable<int> source = Enumerable.Range(1, 100).AsQueryable();
            int index = 2;
            int size = -10;

            Assert.Throws<ArgumentException>(() => source.ToPaginate(index, size));
        }

        [Test]
        public void ToPaginate_WithNegativeFrom_ThrowsArgumentException()
        {
            IQueryable<int> source = Enumerable.Range(1, 100).AsQueryable();
            int index = 2;
            int size = 10;
            int from = -1;

            Assert.Throws<ArgumentException>(() => source.ToPaginate(index, size, from));
        }

        [Test]
        public void ToPaginate_WithFromGreaterThanIndex_ThrowsArgumentException()
        {
            IQueryable<int> source = Enumerable.Range(1, 100).AsQueryable();
            int index = 2;
            int size = 10;
            int from = 3;

            Assert.Throws<ArgumentException>(() => source.ToPaginate(index, size, from));
        }

        [Test]
        public void ToPaginate_WithZeroSize_ReturnsEmptyEnumerable()
        {
            IQueryable<int> source = Enumerable.Range(1, 100).AsQueryable();
            int index = 2;
            int size = 0;

            var result = source.ToPaginate(index, size);
            Assert.That(result.Items, Is.Empty);
        }


        [Test]
        public void ToPaginate_TotalCountCalculatedCorrectly()
        {
            IQueryable<int> source = Enumerable.Range(1, 100).AsQueryable();
            int index = 2;
            int size = 10;

            var result = source.ToPaginate(index, size);

            Assert.That(result, Has.Count.EqualTo(100));
        }

        [Test]
        public void ToPaginate_ItemsCountCalculatedCorrectly()
        {
            IQueryable<int> source = Enumerable.Range(1, 100).AsQueryable();
            int index = 2;
            int size = 10;

            var result = source.ToPaginate(index, size);

            Assert.That(result.Items, Has.Count.EqualTo(10));
        }

        [Test]
        public void ToPaginate_PagesCalculatedCorrectly()
        {
            IQueryable<int> source = Enumerable.Range(1, 100).AsQueryable();
            int index = 2;
            int size = 10;

            var result = source.ToPaginate(index, size);

            Assert.That(result.Pages, Is.EqualTo(10));
        }

        [Test]
        public void ToPaginate_WithEmptySource_ReturnsPaginateObjectWithZeroCount()
        {
            IQueryable<int> source = Enumerable.Empty<int>().AsQueryable();
            int index = 2;
            int size = 10;

            var result = source.ToPaginate(index, size);

            Assert.Multiple(() =>
            {
                Assert.That(result.Items, Is.Empty);
                Assert.That(result, Has.Count.EqualTo(0));
            });
        }

        [Test]
        public void ToPaginate_WithValidParameters_ReturnsPaginateObject()
        {
            IQueryable<int> source = Enumerable.Range(1, 100).AsQueryable();
            int index = 2;
            int size = 10;
            int from = 1;

            var result = source.ToPaginate(index, size, from);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<IPaginate<int>>());
                Assert.That(result.Index, Is.EqualTo(index));
                Assert.That(result.Size, Is.EqualTo(size));
                Assert.That(result.From, Is.EqualTo(from));
                Assert.That(result.Count, Is.EqualTo(100));
                Assert.That(result.Items, Is.EqualTo(Enumerable.Range(11, 10)));
                Assert.That(result.Items, Has.Count.EqualTo(size));
                Assert.That(result.Pages, Is.EqualTo(10));
                Assert.That(result.HasPrevious, Is.True);
                Assert.That(result.HasNext, Is.True);
            });
        }

        [Test]
        public void ToPaginate_PerformanceTest()
        {
            IQueryable<int> source = Enumerable.Range(1, int.MaxValue - 56).AsQueryable();
            int index = 500000;
            int size = 100;

            int numberOfRuns = 100;

            var (averageTime, maxTime) = PerformanceTestHelper.RunPerformanceTest(() =>
            {
                var result = source.ToPaginate(index, size);
            }, numberOfRuns);

            Assert.Multiple(() =>
            {
                Assert.That(averageTime, Is.LessThan(70));
                Assert.That(maxTime, Is.LessThan(100));
            });
        }
        #endregion
    }
}
