using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breifico.Tests
{
    public static class TestHelperExtensions
    {
        public static bool AreEqualApproximately(this double v1, double v2, double epsilon) {
            return Math.Abs(v1 - v2) <= epsilon;
        }

        public static void DoNothing<T>(this T item) {
            return;
        }
    }
}
