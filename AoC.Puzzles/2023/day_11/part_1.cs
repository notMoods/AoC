namespace AoC.Y_2023
{
    partial class Day11
    {
        private readonly string[] input;
        private readonly List<int> no_galaxy_rows;
        private readonly List<int> no_galaxy_columns;

        private readonly IEnumerable<(int x, int y)> galaxies;

        public Day11()
        {
            input = File.ReadAllLines("day_11\\input.txt");

            no_galaxy_rows = input.Select((value, index) => new {Value = value, Index = index})
                                  .Where(x => !x.Value.Contains('#'))
                                  .Select(x => x.Index).ToList();

            no_galaxy_columns = Enumerable.Range(0, input[0].Length)
                                .Where(colIndex => !input.Any(row => row[colIndex] == '#'))
                                .ToList();

            galaxies = input.SelectMany((row, rowIndex) => row.Select((cell, columnIndex) => new {Value = cell, x = columnIndex, y = rowIndex}))
                                .Where(x => x.Value == '#')
                                .Select(x => (x.x, x.y));
        }

        public static void Main(string[] args)
        {
            var sol = new Day11();
            Console.WriteLine($"{sol.SumOfShortestPaths()}, {sol.SumOfShortestPathsExpanded()}");
        }

        public long SumOfShortestPaths()
        {
            var updated_galaxies = galaxies.Select(cord => GalaxyUpdater(cord.x, cord.y)).ToList();

            long res = 0;

            for(int a = 0; a < updated_galaxies.Count; a++)
                for(int b = a + 1; b < updated_galaxies.Count; b++)
                    res += Math.Abs(updated_galaxies[a].x - updated_galaxies[b].x) + Math.Abs(updated_galaxies[a].y - updated_galaxies[b].y);
    
            return res;
        }

        private (int x, int y) GalaxyUpdater(int x, int y)
        {
            int num_cols = 0, num_rows = 0;

            for(int a = 0; a < no_galaxy_rows.Count; a++)
            {
                if(no_galaxy_rows[a] > y)
                {
                    num_rows = a;
                    break;
                }

                if(a == no_galaxy_rows.Count - 1) num_rows = a + 1;
            }

            for(int a = 0; a < no_galaxy_columns.Count; a++)
            {
                if(no_galaxy_columns[a] > x)
                {
                    num_cols = a;
                    break;
                }

                if(a == no_galaxy_columns.Count - 1) num_cols = a + 1;   
            }

            return (num_cols + x, num_rows + y);
        }
    }
}