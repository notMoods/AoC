namespace AoC.Puzzles.Y_2023
{
    class Day8 : Tools.IDay
    {
        public (string, string) Solution(string path)
        {
            var input = File.ReadAllLines(path);

            long res1 = 0;

			var instructions = input[0];
			var network = input[2..];

			int instruction_step = 0;

			int cur_index = FindIndexOf("AAA", network);

			while (network[cur_index][0..3] != "ZZZ")
			{
				string next_node;

				if (instructions[instruction_step] == 'L') next_node = network[cur_index][7..10];
				else next_node = network[cur_index][12..15];

				instruction_step++;

				if (instruction_step == instructions.Length) instruction_step = 0;

				cur_index = FindIndexOf(next_node, network);

				res1++;
			}

			List<int> cur_indexes = FindIndexesEndInA(network);
			List<long> each_step = [];

			for (int a = 0; a < cur_indexes.Count; a++)
				each_step.Add(FindStepToEndInZ(cur_indexes[a], instructions, network));

			return (res1.ToString(), FindLCMList(each_step).ToString());
		}

        private static int FindIndexOf(string _node, string[] network)
        {
            for(int a = 0; a < network.Length; a++)
                if(network[a][0..3] == _node)
                    return a;
            
            return -1;
        }

		private static long FindStepToEndInZ(int index, string instructions, string[] network)
		{
			long count = 0;
			int instruction_step = 0;

			while (network[index][2] != 'Z')
			{
				if (instructions[instruction_step] == 'L') index = FindIndexOf(network[index][7..10], network);
				else index = FindIndexOf(network[index][12..15], network);

				instruction_step++;
				if (instruction_step == instructions.Length) instruction_step = 0;

				count++;
			}

			return count;
		}

		private static List<int> FindIndexesEndInA(string[] network)
		{
			List<int> res = [];

			for (int a = 0; a < network.Length; a++)
				if (network[a][2] == 'A')
					res.Add(a);

			return res;
		}

		private static long LCM(long a, long b)
		{
			long gcd_a = a, gcd_b = b;
			while (gcd_b != 0)
			{
				long temp = gcd_b;
				gcd_b = gcd_a % gcd_b;
				gcd_a = temp;
			}

			return a * b / gcd_a;
		}

		private static long FindLCMList(List<long> _list)
		{
			long res = _list[0];

			for (int a = 1; a < _list.Count; a++)
				res = LCM(res, _list[a]);

			return res;
		}
	}
}