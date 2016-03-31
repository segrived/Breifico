using System;
using Breifico.DataStructures;

namespace Breifico.Algorithms.Numeric
{
    /// <summary>
    /// Предназначен для конвертации числа из одной системы счисления в другую
    /// </summary>
    public class NumberBaseConverter
    {
        public int NumberBase { get; }

        private readonly char[] _bases = {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B',
            'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
            'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        /// <summary>
        /// Создает и возвращает новый экземпляр <see cref="NumberBaseConverter"/> с
        /// указанной системой счисления
        /// </summary>
        /// <param name="numberBase"></param>
        public NumberBaseConverter(int numberBase) {
            if (numberBase < 2 || numberBase > this._bases.Length) {
                throw new ArgumentException($"numberBase should be in range 2..{this._bases.Length}");
            }
            this.NumberBase = numberBase;
        }

        /// <summary>
        /// Переводит число в систему счисления <see cref="NumberBase"/>
        /// </summary>
        /// <param name="number">Число, которое необходимо перевести  другую 
        /// систему счисления</param>
        /// <param name="padding">Минимальное число символов, все отсутствующие
        /// цифры будут заполнены нулями</param>
        /// <returns>Число в необходимой системе счисления</returns>
        public string ToBase(int number, int padding = 0) {

            var outStack = new MyStack<char>();
            while (number > 0) {
                int b = number % this.NumberBase;
                outStack.Push(this._bases[b]);
                number /= this.NumberBase;
            }
            return String.Join("", outStack).PadLeft(padding, '0');
        }
    }
}