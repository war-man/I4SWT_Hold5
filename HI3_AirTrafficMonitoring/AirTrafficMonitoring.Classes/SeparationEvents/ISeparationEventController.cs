using AirTrafficMonitoring.Classes.DataModels;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.SeparationEvents
{
	public interface ISeparationEventController
	{
		List<SeparationEvent> CheckForSeparationEvents(List<Track> trackList);
		string GetFormattedSeparationEvents();
	}
}