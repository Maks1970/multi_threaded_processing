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
        protected readonly string? text;
        protected readonly int threadCount;
        Thread[] _threads = Array.Empty<Thread>();
        protected Dictionary<char,int> _Symbols = new Dictionary<char, int>();
        object lockObj = new object();

        public Frequency_symbols(string? text, Dictionary<char, int> _Symbols, int threadCount)
        {
            this.text = text;
            this.threadCount = threadCount;
            this._Symbols = _Symbols;
        }

        public void Run()
        {
            this._threads = new Thread[threadCount];
            int chunkSize = text.Length / threadCount;

            for (int i = 0; i < threadCount; i++)
            { 
                int startIndex = i * chunkSize;
                int endIndex = (i+1) * chunkSize;
                if(i==threadCount-1) endIndex = text.Length;
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
            
            var span = text.AsSpan(startIndex, endIndex);
            foreach (char c in span)
            {
                lock (lockObj)
                {
                    if (_Symbols.ContainsKey(c))
                        _Symbols[c]++;
                    else
                        _Symbols[c] = 1;
                }

            }
        }
    }
}
