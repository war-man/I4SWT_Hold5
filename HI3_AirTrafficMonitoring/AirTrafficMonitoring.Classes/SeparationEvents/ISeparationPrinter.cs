using AirTrafficMonitoring.Classes.DataModels;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.SeparationEvents
{
	public interface ISeparationPrinter
	{
		void PrintList(List<SeparationEvent> events);
	}
}