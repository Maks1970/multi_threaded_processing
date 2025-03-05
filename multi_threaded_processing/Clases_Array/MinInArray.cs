using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multi_threaded_processing.Clases_Array
{
    internal class MinInArray<T> : ArrayThreadsBase<T> where T : IComparable<T>
    {
        public MinInArray(T[] arr) : base(arr)
        {
        }
        public T? Min(int threadCount) => Run(FindMinInChunk, threadCount).Min();
        private T FindMinInChunk(int start, int end)
        {
            Span<T> span = arr.AsSpan(start, end - start);
            T min = span[0];
            foreach (var item in span)
            {
                if (item.CompareTo(min) < 0)
                {
                    min = item;
                }
            }
            return min;
        }
    }
}
