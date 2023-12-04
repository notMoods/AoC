
namespace AoC.Day4
{
    partial class Day4
    {
        public int TotalCardPoint()
        {
            int total = 0;
            var cardsList = File.ReadAllLines("day_4\\input.txt");

            foreach(var card in cardsList)
                total += CardPoint(card);

            return total;
        }

        private int CardPoint(string card)
        {
            int start_winning = char.IsWhiteSpace(card[card.IndexOf(':') + 2]) ? 
                                            card.IndexOf(':') + 3 : card.IndexOf(':') + 2;

            int index_l = card.IndexOf('|');
            int start_own = char.IsWhiteSpace(card[index_l + 2]) ? index_l + 3 : index_l + 2;
                                            

            var winning = new List<int>();
            var own = new List<int>();
            

            int num_start = start_winning;
            for(int a = start_winning; a <= index_l - 1; a++)
            {
                if(!char.IsDigit(card[a]))
                {
                    winning.Add(int.Parse(card.Substring(num_start, a - num_start)));
                    
                    while(char.IsWhiteSpace(card[a])) a++;
                    
                    num_start = a;
                    a--;
                }
            }
            
            num_start = start_own;
            for(int b = start_own; b < card.Length; b++)
            {
                if(!char.IsDigit(card[b]))
                {
                    own.Add(int.Parse(card.Substring(num_start, b - num_start)));

                    while(char.IsWhiteSpace(card[b])) b++;
                    
                    num_start = b;
                    b--;
                }

                if(b == card.Length - 1) own.Add(int.Parse(card.Substring(num_start, card.Length - num_start)));
            }

            int res = winning.Intersect(own).ToList().Count;

            return res > 0 ? (int)Math.Pow(2, res - 1) : 0;
        }

        public static void Main(string[] args)
        {
            var sol = new Day4();
            Console.WriteLine(sol.TotalCardPoint());
        }
    }
}