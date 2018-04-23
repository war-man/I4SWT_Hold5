using System;

namespace AirTrafficMonitoring.Classes.Printer
{
	public class ConsolePrinter : IPrinter
	{
		public void WriteLine(string line)
		{
			Console.WriteLine(line);
		}

		public void Clear()
		{
			Console.Clear();
		}
	}
}