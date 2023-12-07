namespace AoC.Day6
{
    partial class Day6
    {
        public long FixedWaysToWinRace()
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


            var root = Math.Sqrt(Math.Pow(time, 2) - (4 * distance));

            var top_lim = (time + root) / 2;
            var down_lim = (time - root) / 2;

            if(!IsWholeNumber(top_lim)) top_lim = Math.Ceiling(top_lim);
                
            if(!IsWholeNumber(down_lim)) down_lim = Math.Floor(down_lim);

            return (long)(top_lim - down_lim - 1);
        }
    }
}