using System;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Threading;

class App{
    static void Main(string[] args){
        ResourceManager rm = new ResourceManager(
            "MyRes",
            Assembly.GetExecutingAssembly()
        );

        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

        Console.WriteLine(rm.GetString("Message"));

        Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-DE");

        Console.WriteLine(rm.GetString("Message"));
    }
}