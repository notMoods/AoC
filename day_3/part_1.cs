using System.Linq.Expressions;

namespace AoC.Day3
{
    partial class Day3
    {
        private int x_max;
        private int y_max;
        private string[] grid;
        
        private HashSet<string> stored_numbers;

        public Day3()
        {
            grid = File.ReadAllLines("day_3\\input.txt");
            stored_numbers = new HashSet<string>();
        }
        
        
        public long PartNumberSum()
        {
            long sum = 0;
            var gridLines = grid;
    
            x_max = gridLines[0].Length - 1;
            y_max = gridLines.Length - 1;


            for(int a = 0; a < gridLines.Length; a++)
                for(int b = 0; b < gridLines[a].Length; b++)
                    if(gridLines[a][b] != '.' && !char.IsDigit(gridLines[a][b]))
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