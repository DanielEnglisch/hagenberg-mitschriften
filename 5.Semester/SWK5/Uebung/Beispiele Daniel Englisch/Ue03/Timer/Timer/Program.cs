using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timer
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer timer = new Timer();
            timer.Interval = TimeSpan.FromSeconds(0.5);

            // Registrieren mehrere Methoden im Event Delegate
            timer.Expired += MyMethod1;
            timer.Expired += MyMethod2;
            // Anonyme Methoden können nicht deregistriert werden, da keine referenz darauf verfügbar ist
            // Außer man speichert sie zuvor in einer Delegate Variable
            timer.Expired += (time) => Console.WriteLine("Anonymous " + time); 

            timer.Start();

            // Warten bis eine Taste gedrückt wurde
            Console.ReadLine();

            // Deregistrieren einer Methode
            timer.Expired -= MyMethod1;
        }

        public static void MyMethod1(DateTime time)
        {
            Console.WriteLine("Method1 " + time);
        }

        public static void MyMethod2(DateTime time)
        {
            Console.WriteLine("Method2 " + time);
        }
    }
}
