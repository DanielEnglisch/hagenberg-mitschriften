using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timer_Delegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer t = new Timer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };

            // 4 a.
            t.Expired += OntimerExpired;

            // 4 b.
            // da anonyme methode -> kann nicht mehr dereferenziert werden
            //t.Expired +=
            //   signaledTime => Console.WriteLine($"Timer expired at {signaledTime:HH:mm:ss:ff} ");

            // 4 c.
            // workaround
            ExpiredEventHandler handler = signaledTime => Console.WriteLine($"Timer expired at {signaledTime:HH:mm:ss:ff} ");
            t.Expired += handler;

            t.Start();
            Console.ReadLine();

            // statische Methode deshalb wäre es egal mit Memory Leaks
            // Expired hat eine Referenz gespeichert, deshalb wird es vom GC nicht freigegeben
            // 5 a.
            // t.Expired -= OntimerExpired;

            // 5 b.
            // unmöglich zu dereferenzieren

            // 5 c.
            t.Expired -= handler;
        }

        private static void OntimerExpired(DateTime signaledTime)
        {
            Console.WriteLine($"Timer expired at {signaledTime:HH:mm:ss:ff} ");
        }
    }
}
