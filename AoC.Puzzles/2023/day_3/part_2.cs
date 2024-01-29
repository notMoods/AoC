namespace AoC.Puzzles.Y_2023
{
    partial class Day3
    {
        public long GearRatioSum()
        {
            long sum = 0;

            for(int a = 0; a < grid.Length; a++)
                for(int b = 0; b < grid[a].Length; b++)
                    if(grid[a][b] == '*')
                        sum += GearRatio(x: b, y: a, []);

            return sum;
        }

        private long GearRatio(int x, int y, HashSet<string> avoid_repeats)
        {
            long sum = 1;
            int count = 0;

            //left
            if(BoundsCheck(x - 1, y) && !avoid_repeats.Contains($"x:{x - 1}, y:{y}") && char.IsDigit(grid[y][x - 1]))
            {
                count++;
                sum *= NewDigitizer(x - 1, y, avoid_repeats);
            }

            //right
            if(BoundsCheck(x + 1, y) && !avoid_repeats.Contains($"x:{x + 1}, y:{y}") && char.IsDigit(grid[y][x + 1]))
            {
                count++;
                sum *= NewDigitizer(x + 1, y, avoid_repeats);
            }
            
            //up
            if(BoundsCheck(x, y - 1) && !avoid_repeats.Contains($"x:{x}, y:{y - 1}") && char.IsDigit(grid[y - 1][x]))
            {
                count++;
                sum *= NewDigitizer(x, y - 1, avoid_repeats);
            }
            
            //down
            if(BoundsCheck(x, y + 1) && !avoid_repeats.Contains($"x:{x}, y:{y + 1}") && char.IsDigit(grid[y + 1][x]))
            {
                count++;
                sum *= NewDigitizer(x, y + 1, avoid_repeats);
            }

            //diagonal-up-left
            if(BoundsCheck(x - 1, y - 1) && !avoid_repeats.Contains($"x:{x - 1}, y:{y - 1}") && char.IsDigit(grid[y - 1][x - 1]))
            {
                count++;
                sum *= NewDigitizer(x - 1, y - 1, avoid_repeats);
            }

            //diagonal-up-right
            if(BoundsCheck(x + 1, y - 1) && !avoid_repeats.Contains($"x:{x + 1}, y:{y - 1}") && char.IsDigit(grid[y - 1][x + 1]))
            {
                count++;
                sum *= NewDigitizer(x + 1, y - 1, avoid_repeats);
            }

            //diagonal-down-left
            if(BoundsCheck(x - 1, y + 1) && !avoid_repeats.Contains($"x:{x - 1}, y:{y + 1}") && char.IsDigit(grid[y + 1][x - 1]))
            {
                count++;
                sum *= NewDigitizer(x - 1, y + 1, avoid_repeats);
            }

            //diagonal-down-right
            if(BoundsCheck(x + 1, y + 1) && !avoid_repeats.Contains($"x:{x + 1}, y:{y + 1}") && char.IsDigit(grid[y + 1][x + 1]))
            {
                count++;
                sum *= NewDigitizer(x + 1, y + 1, avoid_repeats);
            }

            if(count == 2) return sum;
            else return 0;
        }

        private long NewDigitizer(int x, int y, HashSet<string> bar)
        {
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
                temp += grid[y][c];
                bar.Add($"x:{c}, y:{y}");
            }
            

            return int.Parse(temp);
        }
    }
}