using System;

namespace AirTrafficMonitoring.Classes.DataModels
{
	public class SeparationEvent
	{
		public SeparationEvent(string tag1, string tag2, DateTime timestamp)
		{
			Tag1 = tag1;
			Tag2 = tag2;
			Timestamp = timestamp;
		}

		public string Tag1 { get; set; }
		public string Tag2 { get; set; }
		public DateTime Timestamp { get; set; }

		public override string ToString()
		{
			return $"Track 1: {Tag1} | Track 2: {Tag2} | Timestamp: {Timestamp}";
		}
	}
}