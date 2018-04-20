using TransponderReceiver;

namespace AirTrafficMonitoring.Classes.AirTrafficController
{
	public interface IAirTrafficController
	{
		void OnTransponderDataReady(object sender, RawTransponderDataEventArgs e);
	}
}