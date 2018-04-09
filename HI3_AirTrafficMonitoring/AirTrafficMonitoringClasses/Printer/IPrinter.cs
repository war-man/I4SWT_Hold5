using AirTrafficMonitoring.Classes.TrackDataModels;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.TrackPrinter
{
	public interface IPrinter
	{
		void WriteLine(string String);
		void Clear();
	}
}