using AirTrafficMonitoring.Classes.TrackDataModels;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.TrackController
{
	public interface ITrackController
	{
		void AddTrackDataObjects(List<TrackData> trackDataList);
	}
}