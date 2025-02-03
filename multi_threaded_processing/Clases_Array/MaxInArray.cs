using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multi_threaded_processing.Clases_Array
{
    internal class MaxInArray<T> : ArrayThreadsBase<T> where T : IComparable<T>
    {
        public MaxInArray(T[] arr) : base(arr)
        {
        }
        public T? Max(int threadCount) => Run(FindMaxInChunk, threadCount).Max();
        private T FindMaxInChunk(int start, int end)
        {
            Span<T> span = arr.AsSpan(start, end - start);
            T maxValue = span[0];
            for (int i = 0; i < span.Length; i++)
            {
                if (span[i].CompareTo(maxValue) >0 )
                {
                    maxValue = span[i];
                }
            }
            return maxValue;
        }
    }
}
