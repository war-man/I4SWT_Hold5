using AirTrafficMonitoring.Classes.TrackDataModels;
using NUnit.Framework;

namespace AirTrafficMonitoring.Tests.Unit
{
	[TestFixture]
	class TrackUnitTest
	{
		private Track _uut;

		[SetUp]
		public void Init()
		{
			_uut = new Track();
		}
	}
}
