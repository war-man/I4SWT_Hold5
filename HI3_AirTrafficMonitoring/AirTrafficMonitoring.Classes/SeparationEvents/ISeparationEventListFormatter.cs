using AirTrafficMonitoring.Classes.DataModels;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.SeparationEvents
{
	public interface ISeparationEventListFormatter
	{
		string Format(List<SeparationEvent> separationEventList);
	}
}