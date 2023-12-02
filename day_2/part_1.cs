namespace AoC.Day2
{
    partial class Day2
    {
        private struct ParsedGame
        {
            public int ID{get; set;}
            public int Green{get; set;}
            public int Red{get; set;}
            public int Blue{get; set;}
        }
        public int ValidGames()
        {
            var words = File.ReadAllLines("day_2\\input.txt");
            int res = 0;

            foreach(var word in words)
                res += GameValidator(Parser(word));

            return res;
        }

        private ParsedGame Parser(string word)
        {
            var parsedGame = new ParsedGame();
            int colon_index = word.IndexOf(':');

            parsedGame.ID = int.Parse(word.Substring(5, colon_index - 5));

            int num_start = colon_index + 2;
            for(int a = colon_index + 2; a < word.Length; a++)
            {
                if(char.IsWhiteSpace(word[a]))
                {
                    int temp = int.Parse(word.Substring(num_start, a - num_start));
                    switch(word[a + 1])
                    {
                        case 'r':
                            parsedGame.Red = Math.Max(parsedGame.Red, temp);
                            break;
                        case 'b':
                            parsedGame.Blue = Math.Max(parsedGame.Blue, temp);
                            break;
                        case 'g':
                            parsedGame.Green = Math.Max(parsedGame.Green, temp);
                            break;
                    }

                    while(a < word.Length && !char.IsDigit(word[a])) a++;
                    num_start = a;

                    a--;
                }
            }
            return parsedGame;            
        }

        private int GameValidator(ParsedGame pG)
        {
            //12 red cubes, 13 green cubes, and 14 blue cubes
            if(pG.Red <= 12 && pG.Green <= 13 && pG.Blue <= 14) return pG.ID;
            else return 0;
        }
    }
}