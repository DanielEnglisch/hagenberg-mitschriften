using System;
using System.IO;
using System.Text;

namespace IndexGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //using (TextReader input = Console.In)
                //using (TextWriter output = Console.Out)
                using (TextReader input = new StreamReader(File.OpenRead("in.txt"), Encoding.Default))
                using (TextWriter output = new StreamWriter(File.OpenWrite("out.txt")))
                {
                    WordMap wordMap = new WordMap();

                    string line;
                    int lineNr = 1;

                    while((line = input.ReadLine())!= null)
                    {
                        string[] words = line.Split(new[] {' ', '.', '?', '!', ','}, StringSplitOptions.RemoveEmptyEntries);
                        foreach(string word in words)
                        {
                            wordMap.AddWord(word, lineNr);
                        }
                        lineNr++;
                    }

                    wordMap.PrintIndex(output);

                    Console.WriteLine();
                    Console.WriteLine("Sort by Frequency:");
                    foreach(var entry in wordMap.SortByFrequency())
                    {
                        Console.WriteLine($"{entry.Key}: {entry.Value.Count}");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }
    }
}
