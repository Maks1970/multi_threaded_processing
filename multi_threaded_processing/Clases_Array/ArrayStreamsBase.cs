
namespace multi_threaded_processing
{
    public class ArrayStreamsBase
    {
        protected readonly int[] arr;
        protected readonly int threadCount;
        protected Thread[] _threads = Array.Empty<Thread>();
        public ArrayStreamsBase(int threadCount, int[] arr)
        {
            this.threadCount = threadCount;
            this.arr = arr;
        }
        public int [] Run(Func<int,int,int> Process,int threadCount)
        {
            this._threads = new Thread[threadCount];
            int chunkSize = arr.Length / threadCount;
            int [] results = new int[threadCount];
            // Створення потоків
            for (int i = 0; i < threadCount; i++)
            {
                int startIndex = i * chunkSize;
                int endIndex = (i + 1) * chunkSize;
                if (i == threadCount - 1) endIndex = arr.Length; // Останній потік обробляє залишок

               // int[] subArray = new int[endIndex - startIndex];
                //Array.Copy(arr, startIndex, subArray, 0, subArray.Length);

                var num = i;
                _threads[i] = new Thread(() => results[num] = Process(startIndex, endIndex))
                {
                    IsBackground = true
                };
            }
            foreach (var thread in _threads)
            {
                thread.Start();
            }

            foreach (var thread in _threads) { thread.Join(); }
            return results;
        }
    }
}