using AirTrafficMonitoring.Classes.TrackDataModels;
using NUnit.Framework;

namespace AirTrafficMonitoring.Tests.Unit
{
	[TestFixture]
	class TrackDataUnitTest
	{
		private TrackData _uut;

		[SetUp]
		public void Init()
		{
			_uut = new TrackData();
		}
	}
}
