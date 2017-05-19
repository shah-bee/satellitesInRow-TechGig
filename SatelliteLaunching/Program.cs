using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteLaunching
{
    class Program
    {

        static void Main(String[] args)
        {
            int output;
            int ip1;
            ip1 = Convert.ToInt32(Console.ReadLine());
            int ip2;
            ip2 = Convert.ToInt32(Console.ReadLine());
            int ip3_size = 0;
            ip3_size = Convert.ToInt32(Console.ReadLine());
            string[] ip3 = new string[ip3_size];
            string ip3_item;
            for (int ip3_i = 0; ip3_i < ip3_size; ip3_i++)
            {
                ip3_item = Console.ReadLine();
                ip3[ip3_i] = ip3_item;
            }
            output = launchingSatellites(ip1, ip2, ip3);
            Console.WriteLine(output);
        }

        static int launchingSatellites(int input1, int input2, string[] input3)
        {

            // int[,] space = new int[input1, input2];

            Dictionary<string, List<string>> hPosition = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> vPosition = new Dictionary<string, List<string>>();

            input3.ToList().Sort();

            foreach (var pos in input3)
            {
                var temp = pos.Split('#');
                var rowKey = temp[0];
                var colKey = temp[1];
                if (!hPosition.ContainsKey(rowKey))
                {
                    hPosition.Add(rowKey, new List<string> { temp[1] });
                }
                else
                {
                    hPosition[rowKey].Add(temp[1]);
                }

                if (!vPosition.ContainsKey(colKey))
                {
                    vPosition.Add(colKey, new List<string> { temp[0] });
                }
                else
                {
                    vPosition[colKey].Add(temp[0]);
                }

                hPosition[rowKey].Sort();
                vPosition[colKey].Sort();
            }

            var result = new List<int>();
            var hPath = FindLength(hPosition);
            var vPath = FindLength(vPosition);

            result.Add(hPath);
            result.Add(vPath);

            var fREsult = result.Max();

            return fREsult >= 3 ? fREsult : 0;
        }

        private static int FindLength(Dictionary<string, List<string>> postions)
        {
            Dictionary<string, int> positions = new Dictionary<string, int>();
            foreach (var rowPos in postions)
            {
                var diff = new List<int>();

                for (int i = 0; i < rowPos.Value.Count; i++)
                {
                    if (i + 1 < rowPos.Value.Count - 1)
                    {
                        var res = Convert.ToInt32(rowPos.Value[i + 1]) - Convert.ToInt32(rowPos.Value[i]);
                        if (!diff.Contains(res))
                        {
                            diff.Add(res);
                        }
                    }
                }
                positions.Add(rowPos.Key, diff.Count);
            }

            var hRow = positions.Where(o => o.Value.Equals(1)).ToList();

            var maxpointPathLength = hRow.Select(x => postions.FirstOrDefault(o => o.Key.Equals(x.Key)).Value.Count).ToList();

            return maxpointPathLength.Any() ? maxpointPathLength.Max() : 0;
        }
    }
}
