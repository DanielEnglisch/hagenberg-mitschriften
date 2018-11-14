using System;

class App
{
    static void Main(string[] args)
    {
        Calc calc = new Calc();

        Random r = new Random();
        for(int i = 0; i < 1000; ++i){
            calc.Add(r.NextDouble());
        }

        Console.WriteLine($"sum: {calc.GetSum():F2}");
    }
}