using AirTrafficMonitoring.Classes.TrackDataModels;

namespace AirTrafficMonitoring.Classes.TrackGenerator
{
	public interface ITrackGenerator
	{
		Track GenerateTrack(TrackData trackData);
	}
}