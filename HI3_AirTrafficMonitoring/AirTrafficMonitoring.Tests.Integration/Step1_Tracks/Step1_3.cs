using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.Tracks;
using NUnit.Framework;
using System;
using System.Globalization;

namespace AirTrafficMonitoring.Tests.Integration.Step1_Tracks
{
	class Step1_3
	{
		// Actual classes
		private ITrackController _trackController;
		private ITrackGenerator _trackGenerator;
		private ICurrentTracksManager _currentTracksManager;
		private ITrackListFormatter _trackListFormatter;

		[SetUp]
		public void Init()
		{
			_currentTracksManager = new CurrentTracksManager();
			_trackGenerator = new TrackGenerator();
			_trackListFormatter = new TrackListFormatter();

			_trackController = new TrackController(_currentTracksManager, _trackGenerator, _trackListFormatter);
		}

		[Test]
		public void GetFormattedCurrentTracks_OneTrackInCurrentTracksList_FormattedListContainsTag()
		{
			// Arrange
			const string tag = "tag";
			const int xCoordinate = 10000;
			const int yCoordinate = 11000;
			const int altitude = 12000;
			var timestamp = DateTime.Now;

			var trackObject = new Track(tag, new TrackData(tag, xCoordinate, yCoordinate, altitude, timestamp));
			_currentTracksManager.AddTrack(trackObject);

			// Act
			var formattedTracksList = _trackController.GetFormattedCurrentTracks();

			// Assert
			Assert.That(formattedTracksList, Contains.Substring(tag));
		}

		[Test]
		public void GetFormattedCurrentTracks_OneTrackInCurrentTracksList_FormattedListContainsXCoordinate()
		{
			// Arrange
			const string tag = "tag";
			const int xCoordinate = 10000;
			const int yCoordinate = 11000;
			const int altitude = 12000;
			var timestamp = DateTime.Now;

			var trackObject = new Track(tag, new TrackData(tag, xCoordinate, yCoordinate, altitude, timestamp));
			_currentTracksManager.AddTrack(trackObject);

			// Act
			var formattedTracksList = _trackController.GetFormattedCurrentTracks();

			// Assert
			Assert.That(formattedTracksList, Contains.Substring(xCoordinate.ToString()));
		}

		[Test]
		public void GetFormattedCurrentTracks_OneTrackInCurrentTracksList_FormattedListContainsYCoordinate()
		{
			// Arrange
			const string tag = "tag";
			const int xCoordinate = 10000;
			const int yCoordinate = 11000;
			const int altitude = 12000;
			var timestamp = DateTime.Now;

			var trackObject = new Track(tag, new TrackData(tag, xCoordinate, yCoordinate, altitude, timestamp));
			_currentTracksManager.AddTrack(trackObject);

			// Act
			var formattedTracksList = _trackController.GetFormattedCurrentTracks();

			// Assert
			Assert.That(formattedTracksList, Contains.Substring(yCoordinate.ToString()));
		}

		[Test]
		public void GetFormattedCurrentTracks_OneTrackInCurrentTracksList_FormattedListContainsAltitude()
		{
			// Arrange
			const string tag = "tag";
			const int xCoordinate = 10000;
			const int yCoordinate = 11000;
			const int altitude = 12000;
			var timestamp = DateTime.Now;

			var trackObject = new Track(tag, new TrackData(tag, xCoordinate, yCoordinate, altitude, timestamp));
			_currentTracksManager.AddTrack(trackObject);

			// Act
			var formattedTracksList = _trackController.GetFormattedCurrentTracks();

			// Assert
			Assert.That(formattedTracksList, Contains.Substring(altitude.ToString()));
		}

		[Test]
		public void GetFormattedCurrentTracks_OneTrackInCurrentTracksList_FormattedListContainsTimestamp()
		{
			// Arrange
			const string tag = "tag";
			const int xCoordinate = 10000;
			const int yCoordinate = 11000;
			const int altitude = 12000;
			var timestamp = DateTime.Now;

			var trackObject = new Track(tag, new TrackData(tag, xCoordinate, yCoordinate, altitude, timestamp));
			_currentTracksManager.AddTrack(trackObject);

			// Act
			var formattedTracksList = _trackController.GetFormattedCurrentTracks();

			// Assert
			Assert.That(formattedTracksList, Contains.Substring(timestamp.ToString(CultureInfo.CurrentCulture)));
		}
	}
}
