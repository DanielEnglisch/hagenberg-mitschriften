using System;

class App {
    static void main(string[] args) {

        Calc c = new Calc();
        c.Add(5);
        c.Add(5);

        c.Dispose();
        // Dispose wird nicht aufgerufen falls eine exception davor passiert
        // deshalb try finally
        // im finally wird dispose aufgerufen.
        // falls im constructor exception passiert dann ist die referenz null
        // deshalb muss im finally auf null überprüft werden
        // oder using verwenden
        /*
        Calc c;
        try{
            c = new Calc();
        }
        finally {
            if (c!= null) c.Dispose();
            // oder c?.Dispose();
        }

        using (alc c = new Calc()) {

        }
         */

        Console.WriteLine($"sum: {c.GetSum()}");
        Console.WriteLine("before GC");
        GC.Collect();
        Console.WriteLine("after gc");
        // csc /t:exe /r:Calc.dll App.cs
    }
}