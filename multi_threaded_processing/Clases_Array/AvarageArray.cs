using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace multi_threaded_processing.Clases_Array
{
    internal class AvarageArray<T> : SumArray<T> where T : IAdditionOperators<T, T, T>
    {
        public AvarageArray(T[] arr) : base(arr)
        {
        }
        public T Avarage(int threadCount) => Run(SumInChunk, threadCount).Aggregate((a, b) => a + b) / (dynamic)arr.Length;
    }
}

