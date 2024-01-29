namespace AoC.Puzzles.Y_2023
{
    partial class Day10
    {
        private readonly string[] input;
        private readonly int x_max;
        private readonly int y_max;
        public Day10()
        {
            input = File.ReadAllLines("day_10\\input.txt");
            x_max = input[0].Length - 1;
            y_max = input.Length - 1;
        }

        public long FarthestPointFromStart()
        {
            (int row, int column) s_index = FindS();

            var loop_points = GetPointsInMainLoop(s_index);
            
            return loop_points.Count / 2;
        }

        private (int row, int column) FindS()
        {
            int row = 0;

            foreach(var line in input)
            {
                int column = line.IndexOf('S');
                if(column != -1) return (row, column);
                row++;
            }

            return (-1, -1);
        }

        private HashSet<(int row, int column)> GetPointsInMainLoop((int row, int column) s_index)
        {
            var hashset = new HashSet<(int row, int column)>{s_index};

            (int cur_row, int cur_column, char prev_direction) = FindNextPointAfterS(s_index.row, s_index.column);

            while(input[cur_row][cur_column] != 'S')
            {
                hashset.Add((cur_row, cur_column));

                (cur_row, cur_column, prev_direction) = FindNextPoint(cur_row, cur_column, prev_direction);
            }
            return hashset;
        }

        private (int cur_row, int cur_column, char direction) FindNextPointAfterS(int row, int column)
        {
            bool BoundsCheck(int row, int column) => row >= 0 && column >= 0 && row <= y_max && column <= x_max;

            if(BoundsCheck(row - 1, column))
            {
                char c = input[row - 1][column];
                if(c == '|' || c == '7' || c == 'F') return (row - 1, column, 'S');
            }

            if(BoundsCheck(row + 1, column))
            {
                char c = input[row + 1][column];
                if(c == '|' || c == 'L' || c == 'J') return (row + 1, column, 'N');
            }

            if(BoundsCheck(row, column - 1))
            {
                char c = input[row][column - 1];
                if(c == '-' || c == 'L' || c == 'F') return (row, column - 1, 'E');
            }

            if(BoundsCheck(row, column + 1))
            {
                char c = input[row][column + 1];
                if(c == '-' || c == '7' || c == 'J') return (row, column + 1, 'W');
            }

            return (0, 0, '0');
        }

        private (int row, int column, char direction) FindNextPoint(int row, int column, char prev_direction)
        {
            char c = input[row][column];

            if(prev_direction == 'N')
            {
                if(c == '|') return (row + 1, column, 'N');
                if(c == 'L') return (row, column + 1, 'W');
                if(c == 'J') return (row, column - 1, 'E');
            }

            if(prev_direction == 'E')
            {
                if(c == '-') return (row, column - 1, 'E');
                if(c == 'L') return (row - 1, column, 'S');
                if(c == 'F') return (row + 1, column, 'N');
            }

            if(prev_direction == 'S')
            {
                if(c == '|') return (row - 1, column, 'S');
                if(c == '7') return (row, column - 1, 'E');
                if(c == 'F') return (row, column + 1, 'W');
            }

            if(prev_direction == 'W')
            {
                if(c == '-') return (row, column + 1, 'W');
                if(c == '7') return (row + 1, column, 'N');
                if(c == 'J') return (row - 1, column, 'S');
            }

            return (-1, -1, '0');
        }
    } 
}