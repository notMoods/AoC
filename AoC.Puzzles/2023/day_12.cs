
namespace AoC.Puzzles.Y_2023
{
    class Day12 : Tools.IDay
    {
        public (string, string) Solution(string path)
        {
            var input = File.ReadAllLines(path);
            long res1 = 0;

            foreach(var row in input)
            {
                res1 += ArrangementFinder(row);
            }

            


            return (res1.ToString(), "0");
        }

        private long ArrangementFinder(string row)
        {
            throw new NotImplementedException();
        }
    }
}