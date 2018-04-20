using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.Tracks;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Tests.Unit.Tracks
{
	[TestFixture]
	class TrackControllerUnitTest
	{
		private TrackController _uut;

		private ICurrentTracksManager _fakeCurrentTracksManager;
		private ITrackGenerator _fakeTrackGenerator;
		private ITrackListFormatter _fakeTrackListFormatter;

		private const int XBoundarySouthWest = 10000;
		private const int YBoundarySouthWest = 10000;
		private const int XBoundaryNorthEast = 90000;
		private const int YBoundaryNorthEast = 90000;
		private const int ZBoundaryLower = 500;
		private const int ZBoundaryUpper = 20000;

		[SetUp]
		public void Init()
		{
			_fakeCurrentTracksManager = Substitute.For<ICurrentTracksManager>();
			_fakeTrackGenerator = Substitute.For<ITrackGenerator>();
			_fakeTrackListFormatter = Substitute.For<ITrackListFormatter>();

			_uut = new TrackController(_fakeCurrentTracksManager, _fakeTrackGenerator, _fakeTrackListFormatter);
		}

		[Test]
		public void AddTrackDataObjects_GivenListIsNull_CurrentTracksManagerTrackListIsNotRequested()
		{
			// Act
			_uut.AddTrackDataObjects(null);

			// Assert
			// Note that the "unused" variable is only there to make the compiler "happy"
			var unused = _fakeCurrentTracksManager.DidNotReceive().CurrentTracks;
		}

		[Test]
		public void AddTrackDataObjects_CurrentTrackListContains3TracksNotInGivenList_RemoveOnCurrentTracksManagerCalled3Times()
		{
			// Arrange
			var trackObject1 = new Track("tag1", new TrackData("tag1", 0, 0, 0, DateTime.Now));
			var trackObject2 = new Track("tag2", new TrackData("tag2", 0, 0, 0, DateTime.Now));
			var trackObject3 = new Track("tag3", new TrackData("tag3", 0, 0, 0, DateTime.Now));
			var trackList = new List<Track>
			{
				trackObject1,
				trackObject2,
				trackObject3
			};
			_fakeCurrentTracksManager.CurrentTracks.Returns(trackList);

			// Act
			_uut.AddTrackDataObjects(new List<TrackData>());

			// Assert
			_fakeCurrentTracksManager.Received(3).RemoveTrack(Arg.Any<Track>());
		}

		[Test]
		public void AddTrackDataObjects_CurrentTrackListContainsTrackNotInGivenList_RemoveOnCurrentTracksManagerCalledWithCorrectTrack()
		{
			// Arrange
			var trackObject = new Track("tag", new TrackData("tag", 0, 0, 0, DateTime.Now));
			var trackList = new List<Track> { trackObject };
			_fakeCurrentTracksManager.CurrentTracks.Returns(trackList);

			// Act
			_uut.AddTrackDataObjects(new List<TrackData>());

			// Assert
			_fakeCurrentTracksManager.Received().RemoveTrack(trackObject);
		}

		[Test]
		public void AddTrackDataObjects_CurrentTrackListContainsNoTracksNotInCurrentGivenList_RemoveOnCurrentTracksManagerNotCalled()
		{
			// Arrange
			var trackList = new List<Track>();
			_fakeCurrentTracksManager.CurrentTracks.Returns(trackList);

			// Act
			_uut.AddTrackDataObjects(new List<TrackData>());

			// Assert
			_fakeCurrentTracksManager.DidNotReceive().RemoveTrack(Arg.Any<Track>());
		}

		[TestCase(XBoundarySouthWest - 1, YBoundarySouthWest, ZBoundaryLower)]
		[TestCase(XBoundarySouthWest, YBoundarySouthWest - 1, ZBoundaryLower)]
		[TestCase(XBoundarySouthWest, YBoundarySouthWest, ZBoundaryLower - 1)]
		[TestCase(XBoundarySouthWest - 1, YBoundarySouthWest - 1, ZBoundaryLower - 1)]
		[TestCase(XBoundaryNorthEast + 1, YBoundaryNorthEast, ZBoundaryUpper)]
		[TestCase(XBoundaryNorthEast, YBoundaryNorthEast + 1, ZBoundaryUpper)]
		[TestCase(XBoundaryNorthEast, YBoundaryNorthEast, ZBoundaryUpper + 1)]
		[TestCase(XBoundaryNorthEast + 1, YBoundaryNorthEast + 1, ZBoundaryUpper + 1)]
		public void AddTrackDataObjects_GivenListContainsTrackWithCoordinatesOutsideArea_AddTrackOnCurrentTracksManagerNotCalled(
			int x, int y, int altitude)
		{
			// Arrange
			var trackList = new List<Track>();
			_fakeCurrentTracksManager.CurrentTracks.Returns(trackList);

			var trackDataObject = new TrackData("tag", x, y, altitude, DateTime.Now);
			var trackDataList = new List<TrackData> { trackDataObject };

			// Act
			_uut.AddTrackDataObjects(trackDataList);

			// Assert
			_fakeCurrentTracksManager.DidNotReceive().AddTrack(Arg.Any<Track>());
		}

		[TestCase(XBoundarySouthWest - 1, YBoundarySouthWest, ZBoundaryLower)]
		[TestCase(XBoundarySouthWest, YBoundarySouthWest - 1, ZBoundaryLower)]
		[TestCase(XBoundarySouthWest, YBoundarySouthWest, ZBoundaryLower - 1)]
		[TestCase(XBoundarySouthWest - 1, YBoundarySouthWest - 1, ZBoundaryLower - 1)]
		[TestCase(XBoundaryNorthEast + 1, YBoundaryNorthEast, ZBoundaryUpper)]
		[TestCase(XBoundaryNorthEast, YBoundaryNorthEast + 1, ZBoundaryUpper)]
		[TestCase(XBoundaryNorthEast, YBoundaryNorthEast, ZBoundaryUpper + 1)]
		[TestCase(XBoundaryNorthEast + 1, YBoundaryNorthEast + 1, ZBoundaryUpper + 1)]
		public void AddTrackDataObjects_GivenListContainsTrackWithCoordinatesOutsideArea_UpdateTrackOnCurrentTracksManagerNotCalled(
			int x, int y, int altitude)
		{
			// Arrange
			var trackList = new List<Track>();
			_fakeCurrentTracksManager.CurrentTracks.Returns(trackList);

			var trackDataObject = new TrackData("tag", x, y, altitude, DateTime.Now);
			var trackDataList = new List<TrackData> { trackDataObject };

			// Act
			_uut.AddTrackDataObjects(trackDataList);

			// Assert
			_fakeCurrentTracksManager.DidNotReceive().UpdateTrack(Arg.Any<TrackData>());
		}

		[TestCase(XBoundarySouthWest, YBoundarySouthWest, ZBoundaryLower)]
		[TestCase(XBoundarySouthWest, YBoundarySouthWest, ZBoundaryUpper)]
		[TestCase(XBoundarySouthWest, YBoundaryNorthEast, ZBoundaryLower)]
		[TestCase(XBoundarySouthWest, YBoundaryNorthEast, ZBoundaryUpper)]
		[TestCase(XBoundaryNorthEast, YBoundarySouthWest, ZBoundaryLower)]
		[TestCase(XBoundaryNorthEast, YBoundarySouthWest, ZBoundaryUpper)]
		[TestCase(XBoundaryNorthEast, YBoundaryNorthEast, ZBoundaryLower)]
		[TestCase(XBoundaryNorthEast, YBoundaryNorthEast, ZBoundaryUpper)]
		public void AddTrackDataObjects_TrackNotInCurrentTracksListAndInsideArea_GenerateOnTrackGeneratorCalledWithCorrectTrackData(
			int x, int y, int altitude)
		{
			// Arrange
			var trackList = new List<Track>();
			_fakeCurrentTracksManager.CurrentTracks.Returns(trackList);

			var trackDataObject = new TrackData("tag", x, y, altitude, DateTime.Now);
			var trackDataList = new List<TrackData> { trackDataObject };

			// Act
			_uut.AddTrackDataObjects(trackDataList);

			// Assert
			_fakeTrackGenerator.Received().GenerateTrack(trackDataObject);
		}

		[TestCase(XBoundarySouthWest, YBoundarySouthWest, ZBoundaryLower)]
		[TestCase(XBoundarySouthWest, YBoundarySouthWest, ZBoundaryUpper)]
		[TestCase(XBoundarySouthWest, YBoundaryNorthEast, ZBoundaryLower)]
		[TestCase(XBoundarySouthWest, YBoundaryNorthEast, ZBoundaryUpper)]
		[TestCase(XBoundaryNorthEast, YBoundarySouthWest, ZBoundaryLower)]
		[TestCase(XBoundaryNorthEast, YBoundarySouthWest, ZBoundaryUpper)]
		[TestCase(XBoundaryNorthEast, YBoundaryNorthEast, ZBoundaryLower)]
		[TestCase(XBoundaryNorthEast, YBoundaryNorthEast, ZBoundaryUpper)]
		public void AddTrackDataObjects_TrackNotInCurrentTracksListAndInsideArea_AddOnCurrentTracksManagerCalledWithCorrectTrack(
			int x, int y, int altitude)
		{
			// Arrange
			var trackList = new List<Track>();
			_fakeCurrentTracksManager.CurrentTracks.Returns(trackList);

			var trackObject = new Track("tag", new TrackData("tag", 0, 0, 0, DateTime.Now));
			_fakeTrackGenerator.GenerateTrack(Arg.Any<TrackData>()).Returns(trackObject);

			var trackDataObject = new TrackData("tag2", x, y, altitude, DateTime.Now);
			var trackDataList = new List<TrackData> { trackDataObject };

			// Act
			_uut.AddTrackDataObjects(trackDataList);

			// Assert
			_fakeCurrentTracksManager.Received().AddTrack(trackObject);
		}

		[TestCase(XBoundarySouthWest, YBoundarySouthWest, ZBoundaryLower)]
		[TestCase(XBoundarySouthWest, YBoundarySouthWest, ZBoundaryUpper)]
		[TestCase(XBoundarySouthWest, YBoundaryNorthEast, ZBoundaryLower)]
		[TestCase(XBoundarySouthWest, YBoundaryNorthEast, ZBoundaryUpper)]
		[TestCase(XBoundaryNorthEast, YBoundarySouthWest, ZBoundaryLower)]
		[TestCase(XBoundaryNorthEast, YBoundarySouthWest, ZBoundaryUpper)]
		[TestCase(XBoundaryNorthEast, YBoundaryNorthEast, ZBoundaryLower)]
		[TestCase(XBoundaryNorthEast, YBoundaryNorthEast, ZBoundaryUpper)]
		public void AddTrackDataObjects_TrackInCurrentTracksListAndInsideArea_UpdateOnCurrentTracksManagerCalledWithCorrectTrackData(
			int x, int y, int altitude)
		{
			// Arrange
			var trackObject = new Track("tag", new TrackData("tag", 0, 0, 0, DateTime.Now));
			var trackList = new List<Track> { trackObject };
			_fakeCurrentTracksManager.CurrentTracks.Returns(trackList);
			_fakeCurrentTracksManager.FindTrack(Arg.Any<string>()).Returns(trackObject);

			var trackDataObject = new TrackData("tag", x, y, altitude, DateTime.Now);
			var trackDataList = new List<TrackData> { trackDataObject };

			// Act
			_uut.AddTrackDataObjects(trackDataList);

			// Assert
			_fakeCurrentTracksManager.Received().UpdateTrack(trackDataObject);
		}

		[Test]
		public void AddTrackDataObjects_CurrentTracksManagerListNumberOfElementsIs0_CorrectCurrentTracksListIsReturned()
		{
			// Arrange
			var trackList = new List<Track>();
			_fakeCurrentTracksManager.CurrentTracks.Returns(trackList);
			_fakeCurrentTracksManager.GetTrackCount().Returns(0);

			// Act
			var currentTracks = _uut.AddTrackDataObjects(new List<TrackData>());

			// Assert
			Assert.That(currentTracks, Is.EqualTo(trackList));
		}

		[Test]
		public void AddTrackDataObjects_CurrentTracksManagerListNumberOfElementsIs5_CorrectCurrentTracksListIsReturned()
		{
			// Arrange
			var trackObject1 = new Track("tag1", new TrackData("tag1", 0, 0, 0, DateTime.Now));
			var trackObject2 = new Track("tag2", new TrackData("tag2", 0, 0, 0, DateTime.Now));
			var trackObject3 = new Track("tag3", new TrackData("tag3", 0, 0, 0, DateTime.Now));
			var trackObject4 = new Track("tag4", new TrackData("tag4", 0, 0, 0, DateTime.Now));
			var trackObject5 = new Track("tag5", new TrackData("tag5", 0, 0, 0, DateTime.Now));
			var trackList = new List<Track>
			{
				trackObject1,
				trackObject2,
				trackObject3,
				trackObject4,
				trackObject5
			};
			_fakeCurrentTracksManager.CurrentTracks.Returns(trackList);
			_fakeCurrentTracksManager.GetTrackCount().Returns(5);

			// Act
			var currentTracks = _uut.AddTrackDataObjects(new List<TrackData>());

			// Assert
			Assert.That(currentTracks, Is.EqualTo(trackList));
		}

		[Test]
		public void GetFormattedCurrentTracks_CallMethod_FormatOnTrackListFormatterCalled()
		{
			// Act
			_uut.GetFormattedCurrentTracks();

			// Assert
			_fakeTrackListFormatter.Received().Format(Arg.Any<List<Track>>());
		}

		[Test]
		public void GetFormattedCurrentTracks_CurrentTracksManagerListNumberOfElementsIs0_CorrectCurrentTracksListIsGivenToFormatter()
		{
			// Arrange
			var trackList = new List<Track>();
			_fakeCurrentTracksManager.CurrentTracks.Returns(trackList);
			_fakeCurrentTracksManager.GetTrackCount().Returns(0);

			// Act
			_uut.GetFormattedCurrentTracks();

			// Assert
			_fakeTrackListFormatter.Received().Format(trackList);
		}

		[Test]
		public void GetFormattedCurrentTracks_CurrentTracksManagerListNumberOfElementsIs5_CorrectCurrentTracksListIsGivenToFormatter()
		{
			// Arrange
			var trackObject1 = new Track("tag1", new TrackData("tag1", 0, 0, 0, DateTime.Now));
			var trackObject2 = new Track("tag2", new TrackData("tag2", 0, 0, 0, DateTime.Now));
			var trackObject3 = new Track("tag3", new TrackData("tag3", 0, 0, 0, DateTime.Now));
			var trackObject4 = new Track("tag4", new TrackData("tag4", 0, 0, 0, DateTime.Now));
			var trackObject5 = new Track("tag5", new TrackData("tag5", 0, 0, 0, DateTime.Now));
			var trackList = new List<Track>
			{
				trackObject1,
				trackObject2,
				trackObject3,
				trackObject4,
				trackObject5
			};
			_fakeCurrentTracksManager.CurrentTracks.Returns(trackList);
			_fakeCurrentTracksManager.GetTrackCount().Returns(5);

			// Act
			_uut.GetFormattedCurrentTracks();

			// Assert
			_fakeTrackListFormatter.Received().Format(trackList);
		}
	}
}
