using System.Globalization;

namespace AoC.Day6
{
    partial class Day6
    {
        public long WaysToBeatRecord()
        {
            long res = 1;

            var input = File.ReadAllLines("day_6\\input.txt");

            var timeArr = input[0][(input[0].IndexOf(':') + 1)..]
                                  .Trim()
                                  .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                  .Select(long.Parse).ToArray();

            var distArr = input[1][(input[1].IndexOf(':') + 1)..]
                                  .Trim()
                                  .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                  .Select(long.Parse).ToArray();

            for(int a = 0; a < timeArr.Length; a++)
            {
                var root = Math.Sqrt(Math.Pow(timeArr[a], 2) - (4 * distArr[a]));

                var top_lim = (timeArr[a] + root) / 2;
                var down_lim = (timeArr[a] - root) / 2;

                if(!IsWholeNumber(top_lim)) top_lim = Math.Ceiling(top_lim);
                
                if(!IsWholeNumber(down_lim)) down_lim = Math.Floor(down_lim);

                res *= (long)(top_lim - down_lim - 1);
            }
            return res;
        }

        private bool IsWholeNumber(double num) => Math.Abs(num - Math.Floor(num)) < double.Epsilon;
    }
}