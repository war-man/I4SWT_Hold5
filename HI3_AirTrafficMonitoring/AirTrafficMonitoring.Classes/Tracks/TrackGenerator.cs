using AirTrafficMonitoring.Classes.DataModels;

namespace AirTrafficMonitoring.Classes.Tracks
{
	public class TrackGenerator : ITrackGenerator
	{
		public Track GenerateTrack(TrackData trackData)
		{
			if (trackData == null) return null;

			return new Track(trackData.Tag, trackData);
		}
	}
}