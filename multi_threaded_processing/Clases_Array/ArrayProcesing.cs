using System.Threading;

namespace multi_threaded_processing
{

    public class ArrayProcesing : ArrayStreamsBase
    {
        public ArrayProcesing(int[] arr) : base(arr)
        {
        }
        
        public int Min(int threadCount)
        {
            return Run(FindMinInChunk, threadCount).Min();
        }
        public int Max(int threadCount)
        {
            return Run(FindMaxInChunk, threadCount).Max();
        }
        public int Sum(int threadCount)
        {
            return Run(SumInChunk, threadCount).Sum();
        }
        public int Avarage(int threadCount)
        {
            var partialSums = Run(SumInChunk, threadCount);
            return partialSums.Sum()/arr.Length;
        }
        private int FindMinInChunk(int start, int end)
        {
            Span<int> span = arr.AsSpan(start, end - start);
            int min = span[0];
            foreach (var item in span)
            {
                if (item < min)
                {
                    min = item;
                }
            }

            return min;
        }
        private int FindMaxInChunk(int start, int end)
        {
            Span<int> span = arr.AsSpan(start, end - start);
            int maxValue = span[0];
            for (int i = 0; i < span.Length; i++)
            {
                if (span[i] > maxValue)
                {
                    maxValue = span[i];
                }
            }

            return maxValue;
        }
        private int SumInChunk( int start, int end)
        {
            Span<int> span = arr.AsSpan(start, end - start);
            int sum = 0;
            for (int i = 0; i < span.Length; i++)
            {
                sum += span[i];
            }
            return sum;
        }
        public int[] InCopyPart(int threadCount, int start, int end)
        {
            int[] copyArr = new int[end - start]; // Розмір масиву залежить від діапазону

            this._threads = new Thread[threadCount];
            int chunkSize = (end - start) / threadCount; // Обчислюємо розмір шматка

            for (int i = 0; i < threadCount; i++)
            {
                // Розраховуємо діапазон для потоку
                int threadStart = start + i * chunkSize;
                int threadEnd = (i + 1) * chunkSize + start;

                // Останній потік обробляє залишок
                if (i == threadCount - 1)
                    threadEnd = end;

                var num = i; // Для доступу до індексу потоку всередині лямбди
                _threads[i] = new Thread(() =>
                {
                    // Створюємо Span тільки всередині потоку
                    var slice = arr.AsSpan(threadStart, threadEnd - threadStart);
                    slice.CopyTo(copyArr.AsSpan(threadStart - start));  // Копіюємо частину в copyArr
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
