namespace AoC.Day4
{
    partial class Day4
    {   
        private List<int> cardsCount = new List<int>();
        private long sum = 0;
        
        public long UltimateTotalCardsPoint()
        {
            var cardsList = File.ReadAllLines("day_4\\input.txt");

            cardsCount = Enumerable.Repeat(1, cardsList.Length).ToList();

            for(int a = 0; a < cardsList.Length; a++)
            {
                Solver(cardsList[a], a);
            }

            return sum;
        }

        private void Solver(string card, int index)
        {
            sum += cardsCount[index];

            int matches = CardMatches(card);

            for(int a = index + 1; a <= index + matches && a < cardsCount.Count; a++)
            {
                cardsCount[a] += cardsCount[index];
            }
        }
    }
}