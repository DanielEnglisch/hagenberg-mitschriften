using System;
using System.Reflection;

internal class App{
    static void Main(){
        Random rand = new Random();
        Calc calc = new Calc();
        for(int i = 0; i < 1000; i++){
            calc.Add(rand.NextDouble());
        }
        Console.WriteLine($"Sum: {calc.GetSum()}");        //--> Stringinterpolation
        //Console.WriteLine("Average: {0}",calc.Average());  --> Andere Schreibweise
        Console.WriteLine($"Assembly version of Calc: {Assembly.GetAssembly(typeof(Calc))}");
    }
}