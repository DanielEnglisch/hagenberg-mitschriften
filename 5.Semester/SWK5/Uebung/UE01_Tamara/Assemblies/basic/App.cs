using System;

internal class App{
    static void Main(){
        Random rand = new Random();
        AdvCalc calc = new AdvCalc();
        for(int i = 0; i < 1000; i++){
            calc.Add(rand.NextDouble());
        }
        Console.WriteLine($"Average: {calc.Average()}");
        //Console.WriteLine("Average: {0}",calc.Average());
    }
}