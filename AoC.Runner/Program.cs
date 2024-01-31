using AoC.Puzzles.Y_2023;
using System.Diagnostics;

namespace AoC.Runner
{
    class Runner{
        public static void Main(string[] args)
        {
            var sol = new Day11();
            var sw = Stopwatch.StartNew();
            var (p1, p2) = sol.Solution("C:\\Users\\HP PAVILION 14\\Documents\\Docs\\Coding_Stuff\\AoC\\AoC.Puzzles\\2023\\day_11\\input.txt");
            sw.Stop();
            Console.WriteLine($"{p1}, {p2}, {sw.ElapsedMilliseconds}");

            Console.ReadLine();
        }
    }
}