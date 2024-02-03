namespace AoC.Puzzles.Y_2023
{
    class Day4 : Tools.IDay
    {
        private List<int> cardsCount = [];
        private long sum;
        
        public (string, string) Solution(string path)
        {
            var cardsList = File.ReadAllLines(path);

            cardsCount = Enumerable.Repeat(1, cardsList.Length).ToList();

            long res1 = 0;

            for(int a = 0; a < cardsList.Length; a++)
            {
                Solver(cardsList[a], a);
                int temp = CardMatches(cardsList[a]);
                if(temp > 0) res1 += (int)Math.Pow(2, temp - 1);
            }
                
            return (res1.ToString(), sum.ToString());
        }

        private static int CardMatches(string card)
        {
            int start_winning = char.IsWhiteSpace(card[card.IndexOf(':') + 2]) ? 
                                            card.IndexOf(':') + 3 : card.IndexOf(':') + 2;

            int index_l = card.IndexOf('|');
            int start_own = char.IsWhiteSpace(card[index_l + 2]) ? index_l + 3 : index_l + 2;
                                            

            var winning = new List<int>();
            var own = new List<int>();
            

            int num_start = start_winning;
            for(int a = start_winning; a <= index_l - 1; a++)
                if(!char.IsDigit(card[a]))
                {
                    winning.Add(int.Parse(card[num_start..a]));
                    
                    while(char.IsWhiteSpace(card[a])) a++;
                    
                    num_start = a;
                    a--;
                }
            
            num_start = start_own;
            for(int b = start_own; b < card.Length; b++)
            {
                if(!char.IsDigit(card[b]))
                {
                    own.Add(int.Parse(card[num_start..b]));

                    while(char.IsWhiteSpace(card[b])) b++;

                    num_start = b;
                    b--;
                }

                if(b == card.Length - 1) own.Add(int.Parse(card[num_start..]));
            }

            return winning.Intersect(own).ToList().Count;
        }

        private void Solver(string card, int index)
        {
            sum += cardsCount[index];

            int matches = CardMatches(card);

            for(int a = index + 1; a <= index + matches && a < cardsCount.Count; a++)
                cardsCount[a] += cardsCount[index];
        }
    }
}