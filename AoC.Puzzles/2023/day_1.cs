namespace AoC.Puzzles.Y_2023
{
    class Day1 : Tools.IDay
    {
        private readonly List<string> digit_words = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
        public (string, string) Solution(string path)
        {
            var words = File.ReadAllLines(path);

            long res1 = 0, res2 = 0;
            foreach(var word in words)
            {
                res1 += (Digiter(word, 1) * 10) + Digiter(word, -1);
                res2 += (FixedDigitizer(word.AsSpan(), 1) *10) + FixedDigitizer(word.AsSpan(), -1);
            }

            return (res1.ToString(), res2.ToString());
        }

        private static int Digiter(string word, int incrementer)
        {
            int a = incrementer > 0 ? 0 : word.Length - 1;
            int stop = incrementer > 0 ? word.Length - 1 : 0;

            while(a != stop){
                if(word[a] - '0' < 10) break;
                a += incrementer;
            }
            return word[a] -'0';  
        }

        private int FixedDigitizer(ReadOnlySpan<char> word, int incrementer)
        {
            int a = incrementer > 0 ? 0 : word.Length - 1;
            int stop = incrementer > 0 ? word.Length : -1;

            int res = -1, counter = 1;
            while (a != stop)
            {
                if(word[a] - '0' < 10) break;

                if(counter > 2)
                {
                    int start = incrementer > 0 ? 0 : a;
                    (bool isTrue, int val) = TextChecker(word.Slice(start, counter));

                    if(isTrue)
                    {
                        res = val;
                        break;
                    }
                }
                a += incrementer;
                counter++;
            }

            if(res == -1) return word[a] - '0';
            else return res;
        }

        private (bool isTrue, int val) TextChecker(ReadOnlySpan<char> v)
        {
            string baz = v.ToString();
            var foo = digit_words.FirstOrDefault(baz.Contains);
            
            if(foo == null) return (false, 0);
            else return (true, digit_words.IndexOf(foo) + 1);
        }
    }
}