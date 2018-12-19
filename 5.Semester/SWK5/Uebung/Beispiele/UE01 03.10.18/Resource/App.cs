using System;
using System.Reflection;
using System.Ressources;
using System.Globalization;
using System.Threading;

class App {
    static void main (string[] args) {

        ResourceManager rm = new ResourceManager(
            "MyRes", Assembly.GetExecutingAssembly());
        
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

        Console.WriteLine(rm.GetString("Message"));

        Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-AT");

        Console.WriteLine(rm.GetString("Message"));
        
    }
}