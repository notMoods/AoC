namespace AoC.Day9
{
    partial class Day9
    {
        public static long SumOfNextValues()
        {
            var input = File.ReadAllLines("day_9\\input.txt");

            long res = 0;

            foreach(var line in input)
                res += GetNextValue(line.Split(' ').Select(long.Parse).ToList());

            return res;
        }

        private static long GetNextValue(List<long> list)
        {
            long res = 0;
            List<(long first, long last)> listOfPairs = [];

            for(int a = 1; a < list.Count; a++)
            {
                long _first = GetFirst(a, list, 1);
                long _last = GetFirst(a, list, -1);

                listOfPairs.Add((first: _first, last: _last));

                if(_first == _last && _first == 0) break;
            }

            foreach(var (first, last) in listOfPairs)
                res += last;

            return list[^1] + res;  
        }

        private static long GetFirst(int a, List<long> list, int determiner)
        {
            List<int> listOfCoefficients = GetCoefficientsPascal(a);
            long res = 0;

            int start = determiner == 1 ? 0 : list.Count - 1;

            foreach(var coefficient in listOfCoefficients)
            {
                res += coefficient * list[start];
                start += determiner;
            }

            return res;
        }

        private static List<int> GetCoefficientsPascal(int row_num)
        {
            List<int> final_row = [];

            for(int i = 0; i < row_num + 1; i++)
            {
                List<int> row = [];

                for(int j = 0; j <= i; j++)
                {
                    if(j == 0 || j == i) row.Add(1);
                    else row.Add(final_row[j - 1] + final_row[j]);
                }
                final_row = row;
            }

            for(int a = 0; a < final_row.Count; a++)
                if(a % 2 == 1) final_row[a] *= -1;

            return final_row;
        }
    }
}