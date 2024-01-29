namespace AoC.Puzzles.Y_2023
{
    static partial class Day2
    {
        public static long Part2Solver()
        {
            var words = File.ReadAllLines("day_2\\input.txt");
            long res = 0;

            foreach(var word in words)
                res += CubeOfGame(Parser(word));

            return res;
        }

        private static long CubeOfGame(ParsedGame parsedGame) => parsedGame.Red * parsedGame.Blue * parsedGame.Green;
    }
}