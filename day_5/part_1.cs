namespace AoC.Day5
{
    partial class Day5
    {
        private record struct MapRange(long Dest, long Source, long Length)
        {
            internal readonly long? Map(long num)
            {
                var temp = num - Source;

                return temp >= 0 && temp < Length ? Dest + temp : null;
            }

            private readonly long SourceEnd => Source + Length - 1;
        }

        public long LowestLocationNumber()
        {
            var input = File.ReadAllLines("day_5\\input.txt");

            var seeds = input[0][7..].Split(' ').Select(long.Parse).ToList();
            
            var mapRanges = GetMapRanges(input);

            foreach(var section in mapRanges)
                for(int a = 0; a < seeds.Count; a++)
                    seeds[a] = Transform(section, seeds[a]);
                
            return seeds.Min();
        }

        private static long Transform(IEnumerable<MapRange> section, long v)
        {
            long? res = 0;
            foreach(var range in section)
            {
                res = range.Map(v);
                if(res is not null) return (long)res;
            }

            return v;
        }

        private IEnumerable<IEnumerable<MapRange>> GetMapRanges(string[] input)
        {
            List<string[]> sections  = [];
            
            int start_index = 3;
            for(int a = 3; a < input.Length; a++)
            {
                if(input[a] == string.Empty)
                {
                    sections.Add(input[start_index..a]);
                    start_index = a + 2; a++;
                }

                if(a == input.Length - 1) sections.Add(input[start_index..]);
            }

            return sections.Select(section => section
                                .Select(line => line.Split(' ')
                                .Select(long.Parse)
                                .ToList()))
                            .Select(x => x
                                .Select(list => new MapRange(list[0], list[1], list[2])));
        }
    }
}