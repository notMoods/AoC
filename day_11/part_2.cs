namespace AoC.Y_2023
{
    partial class Day11
    {
        public long SumOfShortestPathsExpanded()
        {
            var updated_galaxies = galaxies.Select(cord => NewGalaxyUpdater(cord.x, cord.y)).ToList();

            long res = 0;

            for(int a = 0; a < updated_galaxies.Count; a++)
                for(int b = a + 1; b < updated_galaxies.Count; b++)
                    res += Math.Abs(updated_galaxies[a].x - updated_galaxies[b].x) + Math.Abs(updated_galaxies[a].y - updated_galaxies[b].y);
    
            return res;
        }

        private (int x, int y) NewGalaxyUpdater(int x, int y)
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

            if(num_rows != 0) y -= num_rows;
            if(num_cols != 0) x -= num_cols;

            num_cols *= 1_000_000;
            num_rows *= 1_000_000;

            return (num_cols + x, num_rows + y);            
        }
    }
}