using AirTrafficMonitoring.Classes.AirTrafficController;
using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Printer;
using AirTrafficMonitoring.Classes.SeparationEvents;
using AirTrafficMonitoring.Classes.Tracks;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TransponderReceiver;

namespace AirTrafficMonitoring.Tests.Integration.Step3_CompleteSystem
{
	[TestFixture]
	class Step3_1
	{
		// Actual classes
		private IAirTrafficController _airTrafficController;
		private ITrackController _trackController;
		private ICurrentTracksManager _currentTracksManager;

		// Fakes
		private ISeparationEventController _fakeSeparationEventController;
		private ITrackDataObjectifier _fakeTrackDataObjectifier;
		private IPrinter _fakeConsolePrinter;

		// Boundaries for monitored area
		private const int XBoundarySouthWest = 10000;
		private const int YBoundarySouthWest = 10000;
		private const int XBoundaryNorthEast = 90000;
		private const int YBoundaryNorthEast = 90000;
		private const int ZBoundaryLower = 500;
		private const int ZBoundaryUpper = 20000;

		[SetUp]
		public void Init()
		{
			_fakeSeparationEventController = Substitute.For<ISeparationEventController>();
			_fakeTrackDataObjectifier = Substitute.For<ITrackDataObjectifier>();
			_fakeConsolePrinter = Substitute.For<IPrinter>();

			_currentTracksManager = new CurrentTracksManager();

			_trackController = new TrackController(
				_currentTracksManager,
				new TrackGenerator(),
				new TrackListFormatter());

			_airTrafficController = new AirTrafficController(
				TransponderReceiverFactory.CreateTransponderDataReceiver(),
				_fakeTrackDataObjectifier,
				_fakeSeparationEventController,
				_trackController,
				_fakeConsolePrinter);
		}

		[Test]
		public void OnTransponderDataReady_TransponderDataContains1TrackInsideArea_TrackIsAddedToCurrentTracksManager()
		{
			// Arrange
			const string tag = "someTag";
			var trackDataList = new List<TrackData>
			{
				new TrackData(tag, XBoundarySouthWest, YBoundarySouthWest, ZBoundaryLower, DateTime.Now)
			};

			_fakeTrackDataObjectifier.Objectify(Arg.Any<List<string>>()).Returns(trackDataList);

			// Act
			_airTrafficController.OnTransponderDataReady(this, new RawTransponderDataEventArgs(new List<string>()));

			// Assert
			var track = _currentTracksManager.FindTrack(tag);
			Assert.NotNull(track);
		}

		[Test]
		public void OnTransponderDataReady_TransponderDataContains5TracksInsideArea_CurrentTracksListCountIs5()
		{
			// Arrange
			var trackDataList = new List<TrackData>
			{
				new TrackData("tag1", XBoundarySouthWest, YBoundaryNorthEast, ZBoundaryLower, DateTime.Now),
				new TrackData("tag2", XBoundaryNorthEast, YBoundarySouthWest, ZBoundaryUpper, DateTime.Now),
				new TrackData("tag3", XBoundarySouthWest, YBoundaryNorthEast, ZBoundaryLower, DateTime.Now),
				new TrackData("tag4", XBoundaryNorthEast, YBoundarySouthWest, ZBoundaryUpper, DateTime.Now),
				new TrackData("tag5", XBoundarySouthWest, YBoundaryNorthEast, ZBoundaryLower, DateTime.Now)
			};

			_fakeTrackDataObjectifier.Objectify(Arg.Any<List<string>>()).Returns(trackDataList);

			// Act
			_airTrafficController.OnTransponderDataReady(this, new RawTransponderDataEventArgs(new List<string>()));

			// Assert
			Assert.That(_currentTracksManager.GetTrackCount(), Is.EqualTo(5));
		}

		[Test]
		public void OnTransponderDataReady_TransponderDataContains1TrackOutsideArea_TrackIsNotAddedToCurrentTracksManager()
		{
			// Arrange
			const string tag = "someTag";
			var trackDataList = new List<TrackData>
			{
				new TrackData(tag, XBoundarySouthWest - 1, YBoundarySouthWest, ZBoundaryLower, DateTime.Now)
			};

			_fakeTrackDataObjectifier.Objectify(Arg.Any<List<string>>()).Returns(trackDataList);

			// Act
			_airTrafficController.OnTransponderDataReady(this, new RawTransponderDataEventArgs(new List<string>()));

			// Assert
			var track = _currentTracksManager.FindTrack(tag);
			Assert.Null(track);
		}

		[Test]
		public void OnTransponderDataReady_TransponderDataContains5TracksOutsideArea_CurrentTracksListCountIs0()
		{
			// Arrange
			var trackDataList = new List<TrackData>
			{
				new TrackData("tag1", XBoundarySouthWest - 1, YBoundaryNorthEast, ZBoundaryLower, DateTime.Now),
				new TrackData("tag2", XBoundaryNorthEast + 1, YBoundarySouthWest, ZBoundaryUpper, DateTime.Now),
				new TrackData("tag3", XBoundarySouthWest - 1, YBoundaryNorthEast, ZBoundaryLower, DateTime.Now),
				new TrackData("tag4", XBoundaryNorthEast + 1, YBoundarySouthWest, ZBoundaryUpper, DateTime.Now),
				new TrackData("tag5", XBoundarySouthWest - 1, YBoundaryNorthEast, ZBoundaryLower, DateTime.Now)
			};

			_fakeTrackDataObjectifier.Objectify(Arg.Any<List<string>>()).Returns(trackDataList);

			// Act
			_airTrafficController.OnTransponderDataReady(this, new RawTransponderDataEventArgs(new List<string>()));

			// Assert
			Assert.That(_currentTracksManager.GetTrackCount(), Is.EqualTo(0));
		}
	}
}
