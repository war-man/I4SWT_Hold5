using AirTrafficMonitoring.Classes.TrackGenerator;
using NUnit.Framework;

namespace AirTrafficMonitoring.Tests.Unit
{
	[TestFixture]
	class TrackGeneratorUnitTest
	{
		private TrackGenerator _uut;

		[SetUp]
		public void Init()
		{
			_uut = new TrackGenerator();
		}
	}
}
