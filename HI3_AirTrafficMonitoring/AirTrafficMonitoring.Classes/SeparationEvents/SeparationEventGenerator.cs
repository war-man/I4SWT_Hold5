using AirTrafficMonitoring.Classes.DataModels;
using System;

namespace AirTrafficMonitoring.Classes.SeparationEvents
{
	public class SeparationEventGenerator : ISeparationEventGenerator
	{
		public SeparationEvent GenerateSeparationEvent(string tag1, string tag2, DateTime timestamp)
		{
			if (tag1 == null || tag2 == null || timestamp == default(DateTime) || tag1 == tag2)
				return null;

			return new SeparationEvent(tag1, tag2, timestamp);
		}
	}
}
