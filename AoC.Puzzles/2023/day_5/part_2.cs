namespace AoC.Y_2023
{
    partial class Day5
    {
        private List<SeedRange> seeds = [];
        private struct SeedRange(long start, long length)
        {
            public long Start { get; init; } = start;
            public long Length { get; init; } = length;
            public long End { get; init; } = start + length - 1;
        }

        public long LowestLocationNumberFromRange()
        {
            var input = File.ReadAllLines("day_5\\input.txt");

            var seedRanges = input[0][7..].Split(' ')
                                          .Select(long.Parse)
                                          .Chunk(2)
                                          .Select(x => new SeedRange(x[0], x[1]))
                                          .ToList();

            var mapRanges = GetMapRanges(input);

            foreach(var section in mapRanges)
            {
                for(int a = 0; a < seedRanges.Count; a++)
                    RangeTransformer(section, seedRanges[a]);

                seedRanges = seeds;
            }
             
            return MinimumLocation(seedRanges);
        }

        private void RangeTransformer(IEnumerable<MapRange> section, SeedRange seedRange)
        {
            //SeedRange:  Start: 730   Length: 45  End:  774
            // MapRange:  Source: 700  Length: 50  Dest: 90

            //new SeedRanges: 
            //Start: 120 -> 139

            foreach(var range in section)
            {

            }



        }

        private static long MinimumLocation(List<SeedRange> seedRanges)
        {
            long res = long.MaxValue;

            foreach(var seedRange in seedRanges)
                res = Math.Min(res, seedRange.Start);
            
            return res;
        }
    }
}