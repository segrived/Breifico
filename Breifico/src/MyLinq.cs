using System;
using System.Collections.Generic;
using Breifico.DataStructures;

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
            using (IEnumerator<T> enumerator = input.GetEnumerator()) {
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
        /// <exception cref="InvalidOperationException">В случае, если коллекция пустая</exception>
        /// <exception cref="NullReferenceException">Если в качестве исходной коллекции был передан null</exception>
        public static T MyFirst<T>(this IEnumerable<T> input) {
            if (input == null) {
                throw new NullReferenceException();
            }
            using (IEnumerator<T> enumerator = input.GetEnumerator()) {
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
        /// <param name="defaultValue">
        /// Значение по умолчанию, если данный параметр не указан, то будет возвращено
        /// значение по умолчанию для типа <see cref="T" />
        /// </param>
        /// <returns>Первый элемент исходной коллекции</returns>
        /// <exception cref="NullReferenceException">Если в качестве исходной коллекции был передан null</exception>
        public static T MyFirstOrDefault<T>(this IEnumerable<T> input, T defaultValue = default(T)) {
            if (input == null) {
                throw new NullReferenceException();
            }
            using (IEnumerator<T> enumerator = input.GetEnumerator()) {
                if (!enumerator.MoveNext()) {
                    return defaultValue;
                }
                return enumerator.Current;
            }
        }

        /// <summary>
        /// Возвращает последний элемент коллекции
        /// </summary>
        /// <typeparam name="T">Тип элементов в коллекции</typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <returns>Последний элемент исходной коллекции</returns>
        /// <exception cref="InvalidOperationException">В случае, если коллекция пустая</exception>
        /// <exception cref="NullReferenceException">Если в качестве исходной коллекции был передан null</exception>
        public static T MyLast<T>(this IEnumerable<T> input) {
            if (input == null) {
                throw new NullReferenceException();
            }
            using (IEnumerator<T> enumerator = input.GetEnumerator()) {
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
        /// <param name="defaultValue">
        /// Значение по умолчанию, если данный параметр не указан, то будет возвращено
        /// значение по умолчанию для типа <see cref="T" />
        /// </param>
        /// <returns>Последний элемент исходной коллекции</returns>
        /// <exception cref="NullReferenceException">Если в качестве исходной коллекции был передан null</exception>
        public static T MyLastOrDefault<T>(this IEnumerable<T> input, T defaultValue = default(T)) {
            if (input == null) {
                throw new NullReferenceException();
            }
            using (IEnumerator<T> enumerator = input.GetEnumerator()) {
                if (!enumerator.MoveNext()) {
                    return defaultValue;
                }
                while (enumerator.MoveNext()) {}
                return enumerator.Current;
            }
        }

        /// <summary>
        /// Применяет указанную функцию к каждому элементу и возвращает новый перечислитель
        /// </summary>
        /// <typeparam name="T">Тип элементов в исходной коллекции</typeparam>
        /// <typeparam name="TR">Тип элементов в возвращаемой коллекции</typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <param name="f">Функция, трансформирующая каждый элемент типа <see cref="T" /> в <see cref="TR" /></param>
        /// <returns>TODO </returns>
        /// <exception cref="NullReferenceException">Если в качестве исходной коллекции был передан null</exception>
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
        /// <typeparam name="T">
        /// Тип элементов в коллекции, должен имплементировать
        /// интерфейс <see cref="IComparable{T}" />
        /// </typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <returns>Минимальный элемент в коллекции</returns>
        /// <exception cref="InvalidOperationException">В случае, если коллекция пустая</exception>
        /// <exception cref="NullReferenceException">Если в качестве исходной коллекции был передан null</exception>
        public static T MyMin<T>(this IEnumerable<T> input) where T : IComparable<T> {
            if (input == null) {
                throw new NullReferenceException();
            }
            using (IEnumerator<T> enumerator = input.GetEnumerator()) {
                if (!enumerator.MoveNext()) {
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
        /// <exception cref="NullReferenceException">Если в качестве исходной коллекции был передан null</exception>
        public static int MyCount<T>(this IEnumerable<T> input) {
            if (input == null) {
                throw new NullReferenceException();
            }
            int count = 0;
            using (IEnumerator<T> enumerator = input.GetEnumerator()) {
                while (enumerator.MoveNext()) {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Возвращает максимальный элемент коллекции
        /// </summary>
        /// <typeparam name="T">
        /// Тип элементов в коллекции, должен имплементировать
        /// интерфейс <see cref="IComparable{T}" />
        /// </typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <returns>Максимальный элемент в коллекции</returns>
        /// <exception cref="InvalidOperationException">В случае, если коллекция пустая</exception>
        /// <exception cref="NullReferenceException">Если в качестве исходной коллекции был передан null</exception>
        public static T MyMax<T>(this IEnumerable<T> input) where T : IComparable<T> {
            if (input == null) {
                throw new NullReferenceException();
            }
            using (IEnumerator<T> enumerator = input.GetEnumerator()) {
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

        /// <summary>
        /// Возвращает новый экземпляр <see cref="IEnumerable{T}" />, фильтруя элементы по
        /// указанному предикату
        /// </summary>
        /// <typeparam name="T">Тип элементов в коллекции</typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <param name="predicate">Предикат, с которым сравнивается каждый элемент коллекции</param>
        /// <returns>Новый экземпляр <see cref="IEnumerable{T}" />, итерирующий по отфильтрованным элементам</returns>
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

        /// <summary>
        /// Возвращает новый <see cref="IEnumerable{T}" />, итерирующий по элементам коллекции,
        /// пропуская первые <see cref="count" /> элементов исходной коллекции
        /// </summary>
        /// <typeparam name="T">Тип элементов в коллекции</typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <param name="count">Число пропускаемых элементов</param>
        /// <returns><see cref="IEnumerable{T}" />, итерирующий по элементам коллеции</returns>
        /// <exception cref="NullReferenceException">Если в качестве исходной коллекции был передан null</exception>
        public static IEnumerable<T> MySkip<T>(this IEnumerable<T> input, int count) {
            if (input == null) {
                throw new NullReferenceException();
            }
            using (IEnumerator<T> enumerator = input.GetEnumerator()) {
                for (int i = 0; i < count; i++) {
                    if (!enumerator.MoveNext()) {
                        yield break;
                    }
                }
                while (enumerator.MoveNext()) {
                    yield return enumerator.Current;
                }
            }
        }

        /// <summary>
        /// Возвращает новый <see cref="IEnumerable{T}" />, итерирующий по первым <see cref="count" />
        /// элементам исходной коллекции
        /// </summary>
        /// <typeparam name="T">Тип элементов в коллекции</typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <param name="count">Число необходимых элементов</param>
        /// <returns>Экземпляр <see cref="IEnumerable{T}" />, итерирующий по первым элементам коллекции</returns>
        /// <exception cref="NullReferenceException">Если в качестве исходной коллекции был передан null</exception>
        public static IEnumerable<T> MyTake<T>(this IEnumerable<T> input, int count) {
            if (input == null) {
                throw new NullReferenceException();
            }
            using (IEnumerator<T> enumerator = input.GetEnumerator()) {
                for (int i = 0; i < count; i++) {
                    if (!enumerator.MoveNext()) {
                        yield break;
                    }
                    yield return enumerator.Current;
                }
            }
        }

        public static IEnumerable<T> MySkipWhile<T>(IEnumerable<T> input, Func<T, bool> predicate) {
            if (input == null) {
                throw new NullReferenceException();
            }
            using (var enumerator = input.GetEnumerator()) {
                while (enumerator.MoveNext()) {
                    if (predicate(enumerator.Current)) {
                        break;
                    }
                }
                while (enumerator.MoveNext()) {
                    yield return enumerator.Current;
                }
            }
        }

        public static IEnumerable<T> MyTakeWhile<T>(IEnumerable<T> input, Func<T, bool> predicate) {
            if (input == null) {
                throw new NullReferenceException();
            }
            foreach (var item in input) {
                if (predicate(item)) {
                    yield break;
                }
                yield return item;
            }
        }

        /// <summary>
        /// Проверяет наличие указанного элемента в коллекции
        /// </summary>
        /// <typeparam name="T">Тип элементов в коллекции</typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <param name="searchItem">Искомый элемент</param>
        /// <returns>True - если указанный элемент присутствует в коллекции, иначе False</returns>
        /// <exception cref="NullReferenceException">Если в качестве исходной коллекции был передан null</exception>
        public static bool MyContains<T>(this IEnumerable<T> input, T searchItem) {
            if (input == null) {
                throw new NullReferenceException();
            }
            foreach (var item in input) {
                if (item.Equals(searchItem)) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Возвращает новый <see cref="IEnumerable{T}" />, итерирующий по коллекции в обратном порядке
        /// </summary>
        /// <typeparam name="T">Тип элементов в коллекции</typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <returns>Экземпляр <see cref="IEnumerable{T}" />, итерирующий по коллекции в обратном порядке</returns>
        /// <exception cref="NullReferenceException">Если в качестве исходной коллекции был передан null</exception>
        public static IEnumerable<T> MyReverse<T>(this IEnumerable<T> input) {
            if (input == null) {
                throw new NullReferenceException();
            }
            T[] a = input.MyToArray();
            for (int i = a.Length - 1; i >= 0; i--) {
                yield return a[i];
            }
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Если в качестве исходной коллекции был передан null</exception>
        public static T[] MyToArray<T>(this IEnumerable<T> input) {
            if (input == null) {
                throw new NullReferenceException();
            }
            var list = new MyList<T>();
            foreach (var item in input) {
                list.Add(item);
            }
            return list.ToArray();
        }

        /// <summary>
        /// Применяет к последовательности агрегатную функцию
        /// </summary>
        /// <typeparam name="T">Тип элементов в исходной коллекции</typeparam>
        /// <typeparam name="TR">Тип агрегированного значения</typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <param name="f">Функция, принимающая на вход текущее состояние агрегированного значения и 
        /// следующий элемент последовательности и возвращающая новое состояние аггрегированого значения</param>
        /// <param name="init">Начальное значение</param>
        /// <returns>Конечное агрегатное значение</returns>
        public static TR MyAggregate<T, TR>(this IEnumerable<T> input, Func<TR, T, TR> f, TR init) {
            if (input == null) {
                throw new NullReferenceException();
            }
            var result = init;
            foreach (var item in input) {
                result = f(result, item);
            }
            return result;
        }

        /// <summary>
        /// Проверяет коллекцию на равенство всех ее элементов
        /// </summary>
        /// <typeparam name="T">Тип элементов в коллекциях</typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <param name="other">Коллекция для сравнения</param>
        /// <returns>True - если коллекции равны, иначе False</returns>
        public static bool MySequenceEqual<T>(this IEnumerable<T> input, IEnumerable<T> other) {
            if (input == null) {
                throw new NullReferenceException();
            }
            using (IEnumerator<T> inputEnum = input.GetEnumerator())
            using (IEnumerator<T> otherEnum = other.GetEnumerator()) {
                while (true) {
                    bool ie = inputEnum.MoveNext();
                    bool oe = otherEnum.MoveNext();
                    if (!ie && !oe) {
                        return true;
                    }
                    if (ie ^ oe) {
                        return false;
                    }
                    if (!inputEnum.Current.Equals(otherEnum.Current)) {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает элемент по указанному индексу
        /// </summary>
        /// <typeparam name="T">Тип элементов в коллекции</typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <param name="index">Индекс элемента</param>
        /// <returns>Элемент коллекции по указанному индексу</returns>
        public static T ElementAt<T>(this IEnumerable<T> input, int index) {
            if (input == null) {
                throw new NullReferenceException();
            }
            if (index < 0) {
                throw new IndexOutOfRangeException("Index out of range");
            }
            using (var enumerator = input.GetEnumerator()) {
                for (int i = 0; i < index; i++) {
                    if (!enumerator.MoveNext()) {
                        throw new IndexOutOfRangeException("Index out of range");
                    }
                }
                return enumerator.Current;
            }
        }

        /// <summary>
        /// Возвращает элемент по указанному индексу, либо значение по умолчанию, если указанный
        /// индекс находится вне границ
        /// </summary>
        /// <typeparam name="T">Тип элементов в коллекции</typeparam>
        /// <param name="input">Исходная коллекция</param>
        /// <param name="index">Индекс элемента</param>
        /// <param name="defaultValue">Значение по умолчанию</param>
        /// <returns></returns>
        public static T ElementAtOrDefault<T>(IEnumerable<T> input, int index, T defaultValue = default(T)) {
            if (input == null) {
                throw new NullReferenceException();
            }
            if (index < 0) {
                throw new IndexOutOfRangeException("Index out of range");
            }
            using (var enumerator = input.GetEnumerator()) {
                for (int i = 0; i < index; i++) {
                    if (!enumerator.MoveNext()) {
                        return defaultValue;
                    }
                }
                return enumerator.Current;
            }
        }
    }

    // TODO list:
    // Cast            Concat              DefaultIfEmpty     Distinct
    // Except          FirstOrDefault      Union              Zip
    // GroupBy         GroupJoin           Intersect          Join
    // OfType          OrderBy             OrderByDescending  SelectMany           
    // Single          SingleOrDefault     ToDictionary       ToList
    // ToLookup        
    //
    // TODO Numeric
    // Sum             Average
}