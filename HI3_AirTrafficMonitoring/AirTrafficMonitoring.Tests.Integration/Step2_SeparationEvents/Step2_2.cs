using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.Printer;
using AirTrafficMonitoring.Classes.SeparationEvents;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Tests.Integration.Step2_SeparationEvents
{
	[TestFixture]
	class Step2_2
	{
		// Actual classes
		private ISeparationEventController _separationEventController;
		private ICurrentSeparationEventsManager _currentSeparationEventsManager;
		private ISeparationEventGenerator _separationEventGenerator;

		// Fakes
		private ISeparationEventListFormatter _fakeSeparationEventListFormatter;
		private IPrinter _fakeEventLogger;

		[SetUp]
		public void Init()
		{
			_fakeSeparationEventListFormatter = Substitute.For<ISeparationEventListFormatter>();
			_fakeEventLogger = Substitute.For<IPrinter>();

			_currentSeparationEventsManager = new CurrentSeparationEventsManager();
			_separationEventGenerator = new SeparationEventGenerator();

			_separationEventController = new SeparationEventController(
				_currentSeparationEventsManager,
				_separationEventGenerator,
				_fakeSeparationEventListFormatter,
				_fakeEventLogger);
		}

		[TestCase(11000, 12000, 13000, 12000, 11000, 12800)]
		[TestCase(85000, 86000, 14900, 83000, 84000, 15000)]
		public void CheckForSeparationEvents_2ConflictingTracksNotInCurrentEvents_GenerateOnEventGeneratorCalledWithCorrectTag1(
			int x1, int y1, int z1, int x2, int y2, int z2)
		{
			// Arrange
			const string tag1 = "tag1";
			var track1 = new Track(tag1, new TrackData(tag1, x1, y1, z1, DateTime.Now));

			const string tag2 = "tag2";
			var track2 = new Track(tag2, new TrackData(tag2, x2, y2, z2, DateTime.Now));

			var trackList = new List<Track> { track1, track2 };

			// Act
			_separationEventController.CheckForSeparationEvents(trackList);

			// Assert
			var foundEvent = _currentSeparationEventsManager.FindEvent(tag1, tag2);
			Assert.That(foundEvent.Tag1, Is.EqualTo(tag1));
		}

		[TestCase(11000, 12000, 13000, 12000, 11000, 12800)]
		[TestCase(85000, 86000, 14900, 83000, 84000, 15000)]
		public void CheckForSeparationEvents_2ConflictingTracksNotInCurrentEvents_GenerateOnEventGeneratorCalledWithCorrectTag2(
			int x1, int y1, int z1, int x2, int y2, int z2)
		{
			// Arrange
			const string tag1 = "tag1";
			var track1 = new Track(tag1, new TrackData(tag1, x1, y1, z1, DateTime.Now));

			const string tag2 = "tag2";
			var track2 = new Track(tag2, new TrackData(tag2, x2, y2, z2, DateTime.Now));

			var trackList = new List<Track> { track1, track2 };

			// Act
			_separationEventController.CheckForSeparationEvents(trackList);

			// Assert
			var foundEvent = _currentSeparationEventsManager.FindEvent(tag1, tag2);
			Assert.That(foundEvent.Tag2, Is.EqualTo(tag2));
		}

		[TestCase(11000, 12000, 13000, 12000, 11000, 12800)]
		[TestCase(85000, 86000, 14900, 83000, 84000, 15000)]
		public void CheckForSeparationEvents_2ConflictingTracksNotInCurrentEvents_EventGeneratorCalledWithCorrectTimestampDate(
			int x1, int y1, int z1, int x2, int y2, int z2)
		{
			// Arrange
			const string tag1 = "tag1";
			var track1 = new Track(tag1, new TrackData(tag1, x1, y1, z1, DateTime.Now));

			const string tag2 = "tag2";
			var track2 = new Track(tag2, new TrackData(tag2, x2, y2, z2, DateTime.Now));

			var trackList = new List<Track> { track1, track2 };

			var expectedTimestamp = DateTime.Now;

			// Act
			_separationEventController.CheckForSeparationEvents(trackList);

			// Assert
			var foundEvent = _currentSeparationEventsManager.FindEvent(tag1, tag2);
			Assert.That(foundEvent.Timestamp.Date, Is.EqualTo(expectedTimestamp.Date));
		}

		[TestCase(11000, 12000, 13000, 12000, 11000, 12800)]
		[TestCase(85000, 86000, 14900, 83000, 84000, 15000)]
		public void CheckForSeparationEvents_2ConflictingTracksNotInCurrentEvents_EventGeneratorCalledWithCorrectTimestampHour(
			int x1, int y1, int z1, int x2, int y2, int z2)
		{
			// Arrange
			const string tag1 = "tag1";
			var track1 = new Track(tag1, new TrackData(tag1, x1, y1, z1, DateTime.Now));

			const string tag2 = "tag2";
			var track2 = new Track(tag2, new TrackData(tag2, x2, y2, z2, DateTime.Now));

			var trackList = new List<Track> { track1, track2 };

			var expectedTimestamp = DateTime.Now;

			// Act
			_separationEventController.CheckForSeparationEvents(trackList);

			// Assert
			var foundEvent = _currentSeparationEventsManager.FindEvent(tag1, tag2);
			Assert.That(foundEvent.Timestamp.Hour, Is.EqualTo(expectedTimestamp.Hour));
		}

		[TestCase(11000, 12000, 13000, 12000, 11000, 12800)]
		[TestCase(85000, 86000, 14900, 83000, 84000, 15000)]
		public void CheckForSeparationEvents_2ConflictingTracksNotInCurrentEvents_EventGeneratorCalledWithCorrectTimestampMinute(
			int x1, int y1, int z1, int x2, int y2, int z2)
		{
			// Arrange
			const string tag1 = "tag1";
			var track1 = new Track(tag1, new TrackData(tag1, x1, y1, z1, DateTime.Now));

			const string tag2 = "tag2";
			var track2 = new Track(tag2, new TrackData(tag2, x2, y2, z2, DateTime.Now));

			var trackList = new List<Track> { track1, track2 };

			var expectedTimestamp = DateTime.Now;

			// Act
			_separationEventController.CheckForSeparationEvents(trackList);

			// Assert
			var foundEvent = _currentSeparationEventsManager.FindEvent(tag1, tag2);
			Assert.That(foundEvent.Timestamp.Minute, Is.EqualTo(expectedTimestamp.Minute));
		}

		[TestCase(11000, 12000, 13000, 12000, 11000, 12800)]
		[TestCase(85000, 86000, 14900, 83000, 84000, 15000)]
		public void CheckForSeparationEvents_2ConflictingTracksNotInCurrentEvents_EventGeneratorCalledWithCorrectTimestampSecond(
			int x1, int y1, int z1, int x2, int y2, int z2)
		{
			// Arrange
			const string tag1 = "tag1";
			var track1 = new Track(tag1, new TrackData(tag1, x1, y1, z1, DateTime.Now));

			const string tag2 = "tag2";
			var track2 = new Track(tag2, new TrackData(tag2, x2, y2, z2, DateTime.Now));

			var trackList = new List<Track> { track1, track2 };

			var expectedTimestamp = DateTime.Now;

			// Act
			_separationEventController.CheckForSeparationEvents(trackList);

			// Assert
			var foundEvent = _currentSeparationEventsManager.FindEvent(tag1, tag2);
			Assert.That(foundEvent.Timestamp.Second, Is.EqualTo(expectedTimestamp.Second));
		}
	}
}
