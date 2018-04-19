using AirTrafficMonitoring.Classes.DataModels;
using System;

namespace AirTrafficMonitoring.Classes.SeparationEvents
{
	public class SeparationEventGenerator : ISeparationEventGenerator
	{
		public SeparationEvent GenerateSeparationEvent(Track track1, Track track2, DateTime timestamp)
		{
			if (track1 == null ||
			    track2 == null ||
			    timestamp == default(DateTime) ||
			    track1.Tag == track2.Tag
			    ) return null;

			return new SeparationEvent(track1.Tag, track2.Tag, timestamp);
		}
	}
}
