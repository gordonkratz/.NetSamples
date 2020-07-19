using System;

namespace SampleApp.Core
{
    public static class ArrayExtensions
    {
        public static bool None<T>(this T[,] array, Func<T, bool> func)
        {
            foreach(var item in array)
            {
                if (func(item))
                    return false;
            }
            return true;
        }
    }
}
