
namespace multi_threaded_processing
{
    public  class  ArrayThreadsBase <T>
    {
        protected object lockObj = new object();
        protected readonly T[] arr;
        protected Thread[] _threads = Array.Empty<Thread>();
       public ArrayThreadsBase(T[] arr)
        {
            this.arr = arr;
        }
         protected T [] Run(Func<int,int,T> Process,int threadCount)
        {
            this._threads = new Thread[threadCount];
            int chunkSize = arr.Length / threadCount;
            T [] results = new T[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                int startIndex = i * chunkSize;
                int endIndex = (i + 1) * chunkSize;
                if (i == threadCount - 1) endIndex = arr.Length;
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