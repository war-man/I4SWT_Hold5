using AirTrafficMonitoring.Classes.AirTrafficController;
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
	class Step3_3
	{
		// Actual classes
		private IAirTrafficController _airTrafficController;
		private ITrackController _trackController;
		private ITrackDataObjectifier _trackDataObjectifier;
		private ICurrentTracksManager _currentTracksManager;

		// Fakes
		private ISeparationEventController _fakeSeparationEventController;
		private IPrinter _fakeConsolePrinter;

		[SetUp]
		public void Init()
		{
			_fakeSeparationEventController = Substitute.For<ISeparationEventController>();
			_fakeConsolePrinter = Substitute.For<IPrinter>();

			_currentTracksManager = new CurrentTracksManager();

			_trackController = new TrackController(
				_currentTracksManager,
				new TrackGenerator(),
				new TrackListFormatter());

			_trackDataObjectifier = new TrackDataObjectifier();

			_airTrafficController = new AirTrafficController(
				TransponderReceiverFactory.CreateTransponderDataReceiver(),
				_trackDataObjectifier,
				_fakeSeparationEventController,
				_trackController,
				_fakeConsolePrinter);
		}

		[Test]
		public void OnTransponderDataReady_EventArgsContains1Track_ObjectifiedTrackHasCorrectTag()
		{
			// Arrange
			const char delimitter = ';';

			const string tag = "tag";
			const int xCoordinate = 10000;
			const int yCoordinate = 11000;
			const int altitude = 12000;
			const string timestamp = "20180422172726873";
			var transponderData = tag + delimitter +
								  xCoordinate + delimitter +
								  yCoordinate + delimitter +
								  altitude + delimitter +
								  timestamp;

			var transponderDataList = new List<string>
			{
				transponderData
			};

			// Act
			_airTrafficController.OnTransponderDataReady(this, new RawTransponderDataEventArgs(transponderDataList));

			// Assert
			var track = _currentTracksManager.FindTrack(tag);
			Assert.That(track.Tag, Is.EqualTo(tag));
		}

		[Test]
		public void OnTransponderDataReady_EventArgsContains1Track_ObjectifiedTrackHasCurrentTrackWithCorrectXCoordinate()
		{
			// Arrange
			const char delimitter = ';';

			const string tag = "tag";
			const int xCoordinate = 10000;
			const int yCoordinate = 11000;
			const int altitude = 12000;
			const string timestamp = "20180422172726873";
			var transponderData = tag + delimitter +
								  xCoordinate + delimitter +
								  yCoordinate + delimitter +
								  altitude + delimitter +
								  timestamp;

			var transponderDataList = new List<string>
			{
				transponderData
			};

			// Act
			_airTrafficController.OnTransponderDataReady(this, new RawTransponderDataEventArgs(transponderDataList));

			// Assert
			var track = _currentTracksManager.FindTrack(tag);
			Assert.That(track.CurrentTrack.XCoordinate, Is.EqualTo(xCoordinate));
		}

		[Test]
		public void OnTransponderDataReady_EventArgsContains1Track_ObjectifiedTrackHasCurrentTrackWithCorrectYCoordinate()
		{
			// Arrange
			const char delimitter = ';';

			const string tag = "tag";
			const int xCoordinate = 10000;
			const int yCoordinate = 11000;
			const int altitude = 12000;
			const string timestamp = "20180422172726873";
			var transponderData = tag + delimitter +
								  xCoordinate + delimitter +
								  yCoordinate + delimitter +
								  altitude + delimitter +
								  timestamp;

			var transponderDataList = new List<string>
			{
				transponderData
			};

			// Act
			_airTrafficController.OnTransponderDataReady(this, new RawTransponderDataEventArgs(transponderDataList));

			// Assert
			var track = _currentTracksManager.FindTrack(tag);
			Assert.That(track.CurrentTrack.YCoordinate, Is.EqualTo(yCoordinate));
		}

		[Test]
		public void OnTransponderDataReady_EventArgsContains1Track_ObjectifiedTrackHasCurrentTrackWithCorrectAltitude()
		{
			// Arrange
			const char delimitter = ';';

			const string tag = "tag";
			const int xCoordinate = 10000;
			const int yCoordinate = 11000;
			const int altitude = 12000;
			const string timestamp = "20180422172726873";
			var transponderData = tag + delimitter +
								  xCoordinate + delimitter +
								  yCoordinate + delimitter +
								  altitude + delimitter +
								  timestamp;

			var transponderDataList = new List<string>
			{
				transponderData
			};

			// Act
			_airTrafficController.OnTransponderDataReady(this, new RawTransponderDataEventArgs(transponderDataList));

			// Assert
			var track = _currentTracksManager.FindTrack(tag);
			Assert.That(track.CurrentTrack.Altitude, Is.EqualTo(altitude));
		}

		[Test]
		public void OnTransponderDataReady_EventArgsContains1Track_ObjectifiedTrackHasCorrectTimestamp()
		{
			// Arrange
			const char delimitter = ';';

			const string tag = "tag";
			const int xCoordinate = 10000;
			const int yCoordinate = 11000;
			const int altitude = 12000;
			const string timestamp = "20180422172726873";
			var transponderData = tag + delimitter +
								  xCoordinate + delimitter +
								  yCoordinate + delimitter +
								  altitude + delimitter +
								  timestamp;

			var transponderDataList = new List<string>
			{
				transponderData
			};

			var expectedTimestamp = DateTime.ParseExact(timestamp, "yyyyMMddHHmmssfff",
				System.Globalization.CultureInfo.InvariantCulture);

			// Act
			_airTrafficController.OnTransponderDataReady(this, new RawTransponderDataEventArgs(transponderDataList));

			// Assert
			var track = _currentTracksManager.FindTrack(tag);
			Assert.That(track.CurrentTrack.Timestamp, Is.EqualTo(expectedTimestamp));
		}
	}
}
