using multi_threaded_processing.Clases_Array;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace multi_threaded_processing
{


    internal partial class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[2_000_000];//{ 1, 2, 2, 3, 4, 5, 6, 7, 8, 1};
            var p = new RandomArrayThreadProcessor(2, arr);
            var sw = Stopwatch.StartNew();
            p.Run();
            sw.Stop();
            var procArr = new ArrayProcesing(arr);
            Console.WriteLine($"Min = {procArr.Min(8)}");
            Console.WriteLine($"Max = {procArr.Max(8)}");
            Console.WriteLine($"Sum = {procArr.Sum(8)}");
            Console.WriteLine($"Avarage = {procArr.Avarage(8)}");
            var d = procArr.InCopyPart(3,2,8);
            Console.WriteLine(sw.Elapsed);
            string text = "I have a best friend. Her name is Anna. Anna is very nice and funny. We go to the same school. Every day, we walk to school together. After school, we like to play games. Our favorite game is soccer. Anna is very good at soccer. Sometimes, we also watch movies. My favorite movie is «Toy Story». Anna likes it too. We always have fun together.";
            Dictionary<string, int> Words = new Dictionary<string, int>();
            Dictionary<string, int> Symbols = new Dictionary<string, int>();
            var conterSymbols = new Frequency_symbols(text, Symbols, 3);
            conterSymbols.Run();
            var counterWord = new Frequency_Words(text, Words, 3);
            counterWord.Run();
            //foreach (var c in Words)
            //{
            //    Console.WriteLine($"{c.Key} - {c.Value}");
            //}
            //ssw.Stop();
            //00:00:00.0213622
            //Console.WriteLine(ssw.Elapsed);
        }
    }
}
