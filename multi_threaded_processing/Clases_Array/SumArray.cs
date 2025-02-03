using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace multi_threaded_processing.Clases_Array
{
    internal class SumArray<T> : ArrayThreadsBase<T> where T : IAdditionOperators<T,T,T>
    {
        public SumArray(T[] arr) : base(arr)
        {
        }
        public T? Sum(int threadCount) => Run(SumInChunk, threadCount).Aggregate((a, b) => a + b);
        protected T SumInChunk(int start, int end)
        {
            Span<T> span = arr.AsSpan(start, end - start);
            T sum = default(T)!;//(T)(object)0;
            for (int i = 0; i < span.Length; i++)
            {
                sum += span[i];
            }
            return sum;
        }
    }
}
