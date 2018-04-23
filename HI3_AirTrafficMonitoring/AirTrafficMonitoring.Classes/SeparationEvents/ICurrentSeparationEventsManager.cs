using AirTrafficMonitoring.Classes.DataModels;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.SeparationEvents
{
	public interface ICurrentSeparationEventsManager
	{
		List<SeparationEvent> CurrentEvents { get; }
		int EventCount { get; }

		void AddEvent(SeparationEvent separationEvent);
		SeparationEvent FindEvent(string tag1, string tag2);
		void RemoveEvent(string tag1, string tag2);
	}
}