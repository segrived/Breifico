using System;
using System.Collections.Generic;
using Breifico.DataStructures;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests
{
    [TestClass]
    public class MyLinqTests
    {
        private readonly IEnumerable<int> _nullEnum = null;

        [TestMethod]
        public void MyAll_Test() {
            var coll = new MyList<int> {2, 4, 6};
            coll.MyAll(x => x % 2 == 0).Should().BeTrue();
            coll.MyAll(x => x % 2 == 1).Should().BeFalse();
            coll.MyAll(x => x > 0).Should().BeTrue();
            coll.MyAll(x => x > 2).Should().BeFalse();
            coll.MyAll(x => x == 1).Should().BeFalse();
            coll.MyAll(x => x == 2).Should().BeFalse();

            new MyList<int>().MyAll(x => x == 0).Should().BeTrue();

            this._nullEnum.Invoking(c => c.MyAll(x => x > 0))
                .ShouldThrow<NullReferenceException>();
        }

        [TestMethod]
        public void MyAny_WithPredicate_Test() {
            var coll = new MyList<int> {2, 4, 6};
            coll.MyAny(x => x % 2 == 0).Should().BeTrue();
            coll.MyAny(x => x % 2 == 1).Should().BeFalse();
            coll.MyAny(x => x > 0).Should().BeTrue();
            coll.MyAny(x => x > 2).Should().BeTrue();
            coll.MyAny(x => x == 1).Should().BeFalse();
            coll.MyAny(x => x == 2).Should().BeTrue();

            new MyList<int>().MyAny(x => x == 0).Should().BeFalse();

            this._nullEnum.Invoking(c => c.MyAny(x => x > 0))
                .ShouldThrow<NullReferenceException>();
        }

        [TestMethod]
        public void MyAny_WithoutPredicate_Test() {
            var coll = new MyList<int> {2};
            coll.MyAny().Should().BeTrue();

            var coll2 = new MyList<int> {2, 4, 6};
            coll2.MyAny().Should().BeTrue();

            new MyList<int>().MyAny().Should().BeFalse();

            this._nullEnum.Invoking(c => c.MyAny())
                .ShouldThrow<NullReferenceException>();
        }

        [TestMethod]
        public void MyMin_Test() {
            var coll = new MyList<int> { 2, 3, 2, 1, 5, 4 };
            coll.MyMin().Should().Be(1);

            var coll2 = new MyList<int> { 2, 3, 2 };
            coll2.MyMin().Should().Be(2);

            var coll3 = new MyList<int> {10};
            coll3.MyMin().Should().Be(10);

            var coll4 = new MyList<int>();
            coll4.Invoking(c => c.MyMin())
                .ShouldThrow<InvalidOperationException>();

            this._nullEnum.Invoking(c => c.MyMin())
               .ShouldThrow<NullReferenceException>();
        }

        [TestMethod]
        public void MyMax_Test() {
            var coll = new MyList<int> { 2, 3, 2, 1, 5, 4 };
            coll.MyMax().Should().Be(5);

            var coll2 = new MyList<int> { 2, 3, 2 };
            coll2.MyMax().Should().Be(3);

            var coll3 = new MyList<int> { 10 };
            coll3.MyMax().Should().Be(10);

            var coll4 = new MyList<int>();
            coll4.Invoking(c => c.MyMax())
                .ShouldThrow<InvalidOperationException>();

            this._nullEnum.Invoking(c => c.MyMax())
               .ShouldThrow<NullReferenceException>();
        }

        [TestMethod]
        public void MyFirst_Test() {
            var coll = new MyList<int> {2, 4, 6};
            coll.MyFirst().Should().Be(2);

            var coll2 = new MyList<int> {4};
            coll2.MyFirst().Should().Be(4);

            var coll3 = new MyList<int>();
            coll3.Invoking(c => c.MyFirst())
                 .ShouldThrow<InvalidOperationException>();

            this._nullEnum.Invoking(c => c.MyFirst())
                .ShouldThrow<NullReferenceException>();
        }

        [TestMethod]
        public void MyFirstOrDefault_Test() {
            var coll = new MyList<int> { 2, 4, 6 };
            coll.MyFirstOrDefault(10).Should().Be(2);

            var coll2 = new MyList<int> { 4 };
            coll2.MyFirstOrDefault(10).Should().Be(4);

            new MyList<int>().MyFirstOrDefault(10).Should().Be(10);
            new MyList<int>().MyFirstOrDefault().Should().Be(default(int));

            this._nullEnum.Invoking(c => c.MyFirstOrDefault())
                .ShouldThrow<NullReferenceException>();
        }

        [TestMethod]
        public void MyLast_Test() {
            var coll = new MyList<int> { 2, 4, 6 };
            coll.MyLast().Should().Be(6);

            var coll2 = new MyList<int> { 4 };
            coll2.MyLast().Should().Be(4);

            var coll3 = new MyList<int>();
            coll3.Invoking(c => c.MyLast())
                 .ShouldThrow<InvalidOperationException>();

            this._nullEnum.Invoking(c => c.MyLast())
                .ShouldThrow<NullReferenceException>();
        }

        [TestMethod]
        public void MyLastOrDefault_Test() {
            var coll = new MyList<int> { 2, 4, 6 };
            coll.MyLastOrDefault(10).Should().Be(6);

            var coll2 = new MyList<int> { 4 };
            coll2.MyLastOrDefault(10).Should().Be(4);

            new MyList<int>().MyLastOrDefault(10).Should().Be(10);
            new MyList<int>().MyLastOrDefault().Should().Be(default(int));

            this._nullEnum.Invoking(c => c.MyLastOrDefault())
                .ShouldThrow<NullReferenceException>();
        }

        [TestMethod]
        public void Count_Test() {
            var coll = new MyList<int> { 2, 4, 6 };
            coll.Count().Should().Be(3);

            var coll2 = new MyList<int> { 4 };
            coll2.Count().Should().Be(1);

            new MyList<int>().Count().Should().Be(0);

            this._nullEnum.Invoking(c => c.Count())
                .ShouldThrow<NullReferenceException>();
        }

        [TestMethod]
        public void MyWhere_Test() {
            var coll = new MyList<int> { 2, 4, 6, 7, 11, 12 };
            coll.MyWhere(x => x % 2 == 0).Should().Equal(2, 4, 6, 12);
            coll.MyWhere(x => x % 2 == 1).Should().Equal(7, 11);
            coll.MyWhere(x => x > 5).Should().Equal(6, 7, 11, 12);
            coll.MyWhere(x => x != 6).Should().Equal(2, 4, 7, 11, 12);

            new MyList<int>().MyWhere(x => x == 0).Should().BeEmpty();

            this._nullEnum.Invoking(c => c.MyWhere(x => x > 0).MyFirst())
                .ShouldThrow<NullReferenceException>();
        }
    }
}