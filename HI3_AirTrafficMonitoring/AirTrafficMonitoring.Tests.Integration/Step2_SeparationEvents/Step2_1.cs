using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.Printer;
using AirTrafficMonitoring.Classes.SeparationEvents;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Tests.Integration.Step2_SeparationEvents
{
	[TestFixture]
	class Step2_1
	{
		// Actual classes
		private ISeparationEventController _separationEventController;
		private ICurrentSeparationEventsManager _currentSeparationEventsManager;

		// Fakes
		private ISeparationEventGenerator _fakeSeparationEventGenerator;
		private ISeparationEventListFormatter _fakeSeparationEventListFormatter;
		private IPrinter _fakeEventLogger;

		[SetUp]
		public void Init()
		{
			_fakeSeparationEventGenerator = Substitute.For<ISeparationEventGenerator>();
			_fakeSeparationEventListFormatter = Substitute.For<ISeparationEventListFormatter>();
			_fakeEventLogger = Substitute.For<IPrinter>();

			_currentSeparationEventsManager = new CurrentSeparationEventsManager();

			_separationEventController = new SeparationEventController(
				_currentSeparationEventsManager,
				_fakeSeparationEventGenerator,
				_fakeSeparationEventListFormatter,
				_fakeEventLogger);
		}

		[TestCase(11000, 12000, 13000, 15000, 16000, 12700)]
		[TestCase(14600, 15500, 13000, 11000, 12000, 13300)]
		public void CheckForSeparationEvents_2NonConflictingTracksNotInCurrentEvents_EventIsNotAddedToCurrentEventsManager(
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
			Assert.Null(_currentSeparationEventsManager.FindEvent(tag1, tag2));
		}

		[TestCase(11000, 12000, 13000, 15000, 16000, 12700)]
		[TestCase(14600, 15500, 13000, 11000, 12000, 13300)]
		public void CheckForSeparationEvents_2NonConflictingTracksWhoHasCurrentEvent_EventIsRemovedFromCurrentEvensManager(
			int x1, int y1, int z1, int x2, int y2, int z2)
		{
			// Arrange
			const string tag1 = "tag1";
			var track1 = new Track(tag1, new TrackData(tag1, x1, y1, z1, DateTime.Now));

			const string tag2 = "tag2";
			var track2 = new Track(tag2, new TrackData(tag2, x2, y2, z2, DateTime.Now));

			_currentSeparationEventsManager.AddEvent(new SeparationEvent(tag1, tag2, DateTime.Now));

			var trackList = new List<Track> { track1, track2 };

			// Act
			_separationEventController.CheckForSeparationEvents(trackList);

			// Assert
			Assert.Null(_currentSeparationEventsManager.FindEvent(tag1, tag2));
		}

		[TestCase(11000, 12000, 13000, 12000, 11000, 12800)]
		[TestCase(85000, 86000, 14900, 83000, 84000, 15000)]
		public void CheckForSeparationEvents_2ConflictingTracksNotInCurrentEvents_EventIsAddedToCurrentEventsManager(
			int x1, int y1, int z1, int x2, int y2, int z2)
		{
			// Arrange
			const string tag1 = "tag1";
			var track1 = new Track(tag1, new TrackData(tag1, x1, y1, z1, DateTime.Now));

			const string tag2 = "tag2";
			var track2 = new Track(tag2, new TrackData(tag2, x2, y2, z2, DateTime.Now));

			var trackList = new List<Track> { track1, track2 };

			var separationEvent = new SeparationEvent(tag1, tag2, DateTime.Now);
			_fakeSeparationEventGenerator.GenerateSeparationEvent(
				Arg.Any<String>(), Arg.Any<String>(), Arg.Any<DateTime>()).Returns(separationEvent);

			// Act
			_separationEventController.CheckForSeparationEvents(trackList);

			// Assert
			var foundEvent = _currentSeparationEventsManager.FindEvent(tag1, tag2);
			Assert.NotNull(foundEvent);
		}

		[TestCase(11000, 12000, 13000, 12000, 11000, 12800)]
		[TestCase(85000, 86000, 14900, 83000, 84000, 15000)]
		public void CheckForSeparationEvents_2ConflictingTracksNotInCurrentEvents_EventIsAddedToCurrentEventsManagerWithCorrectTag1(
				int x1, int y1, int z1, int x2, int y2, int z2)
		{
			// Arrange
			const string tag1 = "tag1";
			var track1 = new Track(tag1, new TrackData(tag1, x1, y1, z1, DateTime.Now));

			const string tag2 = "tag2";
			var track2 = new Track(tag2, new TrackData(tag2, x2, y2, z2, DateTime.Now));

			var trackList = new List<Track> { track1, track2 };

			var separationEvent = new SeparationEvent(tag1, tag2, DateTime.Now);
			_fakeSeparationEventGenerator.GenerateSeparationEvent(
				Arg.Any<String>(), Arg.Any<String>(), Arg.Any<DateTime>()).Returns(separationEvent);

			// Act
			_separationEventController.CheckForSeparationEvents(trackList);

			// Assert
			var foundEvent = _currentSeparationEventsManager.FindEvent(tag1, tag2);
			Assert.That(foundEvent.Tag1, Is.EqualTo(tag1));
		}

		[TestCase(11000, 12000, 13000, 12000, 11000, 12800)]
		[TestCase(85000, 86000, 14900, 83000, 84000, 15000)]
		public void CheckForSeparationEvents_2ConflictingTracksNotInCurrentEvents_EventIsAddedToCurrentEventsManagerWithCorrectTag2(
				int x1, int y1, int z1, int x2, int y2, int z2)
		{
			// Arrange
			const string tag1 = "tag1";
			var track1 = new Track(tag1, new TrackData(tag1, x1, y1, z1, DateTime.Now));

			const string tag2 = "tag2";
			var track2 = new Track(tag2, new TrackData(tag2, x2, y2, z2, DateTime.Now));

			var trackList = new List<Track> { track1, track2 };

			var separationEvent = new SeparationEvent(tag1, tag2, DateTime.Now);
			_fakeSeparationEventGenerator.GenerateSeparationEvent(
				Arg.Any<String>(), Arg.Any<String>(), Arg.Any<DateTime>()).Returns(separationEvent);

			// Act
			_separationEventController.CheckForSeparationEvents(trackList);

			// Assert
			var foundEvent = _currentSeparationEventsManager.FindEvent(tag1, tag2);
			Assert.That(foundEvent.Tag2, Is.EqualTo(tag2));
		}

		[TestCase(11000, 12000, 13000, 12000, 11000, 12800)]
		[TestCase(85000, 86000, 14900, 83000, 84000, 15000)]
		public void CheckForSeparationEvents_2ConflictingTracksNotInCurrentEvents_EventIsAddedToCurrentEventsManagerWithCorrectTimestamp(
				int x1, int y1, int z1, int x2, int y2, int z2)
		{
			// Arrange
			const string tag1 = "tag1";
			var track1 = new Track(tag1, new TrackData(tag1, x1, y1, z1, DateTime.Now));

			const string tag2 = "tag2";
			var track2 = new Track(tag2, new TrackData(tag2, x2, y2, z2, DateTime.Now));

			var trackList = new List<Track> { track1, track2 };

			var timestamp = DateTime.Now;
			var separationEvent = new SeparationEvent(tag1, tag2, timestamp);
			_fakeSeparationEventGenerator.GenerateSeparationEvent(
				Arg.Any<String>(), Arg.Any<String>(), Arg.Any<DateTime>()).Returns(separationEvent);

			// Act
			_separationEventController.CheckForSeparationEvents(trackList);

			// Assert
			var foundEvent = _currentSeparationEventsManager.FindEvent(tag1, tag2);
			Assert.That(foundEvent.Timestamp, Is.EqualTo(timestamp));
		}

		[TestCase(11000, 12000, 13000, 12000, 11000, 12800)]
		[TestCase(85000, 86000, 14900, 83000, 84000, 15000)]
		public void CheckForSeparationEvents_2ConflictingTracksWhoHasCurrentEvent_EventIsNotRemovedFromCurrentEventsManager(
			int x1, int y1, int z1, int x2, int y2, int z2)
		{
			// Arrange
			const string tag1 = "tag1";
			var track1 = new Track(tag1, new TrackData(tag1, x1, y1, z1, DateTime.Now));

			const string tag2 = "tag2";
			var track2 = new Track(tag2, new TrackData(tag2, x2, y2, z2, DateTime.Now));

			_currentSeparationEventsManager.AddEvent(new SeparationEvent(tag1, tag2, DateTime.Now));

			var trackList = new List<Track> { track1, track2 };

			// Act
			_separationEventController.CheckForSeparationEvents(trackList);

			// Assert
			var foundEvent = _currentSeparationEventsManager.FindEvent(tag1, tag2);
			Assert.NotNull(foundEvent);
		}

		[Test]
		public void CheckForSeparationEvents_CurrentEventsManagerListNumberOfElementsIs0_CorrectCurrentEventsListIsReturned()
		{
			// Arrange
			var expectedEventList = new List<SeparationEvent>();

			// Act
			var currentEvents = _separationEventController.CheckForSeparationEvents(new List<Track>());

			// Assert
			Assert.That(currentEvents, Is.EqualTo(expectedEventList));
		}

		[Test]
		public void CheckForSeparationEvents_1EventIsAddedToCurrentEventsManager_CorrectCurrentEventsListIsReturned()
		{
			// Arrange
			var separationEvent = new SeparationEvent("tag1", "tag2", DateTime.Now);
			_currentSeparationEventsManager.AddEvent(separationEvent);

			// Act
			var currentEvents = _separationEventController.CheckForSeparationEvents(new List<Track>());

			// Assert
			Assert.That(currentEvents[0], Is.EqualTo(separationEvent));
		}
	}
}
