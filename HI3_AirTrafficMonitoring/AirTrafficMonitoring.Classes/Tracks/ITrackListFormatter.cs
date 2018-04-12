using AirTrafficMonitoring.Classes.DataModels;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.Tracks
{
	public interface ITrackListFormatter
	{
		string Format(List<Track> trackList);
	}
}