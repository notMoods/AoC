namespace AoC.Puzzles.Y_2023
{
    class Day6 : Tools.IDay
    {
        public (string, string) Solution(string path)
        {
            long res1 = 1;

            var input = File.ReadAllLines(path);

            var timeArr = input[0][(input[0].IndexOf(':') + 1)..]
                                  .Trim()
                                  .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                  .Select(long.Parse).ToArray();

            var distArr = input[1][(input[1].IndexOf(':') + 1)..]
                                  .Trim()
                                  .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                  .Select(long.Parse).ToArray();


            var time = input[0].Where(char.IsDigit)
                               .Select(char.GetNumericValue)
                               .Aggregate(0L, (acc, next) => acc * 10 + (long)next);

            var dist = input[1].Where(char.IsDigit)
                               .Select(char.GetNumericValue)
                               .Aggregate(0L, (acc, next) => acc * 10 + (long)next);
                               

            for(int a = 0; a < timeArr.Length; a++)
                res1 *= NoOfWays(timeArr[a], distArr[a]);

            return (res1.ToString(), NoOfWays(time, dist).ToString());
        }

        private static long NoOfWays(long time, long dist)
        {
            var root = Math.Sqrt(Math.Pow(time, 2) - (4 * dist));

            var top_lim = (time + root) / 2;
            var down_lim = (time - root) / 2;

            if(!(Math.Abs(top_lim - Math.Floor(top_lim)) < double.Epsilon)) top_lim = Math.Ceiling(top_lim);
                
            if(!(Math.Abs(down_lim - Math.Floor(down_lim)) < double.Epsilon)) down_lim = Math.Floor(down_lim);

            return (long)(top_lim - down_lim - 1);
        }  
    }
}