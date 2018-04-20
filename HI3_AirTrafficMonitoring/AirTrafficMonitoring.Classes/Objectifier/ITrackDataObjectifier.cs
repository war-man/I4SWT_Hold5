using System.Collections.Generic;
using AirTrafficMonitoring.Classes.DataModels;

namespace AirTrafficMonitoring.Classes.Objectifier
{
	public interface ITrackDataObjectifier
	{
		List<TrackData> Objectify(List<string> transponderData);
	}
}