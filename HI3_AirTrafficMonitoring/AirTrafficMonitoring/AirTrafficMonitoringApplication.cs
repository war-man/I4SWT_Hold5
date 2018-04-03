using AirTrafficMonitoringClasses;
using TransponderReceiver;

namespace AirTrafficMonitoring
{
	class AirTrafficMonitoringApplication
	{
		static void Main(string[] args)
		{
			var transponderReceiver = new TestTransponderReceiver(TransponderReceiverFactory.CreateTransponderDataReceiver());
			transponderReceiver.Start();
		}
	}
}
