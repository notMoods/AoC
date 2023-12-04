namespace AoC.Day2
{
    partial class Day2
    {
        public long Part2Solver()
        {
            var words = File.ReadAllLines("day_2\\input.txt");
            long res = 0;

            foreach(var word in words)
                res += CubeOfGame(Parser(word));

            return res;
        }

        private long CubeOfGame(ParsedGame parsedGame) => parsedGame.Red * parsedGame.Blue * parsedGame.Green;
    }
}