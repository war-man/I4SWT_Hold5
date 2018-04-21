using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.Tracks;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Tests.Integration.Step1_Tracks
{
	class Step1_1
	{
		// Actual classes
		private ITrackController _trackController;
		private ICurrentTracksManager _currentTracksManager;

		// Fakes
		private ITrackGenerator _fakeTrackGenerator;
		private ITrackListFormatter _fakeTrackListFormatter;

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
			_currentTracksManager = new CurrentTracksManager();
			_fakeTrackGenerator = Substitute.For<ITrackGenerator>();
			_fakeTrackListFormatter = Substitute.For<ITrackListFormatter>();

			_trackController = new TrackController(_currentTracksManager, _fakeTrackGenerator, _fakeTrackListFormatter);
		}

		[Test]
		public void AddTrackDataObjects_CurrentTrackListContainsTrackNotInGivenList_TrackIsRemovedFromCurrentTracks()
		{
			// Arrange
			const string tag = "someTag";
			var trackObject = new Track(tag, new TrackData("tag", 0, 0, 0, DateTime.Now));
			_currentTracksManager.AddTrack(trackObject);

			// Act
			_trackController.AddTrackDataObjects(new List<TrackData>());

			// Assert
			Assert.Null(_currentTracksManager.FindTrack(tag));
		}

		[Test]
		public void AddTrackDataObjects_GivenListContainsTrackInCurrentTracksList_TrackIsNotRemovedFromCurrentTracks()
		{
			// Arrange
			const string tag = "someTag";
			var trackObject = new Track(tag, new TrackData("tag", 0, 0, 0, DateTime.Now));
			_currentTracksManager.AddTrack(trackObject);

			var trackDataList = new List<TrackData> { new TrackData(tag, 10, 10, 10, DateTime.Now) };

			// Act
			_trackController.AddTrackDataObjects(trackDataList);

			// Assert
			var track = _currentTracksManager.FindTrack(tag);
			Assert.That(track.Tag, Is.EqualTo(tag));
		}

		[TestCase(XBoundarySouthWest - 1, YBoundarySouthWest, ZBoundaryLower)]
		[TestCase(XBoundarySouthWest, YBoundarySouthWest - 1, ZBoundaryLower)]
		[TestCase(XBoundarySouthWest, YBoundarySouthWest, ZBoundaryLower - 1)]
		[TestCase(XBoundarySouthWest - 1, YBoundarySouthWest - 1, ZBoundaryLower - 1)]
		[TestCase(XBoundaryNorthEast + 1, YBoundaryNorthEast, ZBoundaryUpper)]
		[TestCase(XBoundaryNorthEast, YBoundaryNorthEast + 1, ZBoundaryUpper)]
		[TestCase(XBoundaryNorthEast, YBoundaryNorthEast, ZBoundaryUpper + 1)]
		[TestCase(XBoundaryNorthEast + 1, YBoundaryNorthEast + 1, ZBoundaryUpper + 1)]
		public void AddTrackDataObjects_TrackBotInCurrentTracksListAndOutsideArea_TrackIsNotAdded(
			int x, int y, int altitude)
		{
			// Arrange
			const string tag = "someTag";

			var newTrackDataObject = new TrackData(tag, x, y, altitude, DateTime.Now);
			var trackDataList = new List<TrackData> { newTrackDataObject };

			// Act
			_trackController.AddTrackDataObjects(trackDataList);

			// Assert
			var track = _currentTracksManager.FindTrack(tag);
			Assert.Null(track);
		}

		[TestCase(XBoundarySouthWest - 1, YBoundarySouthWest, ZBoundaryLower)]
		[TestCase(XBoundarySouthWest, YBoundarySouthWest - 1, ZBoundaryLower)]
		[TestCase(XBoundarySouthWest, YBoundarySouthWest, ZBoundaryLower - 1)]
		[TestCase(XBoundarySouthWest - 1, YBoundarySouthWest - 1, ZBoundaryLower - 1)]
		[TestCase(XBoundaryNorthEast + 1, YBoundaryNorthEast, ZBoundaryUpper)]
		[TestCase(XBoundaryNorthEast, YBoundaryNorthEast + 1, ZBoundaryUpper)]
		[TestCase(XBoundaryNorthEast, YBoundaryNorthEast, ZBoundaryUpper + 1)]
		[TestCase(XBoundaryNorthEast + 1, YBoundaryNorthEast + 1, ZBoundaryUpper + 1)]
		public void AddTrackDataObjects_TrackInCurrentTracksListAndOutsideArea_TrackDataIsNotUpdated(
			int x, int y, int altitude)
		{
			// Arrange
			const string tag = "someTag";

			var originalTrackDataObject = new TrackData(tag, XBoundarySouthWest, YBoundarySouthWest, ZBoundaryLower, DateTime.Now);
			_currentTracksManager.AddTrack(new Track(tag, originalTrackDataObject));

			var newTrackDataObject = new TrackData(tag, x, y, altitude, DateTime.Now);
			var trackDataList = new List<TrackData> { newTrackDataObject };

			// Act
			_trackController.AddTrackDataObjects(trackDataList);

			// Assert
			var track = _currentTracksManager.FindTrack(tag);
			Assert.That(track.CurrentTrack, Is.EqualTo(originalTrackDataObject));
		}

		[TestCase(XBoundarySouthWest, YBoundarySouthWest, ZBoundaryLower)]
		[TestCase(XBoundarySouthWest, YBoundarySouthWest, ZBoundaryUpper)]
		[TestCase(XBoundarySouthWest, YBoundaryNorthEast, ZBoundaryLower)]
		[TestCase(XBoundarySouthWest, YBoundaryNorthEast, ZBoundaryUpper)]
		[TestCase(XBoundaryNorthEast, YBoundarySouthWest, ZBoundaryLower)]
		[TestCase(XBoundaryNorthEast, YBoundarySouthWest, ZBoundaryUpper)]
		[TestCase(XBoundaryNorthEast, YBoundaryNorthEast, ZBoundaryLower)]
		[TestCase(XBoundaryNorthEast, YBoundaryNorthEast, ZBoundaryUpper)]
		public void AddTrackDataObjects_TrackNotInCurrentTracksListAndInsideArea_TrackIsAdded(
			int x, int y, int altitude)
		{
			// Arrange
			const string tag = "someTag";

			var newTrackDataObject = new TrackData(tag, x, y, altitude, DateTime.Now);
			var trackDataList = new List<TrackData> { newTrackDataObject };

			var newTrackObject = new Track(tag, newTrackDataObject);
			_fakeTrackGenerator.GenerateTrack(newTrackDataObject).Returns(newTrackObject);

			// Act
			_trackController.AddTrackDataObjects(trackDataList);

			// Assert
			var track = _currentTracksManager.FindTrack(tag);
			Assert.That(track.CurrentTrack, Is.EqualTo(newTrackDataObject));
		}

		[TestCase(XBoundarySouthWest, YBoundarySouthWest, ZBoundaryLower)]
		[TestCase(XBoundarySouthWest, YBoundarySouthWest, ZBoundaryUpper)]
		[TestCase(XBoundarySouthWest, YBoundaryNorthEast, ZBoundaryLower)]
		[TestCase(XBoundarySouthWest, YBoundaryNorthEast, ZBoundaryUpper)]
		[TestCase(XBoundaryNorthEast, YBoundarySouthWest, ZBoundaryLower)]
		[TestCase(XBoundaryNorthEast, YBoundarySouthWest, ZBoundaryUpper)]
		[TestCase(XBoundaryNorthEast, YBoundaryNorthEast, ZBoundaryLower)]
		[TestCase(XBoundaryNorthEast, YBoundaryNorthEast, ZBoundaryUpper)]
		public void AddTrackDataObjects_TrackInCurrentTracksListAndInsideArea_TrackDataIsUpdated(
			int x, int y, int altitude)
		{
			// Arrange
			const string tag = "someTag";

			var originalTrackDataObject = new TrackData(tag, XBoundarySouthWest, YBoundarySouthWest, ZBoundaryLower, DateTime.Now);
			_currentTracksManager.AddTrack(new Track(tag, originalTrackDataObject));

			var newTrackDataObject = new TrackData(tag, x, y, altitude, DateTime.Now);
			var trackDataList = new List<TrackData> { newTrackDataObject };

			// Act
			_trackController.AddTrackDataObjects(trackDataList);

			// Assert
			var track = _currentTracksManager.FindTrack(tag);
			Assert.That(track.CurrentTrack, Is.EqualTo(newTrackDataObject));
		}

		[Test]
		public void AddTrackDataObjects_UpdateTrackOnCurrentTracksList_CorrectTracksListIsReturned()
		{
			// Arrange
			const string tag = "tag";

			var trackDataObject = new TrackData(tag, XBoundarySouthWest, YBoundarySouthWest, ZBoundaryLower, DateTime.Now);
			var trackObject = new Track(tag, trackDataObject);
			_currentTracksManager.AddTrack(trackObject);

			var newTrackDataObject = new TrackData(tag, XBoundaryNorthEast, YBoundaryNorthEast, ZBoundaryUpper, DateTime.Now);
			var trackDataList = new List<TrackData> { newTrackDataObject };

			// Act
			var currentTracksList = _trackController.AddTrackDataObjects(trackDataList);

			// Assert
			Assert.That(currentTracksList[0], Is.EqualTo(trackObject));
		}
	}
}
