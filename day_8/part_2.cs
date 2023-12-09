namespace AoC.Y_2023
{
    partial class Day8
    {
        public static long GhostPath()
        {
            var input = File.ReadAllLines("day_8\\input.txt");

            var instructions = input[0];
            var network = input[2..];


            List<int> cur_indexes = FindIndexesEndInA(network);

            List<long> each_step = [];

            for(int a = 0; a < cur_indexes.Count; a++)
                each_step.Add(FindStepToEndInZ(cur_indexes[a], instructions, network));
              
            return FindLCMList(each_step);
        }

        private static long FindStepToEndInZ(int index, string instructions, string[] network)
        {
            long count = 0;
            int instruction_step = 0;

            while(network[index][2] != 'Z')
            {
                if(instructions[instruction_step] == 'L') index = FindIndexOf(network[index][7..10], network);
                else index = FindIndexOf(network[index][12..15], network);
                    
                instruction_step++;
                if(instruction_step == instructions.Length) instruction_step = 0;

                count++;
            }

            return count;
        }

        private static List<int> FindIndexesEndInA(string[] network)
        {
            List<int> res = [];

            for(int a = 0; a < network.Length; a++)
                if(network[a][2] == 'A')
                    res.Add(a);

            return res;
        }

        private static long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        private static long LCM(long a, long b) => a * b / GCD(a, b);

        private static long FindLCMList(List<long> _list)
        {
            long res = _list[0];

            for(int a = 1; a < _list.Count; a++)
                res = LCM(res, _list[a]);
            
            return res;
        }
    }
}