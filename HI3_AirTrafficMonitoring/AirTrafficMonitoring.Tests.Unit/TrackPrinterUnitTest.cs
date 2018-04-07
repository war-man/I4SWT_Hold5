using AirTrafficMonitoring.Classes.TrackPrinter;
using NUnit.Framework;

namespace AirTrafficMonitoring.Tests.Unit
{
	[TestFixture]
	class TrackPrinterUnitTest
	{
		private TrackPrinter _uut;

		[SetUp]
		public void Init()
		{
			_uut = new TrackPrinter();
		}
	}
}
