using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multi_threaded_processing.Clases_Array
{
    internal class Frequency_Words : Frequency_symbols
    {
        public Frequency_Words(string? text, Dictionary<string, int> _counter, int threadCount) : base(text, _counter, threadCount)
        {
            words = text!.Split(' ', '\n', '.', '!', '?','<','>',',');
        }

        public override void Procesing(int startIndex, int endIndex)
        {
            var span = words.AsSpan(startIndex, endIndex);
            foreach (var word in span) 
            {
                lock (lockObj)
                {
                    if (_counter.ContainsKey(word))
                        _counter[word]++;
                    else _counter[word] = 1;
                }
            }
        }
    }



}
