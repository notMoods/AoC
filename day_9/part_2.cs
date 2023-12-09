namespace AoC.Day9
{
    partial class Day9
    {
        public static long ReverseSumOfNextValues()
        {
            var input = File.ReadAllLines("day_9\\input.txt");

            long res = 0;

            foreach(var line in input)
                res += GetBeforeFirstValue(line.Split(' ').Select(long.Parse).ToList());

            return res;
        }

        private static long GetBeforeFirstValue(List<long> list)
        {
            long res = 0;
            List<(long first, long last)> listOfPairs = [];

            for(int a = 1; a < list.Count; a++)
            {
                long _first = GetFirstOrLast(a, list, 1);
                long _last = GetFirstOrLast(a, list, -1);

                listOfPairs.Add((first: _first, last: _last));

                if(_first == _last && _first == 0) break;
            }

            for(int b = listOfPairs.Count - 1; b >= 0; b--)
                res = listOfPairs[b].first  - res;
            
            return list[0] - res;
        }
    }
}