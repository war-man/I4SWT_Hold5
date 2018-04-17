using AirTrafficMonitoring.Classes.DataModels;
using System.Collections.Generic;
using System.Text;

namespace AirTrafficMonitoring.Classes.SeparationEvents
{
	public class SeparationEventListFormatter : ISeparationEventListFormatter
	{
		public string Format(List<SeparationEvent> separationEventList)
		{
			if (separationEventList.Count == 0) return "";

			var returnString = new StringBuilder();

			returnString.AppendLine($"Current number of separation events: {separationEventList.Count}.\n");
			foreach (var separationEvent in separationEventList)
			{
				returnString.AppendLine(separationEvent + "\n");
			}

			return returnString.ToString();
		}
	}
}
