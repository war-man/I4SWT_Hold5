using AirTrafficMonitoring.Classes.DataModels;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.Tracks
{
	public class TrackController : ITrackController
	{
		private readonly ICurrentTracksManager _currentTracksManager;
		private readonly ITrackGenerator _trackGenerator;
		private readonly ITrackListFormatter _trackListFormatter;

		private const int XBoundarySouthWest = 10000;
		private const int YBoundarySouthWest = 10000;
		private const int XBoundaryNorthEast = 90000;
		private const int YBoundaryNorthEast = 90000;
		private const int ZBoundaryLower = 500;
		private const int ZBoundaryUpper = 20000;

		public TrackController(
			ICurrentTracksManager currentTracksManager,
			ITrackGenerator trackGenerator,
			ITrackListFormatter formatter)
		{
			_currentTracksManager = currentTracksManager;
			_trackGenerator = trackGenerator;
			_trackListFormatter = formatter;
		}

		public List<Track> AddTrackDataObjects(List<TrackData> trackDataList)
		{
			if (trackDataList == null) return null;

			// Find tracks that are no longer sends a transponder signal
			var tracksToRemove = new List<Track>();
			foreach (var trackData in _currentTracksManager.CurrentTracks)
			{
				var track = trackDataList.Find(t => t.Tag == trackData.Tag);

				if (track == null)
				{
					tracksToRemove.Add(trackData);
				}
			}

			// Remove tracks that are no longer sends a transponder signal
			foreach (var track in tracksToRemove)
			{
				_currentTracksManager.RemoveTrack(track);
			}

			// Update current tracks or add new tracks if track has not sent a transponder signal before
			foreach (var trackData in trackDataList)
			{
				// Check that track is within the boundaries of the monitored area
				if (!CheckCoordinates(trackData.XCoordinate, trackData.YCoordinate) ||
					!CheckAltitude(trackData.Altitude)) continue;

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

			return _currentTracksManager.CurrentTracks;
		}

		public string GetFormattedCurrentTracks()
		{
			return _trackListFormatter.Format(_currentTracksManager.CurrentTracks);
		}

		private bool CheckCoordinates(int x, int y)
		{
			return x >= XBoundarySouthWest && x <= XBoundaryNorthEast &&
				   y >= YBoundarySouthWest && y <= YBoundaryNorthEast;
		}

		private bool CheckAltitude(int altitude)
		{
			return altitude >= ZBoundaryLower && altitude <= ZBoundaryUpper;
		}
	}
}
