namespace AoC.Day3
{
    partial class Day3
    {
        private int x_max;
        private int y_max;
        private string[] grid;
        
        private HashSet<string> stored_numbers = new HashSet<string>();

        public Day3()
        {
            grid = File.ReadAllLines("day_3\\input.txt");

            x_max = grid[0].Length - 1;
            y_max = grid.Length - 1;
        }

        public long PartNumberSum()
        {
            long sum = 0;

            for(int a = 0; a < grid.Length; a++)
                for(int b = 0; b < grid[a].Length; b++)
                    if(grid[a][b] != '.' && !char.IsDigit(grid[a][b]))
                        sum += SymbolPartAdder(x: b, y: a);

            return sum;
        }

        private long SymbolPartAdder(int x, int y)
        {
            long sum = 0;

            //left
            if(BoundsCheck(x - 1, y) && char.IsDigit(grid[y][x - 1])) sum += Digitizer(x - 1, y);

            //right
            if(BoundsCheck(x + 1, y) && char.IsDigit(grid[y][x + 1])) sum += Digitizer(x + 1, y);
            
            //up
            if(BoundsCheck(x, y - 1) && char.IsDigit(grid[y - 1][x])) sum += Digitizer(x, y - 1);
            
            //down
            if(BoundsCheck(x, y + 1) && char.IsDigit(grid[y + 1][x])) sum += Digitizer(x, y + 1);

            //diagonal-up-left
            if(BoundsCheck(x - 1, y - 1) && char.IsDigit(grid[y - 1][x - 1])) sum += Digitizer(x - 1, y - 1);

            //diagonal-up-right
            if(BoundsCheck(x + 1, y - 1) && char.IsDigit(grid[y - 1][x + 1])) sum += Digitizer(x + 1, y - 1);

            //diagonal-down-left
            if(BoundsCheck(x - 1, y + 1) && char.IsDigit(grid[y + 1][x - 1])) sum += Digitizer(x - 1, y + 1);

            //diagonal-down-right
            if(BoundsCheck(x + 1, y + 1) && char.IsDigit(grid[y + 1][x + 1])) sum += Digitizer(x + 1, y + 1);


            return sum;
        }

        private long Digitizer(int x, int y)
        {
            if(stored_numbers.Contains($"x:{x}, y:{y}")) return 0;

            int start = 0, end = x_max;

            for(int a = x; a >= 0; a--)
                if(!char.IsDigit(grid[y][a]))
                {
                    start = a + 1;
                    break;
                }

            for(int b = x; b <= x_max; b++)
                if(!char.IsDigit(grid[y][b]))
                {
                    end = b - 1;
                    break;
                }
            
            string temp = "";
            for(int c = start; c <= end; c++)
            {
                stored_numbers.Add($"x:{c}, y:{y}");
                temp += grid[y][c];
            }

            return (long)int.Parse(temp);
        }

        private bool BoundsCheck(int x, int y) => x >= 0 && y >= 0 && x <= x_max && y <= y_max;
    }
}