using System;

class App
{
    static void Main(string[] args)
    {
        AdvCalc calc = new AdvCalc();

        Random r = new Random();
        for(int i = 0; i < 1000; ++i){
            calc.Add(r.NextDouble());
        }

        Console.WriteLine($"sum: {calc.GetSum():F2}");
        Console.WriteLine("avg: {0:F2}", calc.GetAverage());
    }
}