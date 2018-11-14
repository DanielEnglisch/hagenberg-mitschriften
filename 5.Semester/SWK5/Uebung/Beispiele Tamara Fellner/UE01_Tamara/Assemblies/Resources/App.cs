using System;
using System.Reflection; // for class Assembly
using System.Resources;
using System.Globalization;
using System.Threading;

class App {
   static void Main() {
		ResourceManager rm = new ResourceManager("MyRes", Assembly.GetExecutingAssembly());
		
		var uiCulture = Thread.CurrentThread.CurrentUICulture;     //Culture = Sprache des Betriebssystems
		Console.WriteLine($" Message: {rm.GetString("Message")} UI Culture: {uiCulture}");

		Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-AT");

		uiCulture = Thread.CurrentThread.CurrentUICulture;     //Culture = Sprache des Betriebssystems
		Console.WriteLine($" Message: {rm.GetString("Message")} UI Culture: {uiCulture}");
	}
}