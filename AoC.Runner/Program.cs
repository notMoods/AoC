using AoC.Puzzles.Y_2023;

namespace AoC.Runner
{
    class Runner{
        public static void Main(string[] args)
        {
            var sol = new Day12();

            var (p1, p2) = sol.Solution("C:\\Users\\HP PAVILION 14\\Documents\\Docs\\Coding_Stuff\\AoC\\AoC.Puzzles\\2023\\day_12\\input.txt");


            Console.WriteLine($"{p1}, {p2}");
            Console.ReadLine();
        }
    }
}