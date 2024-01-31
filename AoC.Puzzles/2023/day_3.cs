namespace AoC.Puzzles.Y_2023
{
    class Day3 : Tools.IDay
    {
        private int x_max;
        private int y_max;
        private string[] grid;
        
        private HashSet<string> stored_numbers = [];

        public Day3() => grid = new string[1];

        public (string, string) Solution(string path)
        {
            grid = File.ReadAllLines(path);
            x_max = grid[0].Length - 1;
            y_max = grid.Length - 1;
            
            long res1 = 0, res2 = 0;

            for(int a = 0; a < grid.Length; a++)
                for(int b = 0; b < grid[a].Length; b++)
                {
                    if(grid[a][b] != '.' && !char.IsDigit(grid[a][b]))
                        res1 += SymbolPartAdder(x: b, y: a);

                    if(grid[a][b] == '*')
                        res2 += GearRatio(x: b, y: a, []);
                }

            return (res1.ToString(), res2.ToString());        
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

            return int.Parse(temp);
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

        private bool BoundsCheck(int x, int y) => x >= 0 && y >= 0 && x <= x_max && y <= y_max;
    }
}