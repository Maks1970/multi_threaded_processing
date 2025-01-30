using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multi_threaded_processing.Clases_Array
{
    internal class Frequency_symbols
    {
        protected readonly string ?text;
        protected readonly int threadCount;
        protected Thread[] _threads = Array.Empty<Thread>();
        protected Dictionary<char, int> freqDict = new Dictionary<char, int>();
        int chunkSize;
        public Frequency_symbols(string? text, int threadCount)
        {
            this.text = text;
            this.threadCount = threadCount;
             chunkSize = text!.Length / threadCount;
        }

        public Dictionary<char, int> Run() 
        {
            this._threads = new Thread[threadCount];
            var lockObj = new object();

            for (int i = 0; i < threadCount; i++)
            {
                _threads[i] = new Thread(() =>
                {
                    Process(i);
                })
                {
                    IsBackground = true
                };
               
            }
            foreach (var thread in _threads)
            {
                thread.Start();
            }

            foreach (var thread in _threads) { thread.Join(); }
            return freqDict;
        }
        protected void Process(int threadIndex)
        {
            object lockObj = new object();
            int startIndex = threadIndex * chunkSize;
            int endIndex = (threadIndex+1) * chunkSize;
            if (threadIndex == threadCount)
            {
                endIndex = text.Length;
                startIndex = (threadIndex-1) * chunkSize;
            }
                lock (lockObj) {
                var span = text.AsSpan(startIndex, endIndex);
                //  var num = threadIndex;
                foreach (char c in span)
                {
                    if (freqDict.ContainsKey(c))
                        freqDict[c]++;
                    else
                        freqDict[c] = 1;
                }
            }
        }

    }
}
