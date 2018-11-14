using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashDictionary.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var dictionary = new HashDictionary<int, string>();
            dictionary[3] = "Willi";
            dictionary[5] = "Marianne";
            dictionary.Add(1, "Alex");

            Console.WriteLine($"[1] = {dictionary[1]}");
            Console.WriteLine($"[3] = {dictionary[3]}");
            Console.WriteLine($"[5] = {dictionary[5]}");

            // Check is element exists before printing it
            // V1:
            // if(dictionary.ContainsKey(7))
            // Console.WriteLine($"[7] = {dictionary[7]}");

            // V2:
            // string result;
            // if(dictionary.TryGetValue(7, out result))
            //   Console.WriteLine($"[7] = {result}");
            // else
            //   Console.WriteLine("[7] does not exist");

            // V3:
            if (dictionary.TryGetValue(7, out string result))
                Console.WriteLine($"[7] = {result}");
            else
                Console.WriteLine("[7] does not exist");

            foreach (KeyValuePair<int,string> pair in dictionary)
            {
                Console.WriteLine(pair);
            }

            Console.ReadLine();

        }
    }
}
