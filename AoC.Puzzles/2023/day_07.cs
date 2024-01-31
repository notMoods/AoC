namespace AoC.Puzzles.Y_2023
{
    class Day7 : Tools.IDay
    {
        private class CardHands(string hand, int bid) : IComparable<CardHands>
        {
            public string hand = hand;
            public int bid = bid;

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

		private class CrazyCardHand(string hand, int bid) : IComparable<CrazyCardHand>
		{
			public string hand = hand;
			public int bid = bid;

			public int CompareTo(CrazyCardHand? other)
			{
				if (other == null) return -1;

				int hand_one = this.GetNewHandValue();
				int hand_two = other.GetNewHandValue();

				if (hand_one > hand_two) return -1;
				else if (hand_one < hand_two) return 1;
				else
				{
					int bar = 0;

					for (int a = 0; a < 5; a++)
					{
						if (GetNewCardValue(this.hand[a]) > GetNewCardValue(other.hand[a]))
						{
							bar = -1;
							break;
						}
						else if (GetNewCardValue(this.hand[a]) < GetNewCardValue(other.hand[a]))
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
				if (char.IsDigit(v)) return int.Parse($"{v}");

				if (v == 'J') return 1;
				if (v == 'T') return 10;
				if (v == 'Q') return 11;
				if (v == 'K') return 12;
				return 13;
			}

			private int GetNewHandValue()
			{
				if (this.hand == "JJJJJ") return 7;

				var _foo = new Dictionary<char, int>();
				int max = 0;
				char let_max = ' ';

				foreach (var letter in this.hand)
				{
					if (_foo.TryGetValue(letter, out int value)) _foo[letter] = ++value;
					else _foo.Add(letter, 1);

					if (letter != 'J')
					{
						if (_foo[letter] > max)
						{
							max = _foo[letter];
							let_max = letter;
						}
					}
				}

				if (_foo.TryGetValue('J', out int val))
				{
					_foo[let_max] += val;
					_foo.Remove('J');
				}

				string foo = "";
				foreach (var kvp in _foo)
					foo += $"{kvp.Value}";

				if (foo == "5") return 7;
				if (foo == "41" || foo == "14") return 6;
				if (foo == "32" || foo == "23") return 5;
				if (foo == "113" || foo == "131" || foo == "311") return 4;
				if (foo == "221" || foo == "212" || foo == "122") return 3;
				if (foo == "11111") return 1;
				return 2;
			}
		}

        public (string, string) Solution(string path)
        {
            var input = File.ReadAllLines(path);

            long res1 = 0, res2 = 0;

            var listOfHands1 = new List<CardHands>();
            var listOfHands2 = new List<CrazyCardHand>();

            foreach(var hand in input)
            {
				listOfHands1.Add(new(hand[0..5], int.Parse(hand[6..])));
				listOfHands2.Add(new(hand[0..5], int.Parse(hand[6..])));
			}

            listOfHands1.Sort(); 
			listOfHands2.Sort();

            int count = listOfHands1.Count;

            for(int a = 0; a < listOfHands1.Count; a++)
            {
                res1 += count * listOfHands1[a].bid;
                res2 += count * listOfHands2[a].bid;

                count--;
            }

            return (res1.ToString(), res2.ToString());
        }
    }
}