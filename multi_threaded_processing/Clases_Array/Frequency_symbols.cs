using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multi_threaded_processing.Clases_Array
{
    internal class Frequency_symbols : ArrayThreadsBase<string>
    {
        private Dictionary<string, int> _counter = new Dictionary<string, int>();
        public Frequency_symbols(string text) : base(text.Split(' '))
        {
        }
        public Dictionary<string, int> FrequencySymbols(int threadCount)
        {
            Run(Procesing, threadCount);
            return _counter;
        }
        private string  Procesing(int startIndex, int endIndex)
        {
            var span = arr.AsSpan(startIndex, endIndex - startIndex);//endIndex - startIndex
            foreach (var word in span)
            {
                foreach (char c in word)
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
            return default;
        }
    }
}
