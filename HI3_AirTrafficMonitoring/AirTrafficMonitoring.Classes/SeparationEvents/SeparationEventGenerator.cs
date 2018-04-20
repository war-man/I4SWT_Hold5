using AirTrafficMonitoring.Classes.DataModels;
using System;

namespace AirTrafficMonitoring.Classes.SeparationEvents
{
	public class SeparationEventGenerator : ISeparationEventGenerator
	{
		public SeparationEvent GenerateSeparationEvent(String track1, String track2, DateTime timestamp)
		{
			if (track1 == null ||
			    track2 == null ||
			    timestamp == default(DateTime) ||
			    track1 == track2
			    ) return null;

			return new SeparationEvent(track1, track2, timestamp);
		}
	}
}
