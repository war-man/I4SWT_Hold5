using AirTrafficMonitoring.Classes;
using NUnit.Framework;

namespace AirTrafficMonitoring.Tests.Unit
{
	[TestFixture]
	class TrackDataObjectifierUnitTest
	{
		private TrackDataObjectifier _uut;

		[SetUp]
		public void Init()
		{
			_uut = new TrackDataObjectifier();
		}
	}
}
