using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.Tracks;
using NUnit.Framework;
using System;

namespace AirTrafficMonitoring.Tests.Integration.Step1_Tracks
{
	class Step1_1
	{
		private ICurrentTracksManager _currentTracksManager;

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
		}

		[Test]
		public void UpdateTrack_CallUpdateTrackWithNewTrackData_CurrentTrackOnActualTrackIsSetCorrectly()
		{
			// Arrange
			const string tag = "someTag";

			var originalTrackDataObject = new TrackData(tag, XBoundarySouthWest, YBoundarySouthWest, ZBoundaryLower, DateTime.Now);
			_currentTracksManager.AddTrack(new Track(tag, originalTrackDataObject));

			var newTrackDataObject = new TrackData(tag, XBoundaryNorthEast, YBoundaryNorthEast, ZBoundaryUpper, DateTime.Now);

			// Act
			_currentTracksManager.UpdateTrack(newTrackDataObject);

			// Assert
			var track = _currentTracksManager.FindTrack(tag);
			Assert.That(track.CurrentTrack, Is.EqualTo(newTrackDataObject));
		}
	}
}
