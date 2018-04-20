using AirTrafficMonitoring.Classes.DataModels;

namespace AirTrafficMonitoring.Classes.Tracks
{
	public interface ITrackGenerator
	{
		Track GenerateTrack(TrackData trackData);
	}
}