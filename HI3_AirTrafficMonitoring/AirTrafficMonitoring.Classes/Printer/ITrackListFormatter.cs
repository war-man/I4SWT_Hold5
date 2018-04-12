using System.Collections.Generic;
using AirTrafficMonitoring.Classes.TrackDataModels;

namespace AirTrafficMonitoring.Classes.Printer
{
	public interface ITrackListFormatter
	{
		string Format(List<Track> trackList);
	}
}