using AirTrafficMonitoring.Classes.TrackDataModels;

namespace AirTrafficMonitoring.Classes.TrackGenerator
{
	public class TrackGenerator : ITrackGenerator
	{
		public Track GenerateTrack(TrackData trackData)
		{
			return new Track(trackData.Tag, trackData);
		}
	}
}