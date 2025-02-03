using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace multi_threaded_processing.Clases_Array
{
    internal class InCopyPartArray<T> : ArrayThreadsBase<T>
    {
        public InCopyPartArray(T[] arr) : base(arr)
        {
        }
        public T[] InCopyPart(int threadCount,int start, int end) 
        {
             T[] copyArr = new T[end - start]; 
            this._threads = new Thread[threadCount];
            int chunkSize = (end - start) / threadCount;
            for (int i = 0; i < threadCount; i++)
            {
                int threadStart = start + i * chunkSize;
                int threadEnd = (i + 1) * chunkSize + start;
                if (i == threadCount - 1)
                    threadEnd = end;
                var num = i;
                _threads[i] = new Thread(() =>
                {
                    var slice = arr.AsSpan(threadStart, threadEnd - threadStart);
                    slice.CopyTo(copyArr.AsSpan(threadStart - start));
                })
                {
                    IsBackground = true
                };
            }

            foreach (var thread in _threads)
            {
                thread.Start();
            }

            foreach (var thread in _threads)
            {
                thread.Join();
            }

            return copyArr;
        }
    }
}
