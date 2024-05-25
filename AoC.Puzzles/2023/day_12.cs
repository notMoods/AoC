using System.Text;

namespace AoC.Puzzles.Y_2023
{
    class Day12 : Tools.IDay
    {
        private class SpringsRow
        {
            public string NeededArrangement {get;}
            public byte[] Rep {get; }
            public int UnknownCount {get;}

            public SpringsRow(string row)
            {
                int unknowns = 0, counter = 0;
                var repp = new List<byte>();

                while(!char.IsDigit(row[counter]))
                {
                    if(row[counter] == '#') repp.Add(1);
                    if(row[counter] == '.') repp.Add(0);
                    if(row[counter] == '?'){
                        repp.Add(2);
                        unknowns++;
                    }
                    counter++;
                }

                NeededArrangement = row[counter..];
                Rep = [.. repp];
                UnknownCount = unknowns;
            }

            public SpringsRow(string row, bool fold)
            {
                int unknowns = 0, counter = 0;
                var repp = new List<byte>();

                while(!char.IsDigit(row[counter]))
                {
                    if(row[counter] == '#') repp.Add(1);
                    if(row[counter] == '.') repp.Add(0);
                    if(row[counter] == '?'){
                        repp.Add(2);
                        unknowns++;
                    }
                    counter++;
                }

                UnknownCount = (unknowns * 5) + 4;

                var sb = new StringBuilder();

                for(int a = 1; a <= 5; a++)
                    sb.Append($"{row[counter..]},");
                sb.Remove(sb.Length - 1, 1);

                NeededArrangement = sb.ToString();

                var bytearr = new List<byte>();
                for(int a = 1; a <= 5; a++)
                {
                    bytearr.AddRange([.. repp]);
                    if(a != 5) bytearr.Add(2);
                }

                Rep = [.. bytearr];
            }
        }
        public (string, string) Solution(string path)
        {
            var input = File.ReadAllLines(path);
            long res1 = 0, res2 = 0;

            foreach(var row in input)
            {
                //Console.WriteLine("start");
                res1 += ArrangementFinder(new SpringsRow(row));
                //Console.WriteLine("middle");
                res2 += ArrangementFinder(new SpringsRow(row, true));
                //Console.WriteLine(++counter);
            }

            return (res1.ToString(), res2.ToString());
        }

        private static long ArrangementFinder(SpringsRow spring_row)
        {
            var numOfArrangements = Math.Pow(2, spring_row.UnknownCount);

            long res = 0;

            for(int a = 0; a < numOfArrangements; a++)
            {
                string temp = ArrangementMaker(spring_row, a);
                if(!temp.Contains('#')) continue;

                if(FollowsArrangement(temp, spring_row.NeededArrangement)) res++;
            }

            return res;
        }

        private static string ArrangementMaker(SpringsRow spring_row, int a)
        {
            var arr = spring_row.Rep.ToArray();

            string binary = Convert.ToString(a, 2).PadLeft(spring_row.UnknownCount, '0');
            int bin_reader = 0;

            var foo = new StringBuilder();

            foreach(var num in arr)
            {
                if(num == 1) foo.Append('#');

                if(num == 0) foo.Append('.');

                if(num == 2)
                {
                    if(binary[bin_reader] == '1') foo.Append('#');
                    else foo.Append('.');

                    bin_reader++;
                }
            }

            return foo.ToString();
        }

        private static bool FollowsArrangement(string spring, string neededArrangement)
        {
            var sb = new StringBuilder();

            int damaged_count = 0;

            for(int a = 0; a < spring.Length; a++)
            {
                if(spring[a] == '#') damaged_count++;

                if(spring[a] == '.' && damaged_count != 0)
                {
                    sb.Append($"{damaged_count},");
                    damaged_count = 0;
                }
            }

            if(damaged_count != 0) sb.Append(damaged_count);

            if(sb[^1] == ',') sb.Remove(sb.Length - 1, 1);
           
            return sb.ToString() == neededArrangement;
        }
    }
}