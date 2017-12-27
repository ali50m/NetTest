using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormTest
{
    public static class Enumerable
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> list, Func<T,bool> indecate)
        {
            foreach (var item in list)
            {
                if (indecate(item))
                {
                    yield return item;
                }
            }
        }
        public static T[] ToArray<T>(this IEnumerable<T> list)
        {
            var initCount = 4;
            T[] array = new T[initCount];
            var i = 0;
            foreach (var item in list)
            {
                array[i++] = item;
                if (i==initCount)
                {
                    initCount*=2;
                    var newArray = new T[initCount];
                    array.CopyTo(newArray, 0);
                    array = newArray;
                }
            }
            var returnArray = new T[i];
            Array.Copy(array,returnArray,i);
            return returnArray;
        }
    }

    public delegate TResult Func<T,TResult>(T input);
}
