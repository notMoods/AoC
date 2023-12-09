namespace AoC.Day8
{
    partial class Day8
    {
        public static long StepsAAAToZZZ()
        {
            var input = File.ReadAllLines("day_8\\input.txt");

            var instructions = input[0];
            int instruction_step = 0;

            var network = input[2..];

            long count = 0;

            int cur_index = FindIndexOf("AAA", network);

            while(network[cur_index][0..3] != "ZZZ")
            {
                string next_node;
                if (instructions[instruction_step] == 'L') next_node = network[cur_index][7..10];
                else next_node = network[cur_index][12..15];
                    
                instruction_step++;
                if(instruction_step == instructions.Length) instruction_step = 0;

                cur_index = FindIndexOf(next_node, network);
                count++;
            }

            return count;
        }

        private static int FindIndexOf(string _node, string[] network)
        {
            for(int a = 0; a < network.Length; a++)
                if(network[a][0..3] == _node)
                    return a;
            
            return -1;
        }
    }
}