namespace AoC.Y_2023
{
    partial class Day6
    {
        public static long FixedWaysToWinRace()
        {
            var input = File.ReadAllLines("day_6\\input.txt");
        
            string _ = "";
            foreach(var chara in input[0])
                if(char.IsDigit(chara)) _ += chara;

            var time = long.Parse(_);

            _ = "";
            foreach(var chara in input[1])
                if(char.IsDigit(chara)) _ += chara;

            var distance = long.Parse(_);


            return NoOfWays(time, distance);
        }
    }
}