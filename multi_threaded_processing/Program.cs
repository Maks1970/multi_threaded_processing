using System.Diagnostics;

namespace multi_threaded_processing
{


    internal partial class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[20_000_000];//{ 1, 2, 2, 3, 4, 5, 6, 7, 8, 1};
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
            //Console.WriteLine(sw.Elapsed);
        }
    }
}
