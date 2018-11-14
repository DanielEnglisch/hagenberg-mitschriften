using System;

class App{
    static void Main(){
        Random rand = new Random();
        Calc calc = new Calc();
        //using(Calc calc = new Calc()){   
            for(int i = 0; i < 1000; i++){
                calc.Add(rand.NextDouble());
            }
            Console.WriteLine($"sum = {calc.GetSum()}");
        //}
    }// (mit using: calc.Dispose -> ~Calc) | (ohne using: !Calc ) wird hier implizit auferufen
}