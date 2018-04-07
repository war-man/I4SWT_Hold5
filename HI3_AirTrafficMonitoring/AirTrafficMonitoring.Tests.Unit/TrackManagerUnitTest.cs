using AirTrafficMonitoring.Classes.TrackManager;
using NUnit.Framework;

namespace AirTrafficMonitoring.Tests.Unit
{
	[TestFixture]
	class TrackManagerUnitTest
	{
		private TrackManager _uut;

		[SetUp]
		public void Init()
		{
			_uut = new TrackManager();
		}
	}
}
