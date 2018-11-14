using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateSyntax
{

    // Delegate Definition für eine Prozedur (void) ohne Parameter
    delegate void Procedure();

    // Delegate mit Parameter
    delegate void ProcedureWithIntParam(int value);

    // Generische Lösung für alle Methodne mit einem Parameter
    delegate void ProcedureWithOneParameter<T>(T value);

    // Delegate mit Rückgabewert
    delegate int IntFunction();

    // Generische Function
    delegate TReturn GenericFunction<TParameter, TReturn>(TParameter);

    class Program
    {
        static void Main(string[] args)
        {
            // Anlegen des oben definierten Delegate Typs
            // Zuweisen einer auf die Signatur passenden Methode
            Procedure p1 = SayHello;
            ProcedureWithIntParam p2 = PrintInt;
            ProcedureWithOneParameter<int> p3 = PrintInt;

            // Es kann auch eine wo anders definierte Funktion zugewiesen werden
            var random = new Random();
            IntFunction f1 = random.Next;

            // int.Parse nimmt einen String uns liefert einen int
            GenericFunction<string, int> f2 = int.Parse;

            // Aufruf der gespeicherten Methode
            p1();
            p2(42);
            p3(-23);
            Console.WriteLine("Random Number: " + f1());
            Console.WriteLine("String 42 to int is " + f2("42"));

            // Lambda Ausdruck
            p2 = (value) => Console.WriteLine("HalfLife WTF -> " + value);
            p2(33);

            // .NET Framework bringt generische Delegates bereits mit
            // Procedures sind Actions
            Action<int> a1 = PrintInt;
            // Functionen sind Funcs wobei der letzte generische Parameter der Rückgabewert ist
            Func<string, int> f3 = int.Parse;
        }

        private static void SayHello()
        {
            Console.WriteLine("Hello");
        }

        private static void PrintInt(int value)
        {
            Console.WriteLine(value);
        }
    }
}
