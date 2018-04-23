using System;

namespace AirTrafficMonitoring.Classes.Printer
{
	public class ConsolePrinter : IPrinter
	{
		public void WriteLine(string stringToPrint)
		{
			Console.WriteLine(stringToPrint);
		}

		public void Clear()
		{
			Console.Clear();
		}
	}
}