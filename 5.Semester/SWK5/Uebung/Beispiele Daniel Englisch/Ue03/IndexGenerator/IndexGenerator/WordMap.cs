using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IndexGenerator
{
    using Entry = KeyValuePair<string, ISet<int>>;
    

    class WordMap
    {
        private IDictionary<string, ISet<int>> map = new Dictionary<string, ISet<int>>();

        public void AddWord(string word, int lineNr)
        {
            /* Simpler but less efficient:
            if (!map.ContainsKey(word))
            {
                map.Add(word, new SortedSet<int>());
            }
            map[word].Add(lineNr);
            */

            if (!map.TryGetValue(word, out ISet<int> lineNrSet))
            {
                lineNrSet = new SortedSet<int>();
                map.Add(word, lineNrSet);
            }

            lineNrSet.Add(lineNr);
        }

        public void PrintIndex(TextWriter output)
        {
            foreach(Entry entry in map)
            {
                output.Write($"{entry.Key}:");
                foreach(int lineNr in entry.Value)
                {
                    output.Write($" {lineNr}");
                }
                output.WriteLine();
            }
        }

        public IEnumerable<Entry> SortByFrequency()
        {
            return map.OrderByDescending(entry => entry.Value.Count);
        }

        public string FindMostFrequentWord()
        {
            // Ineffizient
            // return map.OrderByDescending(entry => entry.Value.Count).First().Key;

            // Mit Extension Method
            Comparison<Entry> comparison = (e1, e2) => e1.Value.Count.CompareTo(e2.Value.Count);
           // return IEnumerableExtension.MaxBy(map, comparison).Key;
            return map.MaxBy(comparison).Key;

        }

        public IEnumerable<string> FindAllWordsInLine(int line)
        {
            var result1 = map.Where(entry => entry.Value.Contains(line)).OrderBy(entry => entry.Key).Select(entry => entry.Key);

            // Using LINQ Syntax
            var result2 = from entry in map
                     where entry.Value.Contains(line)
                     orderby entry.Key
                     select entry.Key;

            return result2.ToList();
        }
    }
}
