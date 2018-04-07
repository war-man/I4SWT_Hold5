using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.TrackManager;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitoring.Tests.Unit
{
	[TestFixture]
	class TrackDataObjectifierUnitTest
	{
		private TrackDataObjectifier _uut;

		private ITransponderReceiver _fakeTransponderReceiver;
		private ITrackManager _fakeTrackManager;

		[SetUp]
		public void Init()
		{
			_fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
			_fakeTrackManager = Substitute.For<ITrackManager>();

			_uut = new TrackDataObjectifier(_fakeTransponderReceiver, _fakeTrackManager);
		}
	}
}
