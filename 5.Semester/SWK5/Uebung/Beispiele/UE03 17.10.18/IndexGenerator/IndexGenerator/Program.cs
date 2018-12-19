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
                //using (TextReader input = Console.In) // CTRL + Z
                //using (TextWriter output = Console.Out)
                using (TextReader input = new StreamReader(File.OpenRead("in.txt"), Encoding.Default))
                using (TextWriter output = new StreamWriter(File.OpenWrite("out.txt")))
                {
                    WordMap wordMap = new WordMap();

                    int lineNr = 1;
                    string line;

                    while ((line = input.ReadLine()) != null)
                    {
                        string[] words = line.Split(new[] { ' ', ',', '.', ';', '-' }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string s in words)
                            wordMap.AddWord(s, lineNr);

                        ++lineNr;
                    }

                    wordMap.Print(output);

                    Console.WriteLine("SortByFrequency:");
                    foreach(var entry in wordMap.SortByFrequency())
                        Console.WriteLine($"{entry.Key}: {entry.Value.Count} ");
                    foreach (var word in wordMap.FindAllWordsInLine(2))
                    {
                        Console.WriteLine(word);
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
