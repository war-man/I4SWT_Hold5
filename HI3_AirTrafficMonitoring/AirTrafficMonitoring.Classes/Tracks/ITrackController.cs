using AirTrafficMonitoring.Classes.DataModels;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.Tracks
{
	public interface ITrackController
	{
		List<Track> AddTrackDataObjects(List<TrackData> trackDataList);
		string GetFormattedCurrentTracks();
	}
}