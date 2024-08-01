namespace AoC.Puzzles.Y_2023;

class Day15 : Tools.IDay
{
	public (string, string) Solution(string path)
	{
		long res1 = 0, res2 = 0;
		string[] sequence = File.ReadAllText(path)
						   .Split(',');

		foreach (var step in sequence)
		{
			res1 += step.Aggregate((long)0, (cur_val, @char) =>
			{
				cur_val += @char;
				cur_val *= 17;
				cur_val %= 256;

				return cur_val;
			});
		}


		return (res1.ToString(), res2.ToString());
	}
}