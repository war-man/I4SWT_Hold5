using AirTrafficMonitoring.Classes.AirTrafficController;
using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Printer;
using AirTrafficMonitoring.Classes.SeparationEvents;
using AirTrafficMonitoring.Classes.Tracks;
using System;
using TransponderReceiver;

namespace AirTrafficMonitoring
{
	class AirTrafficMonitoringApplication
	{
		static void Main(string[] args)
		{
			var trackController = new TrackController(
				new CurrentTracksManager(),
				new TrackGenerator(),
				new TrackListFormatter());

			var separationEventController = new SeparationEventController(
				new CurrentSeparationEventsManager());

			var unused = new AirTrafficController(
				TransponderReceiverFactory.CreateTransponderDataReceiver(),
				new TrackDataObjectifier(),
				separationEventController,
				trackController,
				new ConsolePrinter(),
				new EventLogger());

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
