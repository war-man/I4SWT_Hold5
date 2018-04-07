using AirTrafficMonitoring.Classes.TrackDataModels;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.TrackManager
{
	public interface ITrackManager
	{
		void AddTrackDataObjects(IEnumerable<TrackData> trackDataList);
	}
}