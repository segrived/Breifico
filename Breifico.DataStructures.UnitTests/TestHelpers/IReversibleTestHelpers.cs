using Breifico.DataStructures.Interfaces;
using FluentAssertions;

namespace Breifico.DataStructures.UnitTests.TestHelpers
{
    public static class IReversibleTestHelpers
    {
        public static void Reverse_ShouldReverseList(IReversible<int> rList) {
            rList.AddRange(10, 20, 30);

            rList.Reverse();
            rList.Should().Equal(30, 20, 10);

            rList.Reverse(); // reverse back
            rList.Should().Equal(10, 20, 30);

            rList.Add(40);
            rList.Reverse();
            rList.Should().Equal(40, 30, 20, 10);
        }

        public static void Reverse_ShouldCorrectlyProcessEmptyList(IReversible<int> rList) {
            rList.Reverse();
            rList.Should().BeEmpty();
        }
    }
}
