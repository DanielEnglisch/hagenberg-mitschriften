using System;

class App{
    static void Main(string[] args){
        Calc c = new Calc();

        c.Add(5);
        c.Add(5);
        c.Add(5);

        Console.WriteLine($"sum: {c.GetSum()}");

        Console.WriteLine("before GC");
        GC.Collect();
        Console.WriteLine("after GC");

    }
}