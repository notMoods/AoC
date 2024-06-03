namespace AoC.Puzzles.Y_2023;

class Day14 : Tools.IDay
{
    public (string, string) Solution(string path)
    {
        static void GetListForColumn(string[] grid, int col_index, Span<char> column)
        {
            for(int i = 0; i < column.Length; i++)
                column[i] = grid[i][col_index];
        }

        var input = File.ReadAllLines(path);
        long res1 = 0, res2 = 0;

        var no_of_columns = input[0].Length;

        Span<char> column = stackalloc char[input.Length];

        for(int i = 0; i < no_of_columns; i++)
        {
            GetListForColumn(input, i, column);

            res1 += GetLoadForList(column);
        }
        return (res1.ToString(), res2.ToString());
    }

    private static long GetLoadForList(Span<char> column)
    {
        TiltLoad(column);

        long res = 0;
        int adder = column.Length;

        foreach(var @char in column)
        {
            if(@char == 'O') res += adder;
            adder--;
        }

        return res;
    }

    static void TiltLoad(Span<char> column)
    {
        int first_p = 0;
        while(column[first_p] != '.') first_p++;

        int second_p = first_p + 1;

        bool approachedHash = false;

        while(second_p < column.Length)
        {
            if(column[second_p] == 'O' && !approachedHash)
            {
                column[first_p] = 'O';
                column[second_p] = '.';
                first_p++;
            }else if(column[second_p] == '#')
            {
                approachedHash = true;
            }
            else if(column[second_p] == '.' && approachedHash)
            {
                first_p = second_p;
                approachedHash = false;
            }

            second_p++;
        }
    }
}