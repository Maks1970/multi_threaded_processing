using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace multi_threaded_processing.Clases_Array
{
    internal class Frequency_symbols
    {
        protected string?[] words;
        protected readonly int threadCount;
        Thread[] _threads = Array.Empty<Thread>();
        protected Dictionary<string,int> _counter = new Dictionary<string, int>();
        protected object lockObj = new object();

        public Frequency_symbols(string? text, Dictionary<string, int> _Symbols, int threadCount)
        {
            words = text!.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            this.threadCount = threadCount;
            this._counter = _Symbols;
        }

        public void Run()
        {
            this._threads = new Thread[threadCount];
            int chunkSize = words.Length / threadCount;

            for (int i = 0; i < threadCount; i++)
            { 
                int startIndex = i * chunkSize;
                int endIndex = (i+1) * chunkSize;
                if(i==threadCount-1) endIndex = words.Length;
                _threads[i] = new Thread(() => { Procesing(startIndex, endIndex- startIndex);
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

        }

        public virtual  void  Procesing(int startIndex, int endIndex)
        {
            
            var span = words.AsSpan(startIndex, endIndex);

            foreach (var word in span) 
            {
                foreach (char c  in word)
                {
                    string key = c.ToString();
                    lock (lockObj)
                    {
                        if (_counter.ContainsKey(key))
                            _counter[key]++;
                        else
                            _counter[key] = 1;
                    }

                }
            }
            
        }
    }
}
