namespace AoC.Puzzles.Y_2023
{
    static partial class Day1{
        public static long Calibrator()
        {
            var words = File.ReadAllLines("day_1\\input.txt");
            long res = 0;

            foreach(var word in words)
               res += (Digiter(word, 1) * 10) + Digiter(word, -1);
            
            return res;     
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

    }
}