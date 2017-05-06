namespace Breifico.Algorithms
{
    public static class BitArithmetic
    {
        /// <summary>
        /// "Выключает" последний единичный бит в числе
        /// <para />
        /// Например: <c>01011000 ⇒ 01010000</c>
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="b">Исходное значение</param>
        /// <returns>Результат выполнения операции</returns>
        public static T TurnOffRightmostOne<T>(dynamic b) => unchecked((T)(b & (T)(b - 1)));

        /// <summary>
        /// "Выключает" все конечные единичные биты в конце числа
        /// <para />
        /// Например: <c>10100111 ⇒ 10100000</c>
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="b">Исходное значение</param>
        /// <returns>Результат выполнения операции</returns>
        public static T TurnOffRightmostTrailingOnes<T>(dynamic b) => unchecked((T)(b & (T)(b + 1)));

        /// <summary>
        /// "Включает" последний нулевой бит в числа
        /// <para />
        /// Например: <c>10100111 ⇒ 10101111</c>
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="b">Исходное значение</param>
        /// <returns>Результат выполнения операции</returns>
        public static T TurnOnRigthmostZero<T>(dynamic b) => unchecked((T)(b | (T)(b + 1)));

        /// <summary>
        /// "Включает" все конечные нулевые биты в конце числа
        /// <para />
        /// Например: <c>10101000 ⇒ 10101111</c>
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="b">Исходное значение</param>
        /// <returns>Результат выполнения операции</returns>
        public static T TurnOnRightmostTrailingZeros<T>(dynamic b) => unchecked((T)(b | (T)(b - 1)));

        /// <summary>
        /// Выделяет правый крайний единичный бит
        /// <para />
        /// Например <c>01011000 ⇒ 00001000</c>
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="b">Исходное значение</param>
        /// <returns>Результат выполнения операции</returns>
        public static T IsolateRightmostOneBit<T>(dynamic b) => unchecked((T)(b & (T)(-b)));
    }
}