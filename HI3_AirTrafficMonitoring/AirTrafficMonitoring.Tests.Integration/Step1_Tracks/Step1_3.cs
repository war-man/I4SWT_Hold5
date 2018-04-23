using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.Tracks;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Tests.Integration.Step1_Tracks
{
	class Step1_3
	{
		// Actual classes
		private ITrackController _trackController;
		private ITrackGenerator _trackGenerator;

		// Fakes
		private ICurrentTracksManager _currentTracksManager;
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
			_trackGenerator = new TrackGenerator();
			_fakeTrackListFormatter = Substitute.For<ITrackListFormatter>();

			_trackController = new TrackController(_currentTracksManager, _trackGenerator, _fakeTrackListFormatter);
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
			const string tag = "tag";
			var trackDataObject = new TrackData(tag, x, y, altitude, DateTime.Now);
			var trackDataList = new List<TrackData> { trackDataObject };

			// Act
			_trackController.AddTrackDataObjects(trackDataList);

			// Assert
			var track = _currentTracksManager.FindTrack(tag);
			Assert.That(track.CurrentTrack, Is.EqualTo(trackDataObject));
		}
	}
}
