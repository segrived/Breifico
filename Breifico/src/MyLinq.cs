using System;
using System.Collections.Generic;

// ReSharper disable LoopCanBeConvertedToQuery
namespace Breifico
{
    public static class MyLinq
    {
        /// <summary>
        /// Проверяет, чтобы все элементы коллекции соответствовали указанному предикату
        /// </summary>
        /// <typeparam name="T">Тип элементов в коллекции</typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <param name="predicate">Предикат</param>
        /// <returns>True - если все элементы соответствуют указанному предикату, иначе False</returns>
        /// <exception cref="NullReferenceException">Если в качестве исходной коллекции был передан null</exception>
        public static bool MyAll<T>(this IEnumerable<T> input, Func<T, bool> predicate) {
            if (input == null) {
                throw new NullReferenceException();
            }
            foreach (var item in input) {
                if (!predicate(item)) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Проверяет наличие хотя бы одного элемента в коллекции
        /// </summary>
        /// <typeparam name="T">Тип элементов в коллекции</typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <returns>True если в коллекции есть хоть один элемент, иначе False</returns>
        /// <exception cref="NullReferenceException">Если в качестве исходной коллекции был передан null</exception>
        public static bool MyAny<T>(this IEnumerable<T> input) {
            if (input == null) {
                throw new NullReferenceException();
            }
            using (var enumerator = input.GetEnumerator()) {
                return enumerator.MoveNext();
            }
        }
        /// <summary>
        /// Проверяет, чтобы хотя бы один из элементов коллекции соответствовал указанному предикату
        /// </summary>
        /// <typeparam name="T">Тип элементов в коллекции</typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <param name="predicate">Предикат</param>
        /// <returns>True - если хотя бы один элемент соответствует предикату, иначе False</returns>
        /// <exception cref="NullReferenceException">Если в качестве исходной коллекции был передан null</exception>
        public static bool MyAny<T>(this IEnumerable<T> input, Func<T, bool> predicate) {
            if (input == null) {
                throw new NullReferenceException();
            }
            foreach (var item in input) {
                if (predicate(item)) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Возвращает первый элемент коллекции
        /// </summary>
        /// <typeparam name="T">Тип элементов в коллекции</typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <returns>Первый элемент исходной коллекции</returns>
        public static T MyFirst<T>(this IEnumerable<T> input) {
            if (input == null) {
                throw new NullReferenceException();
            }
            using (var enumerator = input.GetEnumerator()) {
                if (!enumerator.MoveNext()) {
                    throw new InvalidOperationException("Empty collection");
                }
                return enumerator.Current;
            }
        }

        /// <summary>
        /// Возвращает первый элемент коллекции, либо, если коллекция пуста, указанный элемент по умолчанию
        /// </summary>
        /// <typeparam name="T">Тип элементов в коллекции</typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <param name="defaultValue">Значение по умолчанию, если данный параметр не указан, то будет возвращено 
        /// значение по умолчанию для типа <see cref="T"/></param>
        /// <returns>Первый элемент исходной коллекции</returns>
        public static T MyFirstOrDefault<T>(this IEnumerable<T> input, T defaultValue = default(T)) {
            if (input == null) {
                throw new NullReferenceException();
            }
            using (var enumerator = input.GetEnumerator()) {
                if (!enumerator.MoveNext()) {
                    return defaultValue;
                }
                return enumerator.Current;
            }
        }

        public static T MyLast<T>(this IEnumerable<T> input) {
            if (input == null) {
                throw new NullReferenceException();
            }
            using (var enumerator = input.GetEnumerator()) {
                if (!enumerator.MoveNext()) {
                    throw new InvalidOperationException("Empty collection");
                }
                while (enumerator.MoveNext()) {}
                return enumerator.Current;
            }
        }

        /// <summary>
        /// Возвращает последний элемент коллекции, либо, если коллекция пуста, указанный элемент по умолчанию
        /// </summary>
        /// <typeparam name="T">Тип элементов в коллекции</typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <param name="defaultValue">Значение по умолчанию, если данный параметр не указан, то будет возвращено 
        /// значение по умолчанию для типа <see cref="T"/></param>
        /// <returns>Последний элемент исходной коллекции</returns>
        public static T MyLastOrDefault<T>(this IEnumerable<T> input, T defaultValue = default(T)) {
            if (input == null) {
                throw new NullReferenceException();
            }
            using (var enumerator = input.GetEnumerator()) {
                if (!enumerator.MoveNext()) {
                    return defaultValue;
                }
                while (enumerator.MoveNext()) { }
                return enumerator.Current;
            }
        }

        /// <summary>
        /// Применяет указанную функцию к каждому элементу
        /// </summary>
        /// <typeparam name="T">Тип элементов в исходной коллекции</typeparam>
        /// <typeparam name="TR">Тип элементов в возвращаемой коллекции</typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <param name="f">Функция, трансформирующая каждый элемент типа <see cref="T"/> в <see cref="TR"/></param>
        /// <returns></returns>
        public static IEnumerable<TR> MySelect<T, TR>(this IEnumerable<T> input, Func<T, TR> f) {
            if (input == null) {
                throw new NullReferenceException();
            }
            foreach (var item in input) {
                yield return f(item);
            }
        }

        /// <summary>
        /// Возвращает минимальный элемент коллекции
        /// </summary>
        /// <typeparam name="T">Тип элементов в коллекции, должен имплементировать 
        /// интерфейс <see cref="IComparable{T}"/></typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <returns>Минимальный элемент в коллекции</returns>
        /// <exception cref="InvalidOperationException">В случае, если коллекция пустая</exception>
        /// <exception cref="NullReferenceException">Если в качестве исходной коллекции был передан null</exception>
        public static T MyMin<T>(this IEnumerable<T> input) where T : IComparable<T> {
            if (input == null) {
                throw new NullReferenceException();
            }
            using (var enumerator = input.GetEnumerator()) {
                if (! enumerator.MoveNext()) {
                    throw new InvalidOperationException("Empty collection");
                }
                var minElement = enumerator.Current;
                while (enumerator.MoveNext()) {
                    var currentElement = enumerator.Current;
                    if (currentElement.CompareTo(minElement) < 0) {
                        minElement = currentElement;
                    }
                }
                return minElement;
            }
        }

        /// <summary>
        /// Возвращает количество элементов в коллекции
        /// </summary>
        /// <typeparam name="T">Тип элементов в коллекции</typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <returns>Количество элементов в коллекции</returns>
        public static int Count<T>(this IEnumerable<T> input) {
            if (input == null) {
                throw new NullReferenceException();
            }
            int count = 0;
            using (var enumerator = input.GetEnumerator()) {
                while (enumerator.MoveNext()) {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Возвращает максимальный элемент коллекции
        /// </summary>
        /// <typeparam name="T">Тип элементов в коллекции, должен имплементировать 
        /// интерфейс <see cref="IComparable{T}"/></typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <returns>Максимальный элемент в коллекции</returns>
        /// <exception cref="InvalidOperationException">В случае, если коллекция пустая</exception>
        /// <exception cref="NullReferenceException">Если в качестве исходной коллекции был передан null</exception>
        public static T MyMax<T>(this IEnumerable<T> input) where T : IComparable<T> {
            if (input == null) {
                throw new NullReferenceException();
            }
            using (var enumerator = input.GetEnumerator()) {
                if (!enumerator.MoveNext()) {
                    throw new InvalidOperationException("Empty collection");
                }
                var maxElement = enumerator.Current;
                while (enumerator.MoveNext()) {
                    var currentElement = enumerator.Current;
                    if (currentElement.CompareTo(maxElement) > 0) {
                        maxElement = currentElement;
                    }
                }
                return maxElement;
            }
        }

        public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> input, Func<T, bool> predicate) {
            if (input == null) {
                throw new NullReferenceException();
            }
            foreach (var item in input) {
                if (predicate(item)) {
                    yield return item;
                }
            }
        }

        // TODO list:
        // Aggregate
        // Ancestors
        // Cast
        // Concat
        // Contains
        // CopyToDataTable
        // DefaultIfEmpty
        // DescendantNodes
        // Descendants
        // Distinct
        // ElementAt
        // ElementAtOrDefault
        // Elements
        // Except
        // FirstOrDefault
        // GroupBy
        // GroupJoin
        // InDocumentOrder
        // Intersect
        // Join
        // Nodes
        // OfType
        // OrderBy
        // OrderByDescending
        // Remove
        // Reverse
        // SelectMany
        // SequenceEqual
        // Single
        // SingleOrDefault
        // Skip
        // SkipWhile
        // Take
        // TakeWhile
        // ToArray
        // ToDictionary
        // ToList
        // ToLookup
        // Union
        // Zip

        // TODO Numeric
        // Sum
        // Average

    }
}