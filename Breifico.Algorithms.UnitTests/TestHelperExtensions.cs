using System;

namespace Breifico.Algorithms.UnitTests
{
    public static class TestHelperExtensions
    {
        public static bool AreEqualApproximately(this double v1, double v2, double epsilon) {
            return Math.Abs(v1 - v2) <= epsilon;
        }
    }
}
