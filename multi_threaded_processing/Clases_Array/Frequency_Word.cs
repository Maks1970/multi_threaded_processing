using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multi_threaded_processing.Clases_Array
{
    internal class Frequency_Word : ArrayThreadsBase<string>
    {  
        private Dictionary<string, int> _counter = new Dictionary<string, int>();
        public Frequency_Word(string text) : base(text.Split(' '))
        {
        }

        public  Dictionary<string, int> Frequencyword( int threadCount)
        {
            Run(Procesing, threadCount);
            return _counter;
        }
        private string Procesing(int startIndex, int endIndex)
        {
             var d =arr.Length;
                var span = arr.AsSpan(startIndex, endIndex - startIndex);
            foreach (var word in span)
            {
                lock (lockObj)
                {
                    if (_counter.ContainsKey(word))
                        _counter[word]++;
                    else _counter[word] = 1;
                }
            }
            return default;
        }

    }
}
