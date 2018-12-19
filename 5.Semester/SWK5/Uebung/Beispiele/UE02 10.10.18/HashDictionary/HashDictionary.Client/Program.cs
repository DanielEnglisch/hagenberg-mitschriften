using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashDictionary;

namespace HashDictionary.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // to use hashmap you have to reference it first in the project
            // right click references -> add refernces -> projects -> HashDictionary
            // alternatively use strg + . and select add reference
            var dict = new HashDictionary<int, string>();

            dict[3] = "Willi";
            dict[5] = "Andi";
            dict.Add(1, "Franz");

            // V1
            //Console.WriteLine($"[1] = {dict[1]}");
            //Console.WriteLine($"[3] = {dict[3]}");
            //Console.WriteLine($"[5] = {dict[5]}");

            //if (dict.ContainsKey(7))
            //    Console.WriteLine($"[7] = {dict[7]}");
            //else
            //    Console.WriteLine("[7] does not exist");

            // V2
            string result;
            if (dict.TryGetValue(7, out result))
                Console.WriteLine($"[7] = {result}");
            else
                Console.WriteLine("[7] does not exist");

            // V3
            if (dict.TryGetValue(7, out string result2))
                Console.WriteLine($"[7] = {result2}");
            else
                Console.WriteLine("[7] does not exist");

            // iterate
            foreach (KeyValuePair<int, string> pair in dict)
            {
                Console.WriteLine(pair);
            }

            Console.WriteLine("Press any key to exit....");
            Console.ReadLine();
        }
    }
}
