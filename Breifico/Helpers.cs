﻿using System;

namespace Breifico
{
    public static class CommonHelpers
    {
        public static void Swap<T>(ref T lhs, ref T rhs) {
            var temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }

    public static class SortExtensions
    {
        public static T[] MySort<T>(this T[] input, ISorter<T> sorter) where T : IComparable<T> {
            return sorter.Sort(input);
        }

        public static uint[] MySort(this uint[] input, ISorter<uint> sorter) {
            return sorter.Sort(input);
        }
    }

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