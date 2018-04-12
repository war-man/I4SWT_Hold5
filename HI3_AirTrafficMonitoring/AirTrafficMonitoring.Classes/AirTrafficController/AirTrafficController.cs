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
			var formattedCurrentTracks = _trackController.AddTrackDataObjects(trackDataList);

			// Print tracks
			_consolePrinter.Clear();
			_consolePrinter.WriteLine(formattedCurrentTracks);
		}
	}
}
