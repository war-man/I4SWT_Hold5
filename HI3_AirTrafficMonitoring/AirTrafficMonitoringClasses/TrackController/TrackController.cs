using AirTrafficMonitoring.Classes.CurrentTracksManager;
using AirTrafficMonitoring.Classes.TrackDataModels;
using AirTrafficMonitoring.Classes.TrackGenerator;
using System.Collections.Generic;
using AirTrafficMonitoring.Classes.Printer;

namespace AirTrafficMonitoring.Classes.TrackController
{
	public class TrackController : ITrackController
	{
		private readonly ITrackGenerator _trackGenerator;
		private readonly ITrackListFormatter _formatter;
		private readonly IPrinter _printer;
		private readonly ICurrentTracksManager _currentTracksManager;

		public TrackController(
			ICurrentTracksManager currentTracksManager,
			ITrackGenerator trackGenerator,
			ITrackListFormatter formatter,
			IPrinter printer)
		{
			_currentTracksManager = currentTracksManager;
			_trackGenerator = trackGenerator;
			_formatter = formatter;
			_printer = printer;
		}

		public void AddTrackDataObjects(List<TrackData> trackDataList)
		{
			if (trackDataList == null) return;

			// Find tracks that are no longer in the area
			var tracksToRemove = new List<Track>();
			foreach (var trackData in _currentTracksManager.CurrentTracks)
			{
				var track = trackDataList.Find(t => t.Tag == trackData.Tag);

				if (track == null)
				{
					tracksToRemove.Add(trackData);
				}
			}

			// Remove tracks that are no longer in the area
			foreach (var track in tracksToRemove)
			{
				_currentTracksManager.RemoveTrack(track);
			}

			// Update current tracks or add new tracks if track is not already in the area
			foreach (var trackData in trackDataList)
			{
				var track = _currentTracksManager.FindTrack(trackData.Tag);

				if (track == null)
				{
					_currentTracksManager.AddTrack(_trackGenerator.GenerateTrack(trackData));
				}
				else
				{
					_currentTracksManager.UpdateTrack(trackData);
				}
			}

			if (_currentTracksManager.GetTrackCount() > 0)
			{
				string formattedString = _formatter.Format(_currentTracksManager.CurrentTracks);

				_printer.Clear();
				_printer.WriteLine(formattedString);
			}
				
		}
	}
}
