using AirTrafficMonitoring.Classes.DataModels;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.Tracks
{
	public interface ITrackController
	{
		string AddTrackDataObjects(List<TrackData> trackDataList);
	}
}