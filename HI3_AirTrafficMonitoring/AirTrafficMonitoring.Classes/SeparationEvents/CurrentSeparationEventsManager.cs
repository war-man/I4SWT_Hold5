using AirTrafficMonitoring.Classes.DataModels;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.SeparationEvents
{
	public class CurrentSeparationEventsManager : ICurrentSeparationEventsManager
	{
		public CurrentSeparationEventsManager()
		{
			CurrentEvents = new List<SeparationEvent>();
		}

		public List<SeparationEvent> CurrentEvents { get; }

		public void AddEvent(SeparationEvent separationEvent)
		{
			if (separationEvent != null && separationEvent.Tag1 != separationEvent.Tag2)
				CurrentEvents.Add(separationEvent);
		}

		public SeparationEvent FindEvent(string tag1, string tag2)
		{
			if (tag1 == null || tag2 == null || tag1 == tag2) return null;

			return CurrentEvents.Find(e => (e.Tag1 == tag1 || e.Tag1 == tag2) &&
										   (e.Tag2 == tag1 || e.Tag2 == tag2));
		}

		public int GetEventCount()
		{
			return CurrentEvents.Count;
		}

		public void RemoveEvent(string tag1, string tag2)
		{
			var separationEvent = FindEvent(tag1, tag2);

			if (separationEvent != null)
				CurrentEvents.Remove(separationEvent);
		}
	}
}