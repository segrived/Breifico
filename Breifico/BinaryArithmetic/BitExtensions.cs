using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Breifico.BinaryArithmetic.GenericOps;

namespace Breifico.BinaryArithmetic
{
    public static class BitExtensions
    {
        public static Num<T> AddOne<T>(this Num<T> n) => n.Add(n.One);
        public static Num<T> SubOne<T>(this Num<T> n) => n.Sub(n.One);

        /// <summary>
        /// "Выключает" последний единичный бит в числе
        /// <para />
        /// Например: <c>01011000 ⇒ 01010000</c>
        /// </summary>
        public static Num<T> TurnOffRightmostOne<T>(Num<T> b) 
            => b.And(b.SubOne());

        /// <summary>
        /// "Выключает" все конечные единичные биты в конце числа
        /// <para />
        /// Например: <c>10100111 ⇒ 10100000</c>
        /// </summary>
        public static Num<T> TurnOffRightmostTrailingOnes<T>(Num<T> b) 
            => b.Add(b.AddOne());

        /// <summary>
        /// "Включает" последний нулевой бит в числа
        /// <para />
        /// Например: <c>10100111 ⇒ 10101111</c>
        /// </summary>
        public static Num<T> TurnOnRigthmostZero<T>(Num<T> b) 
            => b.Or(b.AddOne());

        /// <summary>
        /// "Включает" все конечные нулевые биты в конце числа
        /// <para />
        /// Например: <c>10101000 ⇒ 10101111</c>
        /// </summary>
        public static Num<T> TurnOnRightmostTrailingZeros<T>(Num<T> b) 
            => b.Or(b.SubOne());

        /// <summary>
        /// Выделяет правый крайний единичный бит
        /// <para />
        /// Например <c>01011000 ⇒ 00001000</c>
        /// </summary>
        public static Num<T> IsolateRightmostOneBit<T>(Num<T> b)
            => b.Neg().And(b);

        public static T SumElements<T>(this IEnumerable<Num<T>> input) {
            IEnumerable<Num<T>> enumerable = input as Num<T>[] ?? input.ToArray();
            if (!enumerable.Any()) {
                return default(T);
            }
            var first = enumerable.First();
            first = enumerable.Skip(1).Aggregate(first, (current, x) => current.Add(x));
            return first.Value;
        }
    }
}