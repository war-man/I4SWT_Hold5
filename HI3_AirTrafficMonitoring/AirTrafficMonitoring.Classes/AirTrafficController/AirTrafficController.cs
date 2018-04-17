using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Printer;
using AirTrafficMonitoring.Classes.SeparationEvents;
using AirTrafficMonitoring.Classes.Tracks;
using TransponderReceiver;

namespace AirTrafficMonitoring.Classes.AirTrafficController
{
	public class AirTrafficController : IAirTrafficController
	{
		private readonly ITrackDataObjectifier _trackDataObjectifier;
		private readonly ISeparationEventController _separationEventController;
		private readonly ITrackController _trackController;
		private readonly IPrinter _consolePrinter;
		private readonly IPrinter _eventLogger;

		public AirTrafficController(
			ITransponderReceiver transponderReceiver,
			ITrackDataObjectifier trackDataObjectifier,
			ISeparationEventController separationEventController,
			ITrackController trackController,
			IPrinter consolePrinter,
			IPrinter eventLogger)
		{
			transponderReceiver.TransponderDataReady += OnTransponderDataReady;
			_trackDataObjectifier = trackDataObjectifier;
			_separationEventController = separationEventController;
			_trackController = trackController;
			_consolePrinter = consolePrinter;
			_eventLogger = eventLogger;
		}

		public void OnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
		{
			// Objectify data received from TransponderReceiver
			var trackDataList = _trackDataObjectifier.Objectify(e.TransponderData);

			// Update tracks with given data
			var currentTracks = _trackController.AddTrackDataObjects(trackDataList);

			// Check for separation events
			var separationEvents = _separationEventController.CheckForSeparationEvents(currentTracks);

			// Print current tracks to console
			if (currentTracks?.Count > 0)
			{
				_consolePrinter.Clear();
				_consolePrinter.WriteLine("Current tracks:\n");
				_consolePrinter.WriteLine(_trackController.GetFormattedCurrentTracks());
			}

			// Print separation events to console and file
			if (separationEvents?.Count > 0)
			{
				_consolePrinter.WriteLine("\n==================================================\n");
				_consolePrinter.WriteLine("Current separation events:\n");
				_consolePrinter.WriteLine(_separationEventController.GetFormattedSeparationEvents());

				_eventLogger.WriteLine(_separationEventController.GetFormattedSeparationEvents());
			}
		}
	}
}
