using System;
using System.Collections.Generic;
using Breifico.DataStructures;
using Breifico.Functional;
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
        public void MyCount_Test() {
            var coll = new MyList<int> { 2, 4, 6 };
            coll.MyCount().Should().Be(3);

            var coll2 = new MyList<int> { 4 };
            coll2.MyCount().Should().Be(1);

            new MyList<int>().MyCount().Should().Be(0);

            this._nullEnum.Invoking(c => c.MyCount())
                .ShouldThrow<NullReferenceException>();
        }

        [TestMethod]
        public void MyContains_Test() {
            var coll = new MyList<int> { 2, 4, 6 };
            coll.MyContains(2).Should().BeTrue();
            coll.MyContains(3).Should().BeFalse();
            coll.MyContains(4).Should().BeTrue();
            coll.MyContains(5).Should().BeFalse();
            coll.MyContains(6).Should().BeTrue();
            coll.MyContains(7).Should().BeFalse();

            var coll2 = new MyList<int> { 4 };
            coll2.MyContains(4).Should().BeTrue();

            new MyList<int>().MyContains(0).Should().BeFalse();

            this._nullEnum.Invoking(c => c.MyContains(0))
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

        [TestMethod]
        public void MySkip_Test() {
            var coll = new MyList<int> { 2, 3, 2, 1, 5, 4 };
            coll.MySkip(1).Should().Equal(3, 2, 1, 5, 4);
            coll.MySkip(2).Should().Equal(2, 1, 5, 4);
            coll.MySkip(3).Should().Equal(1, 5, 4);
            coll.MySkip(4).Should().Equal(5, 4);
            coll.MySkip(5).Should().Equal(4);
            coll.MySkip(6).Should().BeEmpty();
            coll.MySkip(7).Should().BeEmpty();

            new MyList<int> { 10 }.MySkip(1).Should().BeEmpty();

            new MyList<int>().MySkip(1).Should().BeEmpty();

            this._nullEnum.Invoking(c => c.MySkip(0).MyFirst())
               .ShouldThrow<NullReferenceException>();
        }

        [TestMethod]
        public void MyTake_Test() {
            var coll = new MyList<int> { 2, 3, 2, 1, 5, 4 };
            coll.MyTake(0).Should().BeEmpty();
            coll.MyTake(1).Should().Equal(2);
            coll.MyTake(2).Should().Equal(2, 3);
            coll.MyTake(3).Should().Equal(2, 3, 2);
            coll.MyTake(4).Should().Equal(2, 3, 2, 1);
            coll.MyTake(5).Should().Equal(2, 3, 2, 1, 5);
            coll.MyTake(6).Should().Equal(2, 3, 2, 1, 5, 4);
            coll.MyTake(7).Should().Equal(2, 3, 2, 1, 5, 4);

            new MyList<int> { 10 }.MyTake(5).Should().Equal(10);

            new MyList<int>().MyTake(1).Should().BeEmpty();

            this._nullEnum.Invoking(c => c.MyTake(0).MyFirst())
               .ShouldThrow<NullReferenceException>();
        }

        [TestMethod]
        public void MyReverse_Test() {
            var coll = new MyList<int> { 2, 4, 6, 10 };
            coll.MyReverse().Should().Equal(10, 6, 4, 2);

            var coll2 = new MyList<int> { 2, 4 };
            coll2.MyReverse().Should().Equal(4, 2);

            new MyList<int>().MyReverse().Should().BeEmpty();

            this._nullEnum.Invoking(c => c.MyReverse().MyFirst())
                .ShouldThrow<NullReferenceException>();
        }

        [TestMethod]
        public void MyToArray_Test() {
            var coll = new MyList<int> { 2, 4, 6 };
            var arr = coll.MyToArray();
            arr[0].Should().Be(2);
            arr[1].Should().Be(4);
            arr[2].Should().Be(6);
            arr.Length.Should().Be(3);

            var coll2 = new MyList<int> { 2 };
            var arr2 = coll2.MyToArray();
            arr2[0].Should().Be(2);
            arr2.Length.Should().Be(1);

            new MyList<int>().MyToArray().Should().BeEmpty();

            this._nullEnum.Invoking(c => c.MyToArray())
                .ShouldThrow<NullReferenceException>();
        }

        [TestMethod]
        public void MyAggregate_Test() {
            new MyList<int> { 2, 4, 6 }.MyAggregate((a, b) => a + b, 0).Should().Be(12);
            new MyList<int> { 2, 4 }.MyAggregate((a, b) => a * b, 1).Should().Be(8);
            new MyList<int> { 2, 4 }.MyAggregate((a, b) => a + b.ToString(), "").Should().Be("24");
            new MyList<int>().MyAggregate((a, b) => a + b, 10).Should().Be(10);

            this._nullEnum.Invoking(c => c.MyAggregate((a, b) => a + b, 0))
                .ShouldThrow<NullReferenceException>();
        }

        [TestMethod]
        public void MySequenceEqual_Test() {
            var c1 = new MyList<int> {2, 4, 6, 7, 11};
            var c2 = new MyList<int> { 2, 4, 6, 7, 11 };
            var c3 = new MyList<int> { 2, 4, 6, 7 };
            var c4 = new MyList<int> { 2 };
            var c5 = new MyList<int> {2, 4, 6, 8, 11};
            var c6 = new MyList<int> { 2, 4, 6, 7, 12 };

            c1.MySequenceEqual(c2).Should().BeTrue();
            c1.MySequenceEqual(c3).Should().BeFalse();
            c3.MySequenceEqual(c1).Should().BeFalse();
            new MyList<int>().MySequenceEqual(new MyList<int>()).Should().BeTrue();
            c4.MySequenceEqual(new MyList<int>()).Should().BeFalse();
            c1.MySequenceEqual(c5).Should().BeFalse();
            c1.MySequenceEqual(c6).Should().BeFalse();

            this._nullEnum.Invoking(c => c.MySequenceEqual(new MyList<int>()))
                .ShouldThrow<NullReferenceException>();
        }
    }
}