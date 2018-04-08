using AirTrafficMonitoring.Classes.CurrentTracksManager;
using AirTrafficMonitoring.Classes.TrackDataModels;
using AirTrafficMonitoring.Classes.TrackGenerator;
using AirTrafficMonitoring.Classes.TrackPrinter;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.TrackController
{
	public class TrackController : ITrackController
	{
		private readonly ITrackGenerator _trackGenerator;
		private readonly ITrackPrinter _trackPrinter;
		private readonly ICurrentTracksManager _currentTracksManager;

		public TrackController(
			ICurrentTracksManager currentTracksManager,
			ITrackGenerator trackGenerator,
			ITrackPrinter trackPrinter)
		{
			_currentTracksManager = currentTracksManager;
			_trackGenerator = trackGenerator;
			_trackPrinter = trackPrinter;
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
				_trackPrinter.Print(_currentTracksManager.CurrentTracks);
		}
	}
}