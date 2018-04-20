using AirTrafficMonitoring.Classes.DataModels;
using System;

namespace AirTrafficMonitoring.Classes.SeparationEvents
{
	public interface ISeparationEventGenerator
	{
		SeparationEvent GenerateSeparationEvent(Track track1, Track track2, DateTime timestamp);
	}
}