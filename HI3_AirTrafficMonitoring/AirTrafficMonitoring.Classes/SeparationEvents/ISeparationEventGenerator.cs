using AirTrafficMonitoring.Classes.DataModels;
using System;

namespace AirTrafficMonitoring.Classes.SeparationEvents
{
	public interface ISeparationEventGenerator
	{
		SeparationEvent GenerateSeparationEvent(string tag1, string tag2, DateTime timestamp);
	}
}