using System;
using TransponderReceiver;

namespace AirTrafficMonitoringClasses
{
	public class TestTransponderReceiver
	{
		public TestTransponderReceiver(ITransponderReceiver transponderReceiver)
		{
			transponderReceiver.TransponderDataReady += TransponderReceiverOnTransponderDataReady;
		}

		public void Start()
		{
			while (true)
			{
				var keyPressed = Console.ReadKey();
				if (keyPressed.Key == ConsoleKey.Q)
					return;
			}
		}

		private void TransponderReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs rawTransponderDataEventArgs)
		{
			Console.Clear();

			var transponderData = rawTransponderDataEventArgs.TransponderData;
			foreach (var data in transponderData)
			{
				Console.WriteLine(data);
			}
		}
	}
}
