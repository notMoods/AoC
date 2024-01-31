namespace AoC.Puzzles.Y_2023
{
    class Day11 : Tools.IDay
    {
        private string[] input = [];
        private List<int> no_galaxy_rows = [];
        private List<int> no_galaxy_columns = [];

        private IEnumerable<(int x, int y)> galaxies = [];


        public (string, string) Solution(string path)
        {
			input = File.ReadAllLines(path);

			no_galaxy_rows = input.Select((value, index) => new { Value = value, Index = index })
								  .Where(x => !x.Value.Contains('#'))
								  .Select(x => x.Index).ToList();

			no_galaxy_columns = Enumerable.Range(0, input[0].Length)
								.Where(colIndex => !input.Any(row => row[colIndex] == '#'))
								.ToList();

			galaxies = input.SelectMany((row, rowIndex) => row.Select((cell, columnIndex) => new { Value = cell, x = columnIndex, y = rowIndex }))
								.Where(x => x.Value == '#')
								.Select(x => (x.x, x.y));

			long res1 = 0, res2 = 0;

			var updated_galaxies1 = galaxies.Select(cord => GalaxyUpdater(cord.x, cord.y)).ToList();
			var updated_galaxies2 = galaxies.Select(cord => GalaxyUpdater(cord.x, cord.y, true)).ToList();

			for (int a = 0; a < updated_galaxies1.Count; a++)
				for (int b = a + 1; b < updated_galaxies1.Count; b++)
                {
					res1 += Math.Abs(updated_galaxies1[a].x - updated_galaxies1[b].x) + Math.Abs(updated_galaxies1[a].y - updated_galaxies1[b].y);
					res2 += Math.Abs(updated_galaxies2[a].x - updated_galaxies2[b].x) + Math.Abs(updated_galaxies2[a].y - updated_galaxies2[b].y);
				}

            return (res1.ToString(), res2.ToString());
		}

        private (int x, int y) GalaxyUpdater(int x, int y, bool new_update = false)
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

            if(new_update)
            {
				if (num_rows != 0) y -= num_rows;
				if (num_cols != 0) x -= num_cols;

				num_cols *= 1_000_000;
				num_rows *= 1_000_000;
			}

            return (num_cols + x, num_rows + y);
        }
	}
}