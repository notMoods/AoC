
namespace AoC.Day1
{
    static partial class Day1
    {
        public static long FixedCalibrator()
        {
            var words = File.ReadAllLines("day_1\\input.txt");

            long res = 0;

            foreach(var word in words)
                res += (FixedFirstDigit(word) * 10) + FixedLastDigit(word);
            
            return res;
        }

        private static int FixedFirstDigit(string word)
        {
            int a = 0;
            int res = -1;

            while(a < word.Length)
            {
                if(char.IsDigit(word[a])) break;

                if(a >= 2)
                {
                    (bool isTrue, int val) = TextChecker(word[..(a + 1)]);

                    if(isTrue)
                    {
                        res = val;
                        break;
                    }
                }
                a++;
            }
            if(res == -1) return word[a] - '0';
            else return res;
        }

        private static int FixedLastDigit(string word)
        {
            int a = word.Length - 1;
            int res = -1;

            while(a >= 0)
            {
                if(char.IsDigit(word[a])) break;

                if(a <= word.Length - 3)
                {
                    (bool isTrue, int val) = TextChecker(word[a..]);

                    if(isTrue)
                    {
                        res = val;
                        break;
                    }
                }
                a--;
            }
            if(res == -1) return word[a] - '0';
            else return res;
        }

        private static (bool isTrue, int val) TextChecker(string v)
        {
            var digits = new List<string>{"one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};

            var foo = digits.FirstOrDefault(s => v.Contains(s));
            

            if(foo == null) return (false, 0);
            else return (true, digits.IndexOf(foo) + 1);
        }
    }
}