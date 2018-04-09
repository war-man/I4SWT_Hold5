using System;

namespace AirTrafficMonitoring.Classes.TrackPrinter
{
	public class ConsolePrinter : IPrinter
	{
		public void WriteLine(string String)
		{
			Console.WriteLine(String);
		}

		public void Clear()
		{
			Console.Clear();
		}

	}
}