using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.TrackGenerator;
using AirTrafficMonitoring.Classes.TrackManager;
using AirTrafficMonitoring.Classes.TrackPrinter;
using System;
using TransponderReceiver;

namespace AirTrafficMonitoring
{
	class AirTrafficMonitoringApplication
	{
		static void Main(string[] args)
		{
			new TrackDataObjectifier(TransponderReceiverFactory.CreateTransponderDataReceiver(),
				new TrackManager(new TrackGenerator(), new TrackPrinter()));

			while (true)
			{
				Console.WriteLine("Welcome to the AirTrafficMonitoring application. Press 'Q' to quit.");
				var keyPressed = Console.ReadKey();
				if (keyPressed.Key == ConsoleKey.Q)
					return;
			}
		}
	}
}
