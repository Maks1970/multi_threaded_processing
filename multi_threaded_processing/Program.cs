using multi_threaded_processing.Clases_Array;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace multi_threaded_processing
{


    internal partial class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[20];//{ 1, 2, 2, 3, 4, 5, 6, 7, 8, 1};
            var p = new RandomArrayThreadProcessor(4, arr);
            var sw = Stopwatch.StartNew();
            p.Run();
            sw.Stop();
            var procArr = new ArrayProcesing(4, arr);
            Console.WriteLine($"Min = {procArr.Min(2)}");
            Console.WriteLine($"Max = {procArr.Max(2)}");
            Console.WriteLine($"Sum = {procArr.Sum(3)}");
            Console.WriteLine($"Avarage = {procArr.Avarage(1)}");
            var d = procArr.InCopyPart(3,2,8);
            string text = "I have a best friend. Her name is Anna. Anna is very nice and funny. We go to the same school. Every day, we walk to school together. After school, we like to play games. Our favorite game is soccer. Anna is very good at soccer. Sometimes, we also watch movies. My favorite movie is «Toy Story». Anna likes it too. We always have fun together.";
            var ssw = Stopwatch.StartNew();
            //var dic = new Frequency_Words(text,3);
            Dictionary<char, int> freqDict = new Dictionary<char, int>();
           // Dictionary<string, int> freqDictt = new Dictionary<string, int>();
           var conterSymbols = new Frequency_symbols(text,freqDict,3);
            conterSymbols.Run();
            //freqDict = dic.Run();

            //var texdcouner = text.AsSpan();

            //foreach (char c in texdcouner)
            //{
            //    if (freqDict.ContainsKey(c))
            //        freqDict[c]++;
            //    else
            //        freqDict[c] = 1;
            //}
            foreach (var c in freqDict)
            {
                Console.WriteLine($"{c.Key} - {c.Value}");
            }
            //var sw = Stopwatch.StartNew();
            ssw.Stop();
            Console.WriteLine(ssw.Elapsed);

            //Console.WriteLine(sw.Elapsed);
        }
    }
}
