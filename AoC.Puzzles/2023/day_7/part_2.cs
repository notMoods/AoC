namespace AoC.Puzzles.Y_2023
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
                if(this.hand == "JJJJJ") return 7;

                var _foo = new Dictionary<char, int>();
                int max = 0;
                char let_max = ' ';

                foreach(var letter in this.hand)
                {
                    if(_foo.TryGetValue(letter, out int value)) _foo[letter] = ++value;
                    else _foo.Add(letter, 1);

                    if(letter != 'J')
                    {
                        if(_foo[letter] > max)
                        {
                            max = _foo[letter];
                            let_max = letter;
                        }
                    }
                }

                if(_foo.TryGetValue('J', out int val))
                {
                    _foo[let_max] += val;
                    _foo.Remove('J');
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
    }
}