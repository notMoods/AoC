namespace AoC.Day7
{
    partial class Day7
    {
        private class CrazyCardHand : IComparable<CrazyCardHand>
        {
            public string hand;
            public int bid;

            public CrazyCardHand(string hand, int bid)
            {
                this.hand = hand;
                this.bid = bid;
            }

            public int CompareTo(CrazyCardHand? other)
            {
                if(other ==  null) return -1;
                
                int hand_one = this.GetNewHandValue();
                int hand_two = other.GetNewHandValue();

                if(hand_one > hand_two) return -1;
                else if(hand_one < hand_two) return 1;
                else{
                    int bar = 0;

                    for(int a = 0; a < 5; a++)
                    {
                        if(GetNewCardValue(this.hand[a]) > GetNewCardValue(other.hand[a]))
                        {
                            bar = -1;
                            break;
                        }
                        else if(GetNewCardValue(this.hand[a]) < GetNewCardValue(other.hand[a]))
                        {
                            bar = 1;
                            break;
                        }
                    }

                    return bar;
                }
            }

            private static int GetNewCardValue(char v)
            {
                if(char.IsDigit(v)) return int.Parse($"{v}");

                if(v == 'J') return 1;
                if(v == 'T') return 10;
                if(v == 'Q') return 11;
                if(v == 'K') return 12;
                return 13;
            }

            private int GetNewHandValue()
            {
                throw new NotImplementedException();
            }
        }

        public static long NewTotalwinnings()
        {
            long res = 0;
            var input = File.ReadAllLines("day_7\\input.txt");

            var listOfHands = new List<CrazyCardHand>();

            foreach(var hand in input)
                listOfHands.Add(new(hand[0..5], int.Parse(hand[6..])));

            listOfHands.Sort();

            int count = listOfHands.Count;

            foreach(var hands in listOfHands)
            {
                res += count * hands.bid;
                count--;
            }

            return res;
        }       

        public static void Main(string[] args) => Console.WriteLine(Day7.NewTotalwinnings()); 
    }
}