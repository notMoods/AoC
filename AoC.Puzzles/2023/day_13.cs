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

            static double CalcPart1(double loR, bool horizontal)
            {
                var prevSides = Math.Floor(loR);
                return horizontal ? 100 * prevSides : prevSides;
            }

            var input = File.ReadAllLines(path);

            var listOfPatterns = SequenceOfGrids(input);

            foreach(var grid in listOfPatterns)
            {
                (double lineOfReflection, bool horizontal) = FindLineOfReflection(grid);
                res1 += CalcPart1(lineOfReflection, horizontal);
            }
            return(res1.ToString(), res2.ToString());
        }

        private (double lineOfReflection, bool horizontal) FindLineOfReflection(string[] grid)
        {
            int horizontalCount = grid.Length, verticalCount = grid[0].Length;

            Span<int> codesForHorizontal = stackalloc int[horizontalCount];
            Span<int> codesForVertical = stackalloc int[verticalCount];

            //adding to codesForHorizontal
            for(int i = 0; i < horizontalCount; i++)
                codesForHorizontal[i] = grid[i].GetHashCode();


            //adding to codesForVertical
            Span<char> charContainer = stackalloc char[horizontalCount];
            for(int i = 0; i < verticalCount; i++)
            {
                for(int j = 0; j < horizontalCount; j++)
                    charContainer[j] = grid[j][i];
                
                codesForVertical[i] = string.GetHashCode(charContainer);
            }


            //checking vertical for the mirror
            for(var i = 0.5; i < verticalCount - 1; i++)
            {
                int bridge = (int)Math.Ceiling(i);

                var leftSpan = codesForVertical[..bridge];
                var rightSpan = codesForVertical[bridge..];

                if(Reflects(leftSpan, rightSpan)) return (1 + i, false);
            }

            //checking horizontal for the mirror
            for(var i = 0.5; i < horizontalCount - 1; i++)
            {
                int bridge = (int)Math.Ceiling(i);

                var leftSpan = codesForHorizontal[..bridge];
                var rightSpan = codesForHorizontal[bridge..];

                if (Reflects(leftSpan, rightSpan)) return (1 + i, true);
            }

            return (0, false);

            static bool Reflects(Span<int> left, Span<int> right)
            {
                int leftPointer = left.Length - 1, rightPointer = 0;

                while(leftPointer >= 0 && rightPointer < right.Length)
                {
                    if(left[leftPointer] != right[rightPointer]) return false;
                    leftPointer--; rightPointer++;
                }

                return true;
            }
        }
    }
}