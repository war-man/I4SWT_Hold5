using AirTrafficMonitoring.Classes.DataModels;
using System;

namespace AirTrafficMonitoring.Classes.SeparationEvents
{
	public interface ISeparationEventGenerator
	{
		SeparationEvent GenerateSeparationEvent(String track1, String track2, DateTime timestamp);
	}
}