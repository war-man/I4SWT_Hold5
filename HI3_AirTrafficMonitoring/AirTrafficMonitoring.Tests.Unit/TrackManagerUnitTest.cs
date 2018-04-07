using AirTrafficMonitoring.Classes.TrackGenerator;
using AirTrafficMonitoring.Classes.TrackManager;
using AirTrafficMonitoring.Classes.TrackPrinter;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Tests.Unit
{
	[TestFixture]
	class TrackManagerUnitTest
	{
		private TrackManager _uut;

		private ITrackGenerator _fakeTrackGenerator;
		private ITrackPrinter _fakeTrackPrinter;

		[SetUp]
		public void Init()
		{
			_fakeTrackGenerator = Substitute.For<ITrackGenerator>();
			_fakeTrackPrinter = Substitute.For<ITrackPrinter>();

			_uut = new TrackManager(_fakeTrackGenerator, _fakeTrackPrinter);
		}
	}
}
