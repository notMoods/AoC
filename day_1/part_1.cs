namespace AoC.Day1
{
    partial class Day1{
        public long Calibrator()
        {
            var words = File.ReadAllLines("day_1\\input.txt");
            long res = 0;

            foreach(var word in words)
               res += ((FirstDigit(word) * 10) + LastDigit(word));
            
            return res;     
        }

        private int FirstDigit(string word)
        {
            int a = 0;
            while(a < word.Length){
                if(word[a] - '0' < 10 && word[a] - '0' >= 0) break;
                a++;
            }
            return word[a] -'0';  
        }

        private int LastDigit(string word)
        {
            int a = word.Length - 1;
            while(a >= 0){
                if(word[a] - '0' < 10 && word[a] - '0' >= 0) break;
                a--;
            }
            return word[a] -'0';
        }
    }
}