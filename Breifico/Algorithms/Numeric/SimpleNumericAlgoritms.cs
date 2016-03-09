namespace Breifico.Algorithms.Numeric
{
    /// <summary>
    /// Различные числовые алгоритмы
    /// </summary>
    public static class SimpleNumericAlgoritms
    {
        /// <summary>
        /// Поиск наименьшего общего делителя для 2 двух чисел
        /// </summary>
        /// <param name="a">Первое число</param>
        /// <param name="b">Второе число</param>
        /// <returns>Наименьший общий делитель указанных чисел</returns>
        public static int GcdFinder(int a, int b) {
            while (b != 0) {
                int rem = a % b;
                a = b;
                b = rem;
            }
            return a;
        }

        /// <summary>
        /// Перемешивает числа в массиве в случайном порядке
        /// </summary>
        /// <typeparam name="T">Тип данных в исходном массиве</typeparam>
        /// <param name="input">Исходный массив</param>
        public static void RandomizeArray<T>(T[] input) {
            var generator = new LinearCongruentialGenerator();
            int maxIndex = input.Length - 1;
            for (int i = 0; i < maxIndex; i++) {
                int newIndex = generator.Next(i, maxIndex);
                CommonHelpers.Swap(ref input[newIndex], ref input[i]);
            }
        }

        /// <summary>
        /// Возводит число в указанную степень
        /// </summary>
        /// <param name="number">Число, которое необходимо возвести в степень</param>
        /// <param name="exp">Экспонента</param>
        /// <returns>Число возведенное в указанную степень</returns>
        public static double ExpNumber(long number, long exp) {
            if (exp < 0) {
                return 1.0 / ExpNumber(number, -exp);
            }
            long result = 1;
            while (exp > 0) {
                if ((exp & 1) != 0) {
                    result *= number;
                }
                exp >>= 1;
                number *= number;
            }
            return result;
        }
    }
}
