namespace multi_threaded_processing
{
    internal partial class Program
    {
        class RandomArrayThreadProcessor 
        {
            private readonly int[] arr;
            private readonly int threadCount;
            private Thread[] _threads = Array.Empty<Thread>();
            public RandomArrayThreadProcessor(int threadCount, int[] arr)
            {
                this.threadCount = threadCount;
                this.arr = arr;
            }
            public void Run()
            {
                this._threads = new Thread[threadCount];
                int chunkSize = arr.Length / threadCount;
                for (int i = 0; i < threadCount; i++)
                {
                    int startIndex = i * chunkSize;
                    int endIndex = (i + 1) * chunkSize;
                    if (i == threadCount - 1) endIndex = arr.Length;
                    int[] subArray = new int[endIndex - startIndex];
                    var num = i;
                    _threads[i] = new Thread(() => Process(num))
                    {
                        IsBackground = true
                    };
                }
                foreach (var thread in _threads)
                {
                    thread.Start();
                }

                foreach (var thread in _threads) { thread.Join(); }
            }
            protected void Process(int threadIndex)
            {
                var rand = new Random();

                int chunkSize = arr.Length / threadCount;
                int start = threadIndex * chunkSize;
                int end = (threadIndex == threadCount - 1) ? arr.Length : start + chunkSize;

                var span = arr.AsSpan(start, end - start);

                for (int i = 0; i < span.Length; i++)
                {
                    span[i] = rand.Next(1,200_000);
                }
            }
        }
    }
}
