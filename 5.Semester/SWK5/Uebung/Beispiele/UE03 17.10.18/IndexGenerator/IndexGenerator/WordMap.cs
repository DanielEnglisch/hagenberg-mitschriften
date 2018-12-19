using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IndexGenerator
{
    using Entry = KeyValuePair<string, ISet<int>>;

    public class WordMap
    {
        private IDictionary<string, ISet<int>> map = new Dictionary<string, ISet<int>>();

        public void AddWord(string word, int lineNr)
        {
            // simpled but less efficient
            //if (!map.ContainsKey(word))
            //    map.Add(word, new SortedSet<int>());

            //map[word].Add(lineNr);

            // this is more efficient because map is only accessed once
            // if there already is an entry for a word.
            ISet<int> lineNrSet;
            if (!map.TryGetValue(word, out lineNrSet))
            {
                lineNrSet = new SortedSet<int>();
                map.Add(word, lineNrSet);
            }

            lineNrSet.Add(lineNr);
        }

        public void Print(TextWriter output)
        {
            // entry ist ein KeyValuePair<string, ISet<int>>
            foreach (Entry entry in map)
            {
                output.Write($"{entry.Key}: ");
                foreach (int lineNr in entry.Value)
                    output.Write($"{lineNr} ");
                output.WriteLine();
            }
        }

        public IEnumerable<Entry> SortByFrequency()
        {
            return map.OrderByDescending(entry => entry.Value.Count);
        }

        public string FindMostFrequentWord()
        {
            // V1
            // nicht sehr performant, da sortiert wird
            // return map.OrderByDescending(entry => entry.Value.Count).First().Key;

            // V2
             Comparison<Entry> comparison =
                (e1, e2) => e1.Value.Count.CompareTo(e2.Value.Count);

            // V2.1
            // return IEnumerableExtensions.MaxBy(map, comparison).Key;

            // V2.2
            // return map.MaxBy(comparison).Key;

            // V2.3
            return map.MaxBy((e1, e2) => e1.Value.Count.CompareTo(e2.Value.Count)).Key;
        }

        public IEnumerable<string> FindAllWordsInLine(int line)
        {
            /*
              Enumerable.ToList(
                Enumerable.Select(
                    Enumerable.OrderBy(
                        Enumerable.Where(
                            map,
                            entry => entry.Value.Contains(line)),
                        entry => entry.Key),
                    entry => entry.Key));
            */

            /*
               ohne ToList() kommt IEnumerable zurück, damit wird die Filterung und Sortierung
               erst durchgeführt wenn darüber iteriert wird bzw wenn result.ToList() ausgeführt wird
               Es sein das der User mehrfach drüber iteriert, damit wird diese 
               komplexe operation bei jeder Iteration neu ausgeführt.
               Wenn jedoch eine List zurückgegeben wird, wird diese abfrage nur einmal durchgeführt!
            */
            /*
               var result = map.Where(entry => entry.Value.Contains(line))
                .OrderBy(entry => entry.Key)
                .Select(entry => entry.Key)
                .ToList();
             */

            var result = from entry in map
                         where entry.Value.Contains(line)
                         orderby entry.Key
                         select entry.Key;

            return result.ToList(); 
        }
    }
}