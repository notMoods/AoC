namespace AoC.Y_2023
{
    partial class Day7
    {
        private class CardHands : IComparable<CardHands>
        {
            public string hand;
            public int bid;

            public CardHands(string hand, int bid)
            {
                this.hand = hand;
                this.bid = bid;
            }

            public int CompareTo(CardHands? other)
            {
                if(other ==  null) return -1;
                
                int hand_one = this.GetHandValue();
                int hand_two = other.GetHandValue();

                if(hand_one > hand_two) return -1;
                else if(hand_one < hand_two) return 1;
                else{
                    int bar = 0;

                    for(int a = 0; a < 5; a++)
                    {
                        if(GetCardValue(this.hand[a]) > GetCardValue(other.hand[a]))
                        {
                            bar = -1;
                            break;
                        }
                        else if(GetCardValue(this.hand[a]) < GetCardValue(other.hand[a]))
                        {
                            bar = 1;
                            break;
                        }
                    }

                    return bar;
                }
            }

            private static int GetCardValue(char v)
            {
                if(char.IsDigit(v)) return int.Parse($"{v}");

                if(v == 'T') return 10;
                if(v == 'J') return 11;
                if(v == 'Q') return 12;
                if(v == 'K') return 13;
                return 14;
            }

            private int GetHandValue()
            {
                var _foo = new Dictionary<char, int>();

                foreach(var letter in this.hand)
                {
                    if(_foo.TryGetValue(letter, out int value)) _foo[letter] = ++value;
                    else _foo.Add(letter, 1);
                }

                string foo = "";
                foreach(var kvp in _foo)
                    foo += $"{kvp.Value}";

                if(foo == "5") return 7;
                if(foo == "41" || foo == "14") return 6;
                if(foo == "32" || foo == "23") return 5;
                if(foo == "113" || foo == "131" || foo == "311") return 4;
                if(foo == "221" || foo == "212" || foo == "122") return 3;
                if(foo == "11111") return 1;
                return 2;
            }
        }
        public static long TotalWinnings()
        {
            long res = 0;
            var input = File.ReadAllLines("day_7\\input.txt");

            var listOfHands = new List<CardHands>();

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
    }
}