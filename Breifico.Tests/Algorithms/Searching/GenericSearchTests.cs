using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Searching
{
    public abstract class GenericSearchTests
    {
        private readonly ISearcher<int> _searcher;

        protected GenericSearchTests(ISearcher<int> searcher)
        {
            this._searcher = searcher;
        }

        [TestMethod]
        public void Search_WhenEmpty_MinusOne()
        {
            this._searcher.Search(new int[] { }, 0).Should().Be(-1);
            this._searcher.Search(new int[] { }, 1).Should().Be(-1);
            this._searcher.Search(new int[] { }, -1).Should().Be(-1);
        }

        [TestMethod]
        public void Search_WhenOneElement_Index()
        {
            this._searcher.Search(new[] { 2 }, 2).Should().Be(0);
            this._searcher.Search(new[] { 2 }, 1).Should().Be(-1);
            this._searcher.Search(new[] { 2 }, 3).Should().Be(-1);
        }

        [TestMethod]
        public void Search_WhenMultipleElements_Index()
        {
            this._searcher.Search(new[] { 10, 20 }, 10).Should().Be(0);
            this._searcher.Search(new[] { 10, 20 }, 20).Should().Be(1);
            this._searcher.Search(new[] { 10, 20 }, 5).Should().Be(-1);
            this._searcher.Search(new[] { 10, 20 }, 15).Should().Be(-1);
            this._searcher.Search(new[] { 10, 20 }, 30).Should().Be(-1);

            this._searcher.Search(new[] { 10, 20, 30 }, 10).Should().Be(0);
            this._searcher.Search(new[] { 10, 20, 30 }, 20).Should().Be(1);
            this._searcher.Search(new[] { 10, 20, 30 }, 30).Should().Be(2);

            this._searcher.Search(new[] { 10, 20, 30, 40 }, 10).Should().Be(0);
            this._searcher.Search(new[] { 10, 20, 30, 40 }, 20).Should().Be(1);
            this._searcher.Search(new[] { 10, 20, 30, 40 }, 30).Should().Be(2);
            this._searcher.Search(new[] { 10, 20, 30, 40 }, 40).Should().Be(3);

            this._searcher.Search(new[] { 10, 20, 30, 40, 50 }, 32).Should().Be(-1);
            this._searcher.Search(new[] { 10, 20, 30, 40 }, 32).Should().Be(-1);
            this._searcher.Search(new[] { 10, 20, 30, 40, 50 }, 5).Should().Be(-1);
            this._searcher.Search(new[] { 10, 20, 30, 40 }, 5).Should().Be(-1);
            this._searcher.Search(new[] { 10, 20, 30, 40, 50 }, 120).Should().Be(-1);
            this._searcher.Search(new[] { 10, 20, 30, 40 }, 120).Should().Be(-1);

            this._searcher.Search(new[] { 10, 20, 30, 40, 50 }, 30).Should().Be(2);
            this._searcher.Search(new[] { 10, 20, 30, 40, 50, 60 }, 60).Should().Be(5);
        }
    }
}
