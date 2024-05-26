namespace AoC.Puzzles.Y_2023
{
    class Day13 : Tools.IDay
    {
        public (string, string) Solution(string path)
        {
            double res1 = 0, res2 = 0;
            static IEnumerable<string[]> SequenceOfGrids(string[] input)
            {
                int start = 0;

                for (int a = 1; a < input.Length; a++)
                {
                    if (string.IsNullOrWhiteSpace(input[a]))
                    {
                        yield return input[start..a];
                        start = a + 1;
                    }
                }

                yield return input[start..^0];
            }

            static double Calc(double loR, bool horizontal)
            {
                var prevSides = Math.Floor(loR);
                return horizontal ? 100 * prevSides : prevSides;
            }

            var input = File.ReadAllLines(path);
            var listOfPatterns = SequenceOfGrids(input);

            foreach(var grid in listOfPatterns)
            {
                (double lineOfReflection, bool horizontal) = FindLineOfReflection(grid);
                res1 += Calc(lineOfReflection, horizontal);

                (double nlineOfReflection, bool nhorizontal) = FindLineOfReflection(grid, true);
                res2 += Calc(nlineOfReflection, nhorizontal);
            }

            return (res1.ToString(), res2.ToString());
        }

        private (double lineOfReflection, bool horizontal) FindLineOfReflection(string[] grid, bool part2 = false)
        {
            int horizontalCount = grid.Length, verticalCount = grid[0].Length;

            Span<long> codesForHorizontal = stackalloc long[horizontalCount];
            Span<long> codesForVertical = stackalloc long[verticalCount];

            //adding to codes for horizontal
            for(int i = 0; i < horizontalCount; i++)
            {
                long code = 0;
                for(int j = 0; j < verticalCount; j++)
                {
                    if(grid[i][j] == '#') code += 2 * (long)Math.Pow(3, j);
                    else code += 1 * (long)Math.Pow(3, j);
                }
                codesForHorizontal[i] = code;
            }

            //adding to codes for vertical
            for(int i = 0; i < verticalCount; i++)
            {
                long code = 0;
                for(int j = 0; j < horizontalCount; j++)
                {
                    if(grid[j][i] == '#') code += 2 * (long)Math.Pow(3, j);
                    else code += 1 * (long)Math.Pow(3, j);
                }
                codesForVertical[i] = code;
            }
  
            //checking for mirror through vertical
            for(var i = 0.5; i < verticalCount - 1; i++)
            {
                int bridge = (int)Math.Ceiling(i);

                var leftSpan = codesForVertical[..bridge];
                var rightSpan = codesForVertical[bridge..];

                if(Reflects(leftSpan, rightSpan, part2)) return (1 + i, false);
            }

            //checking for mirror through horizontal
            for(var i = 0.5; i < horizontalCount - 1; i++)
            {
                int bridge = (int)Math.Ceiling(i);

                var leftSpan = codesForHorizontal[..bridge];
                var rightSpan = codesForHorizontal[bridge..];

                if (Reflects(leftSpan, rightSpan, part2)) return (1 + i, true);
            }

            return (0, false);

            static bool Reflects(Span<long> left, Span<long> right, bool part2)
            {
                int leftPointer = left.Length - 1, rightPointer = 0, dif_count = 0;
                long dif = 0;

                while(leftPointer >= 0 && rightPointer < right.Length)
                {
                    var difPerNum = left[leftPointer] - right[rightPointer];
                    if(difPerNum != 0)
                    {
                        dif_count++;
                        if(dif_count > 1) break;
                        dif += Math.Abs(difPerNum);
                    }
                    leftPointer--; rightPointer++;
                }

                if(part2) return dif_count == 1 && IfPowerOf3(dif);
                else return dif_count == 0;

                static bool IfPowerOf3(long n)
                {
                    if(n == 0) return false;
                    if(n == 1) return true;
                    while(n % 3 == 0) n /= 3;
            
                    return n == 1;
                }
            }
        }
    }
}