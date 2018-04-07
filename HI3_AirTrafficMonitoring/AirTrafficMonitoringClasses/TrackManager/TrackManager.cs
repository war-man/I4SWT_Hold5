using AirTrafficMonitoring.Classes.TrackDataModels;
using AirTrafficMonitoring.Classes.TrackGenerator;
using AirTrafficMonitoring.Classes.TrackPrinter;
using System.Collections.Generic;
using System.Linq;

namespace AirTrafficMonitoring.Classes.TrackManager
{
	public class TrackManager : ITrackManager
	{
		private readonly ITrackGenerator _trackGenerator;
		private readonly ITrackPrinter _trackPrinter;
		private readonly List<Track> _currentTracks;

		public TrackManager(ITrackGenerator trackGenerator, ITrackPrinter trackPrinter)
		{
			_trackGenerator = trackGenerator;
			_trackPrinter = trackPrinter;

			_currentTracks = new List<Track>();
		}

		public void AddTrackDataObjects(IEnumerable<TrackData> trackDataList)
		{
			foreach (var trackData in trackDataList)
			{
				var tracks = (from track in _currentTracks
							  where track.Tag == trackData.Tag
							  select track).ToList();

				if (!tracks.Any())
				{
					_currentTracks.Add(_trackGenerator.GenerateTrack(trackData));
				}
				else
				{
					tracks.First().AddNewTrackData(trackData);
				}
			}

			_trackPrinter.Print(_currentTracks);
		}
	}
}