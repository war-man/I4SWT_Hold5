using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.Printer;
using AirTrafficMonitoring.Classes.SeparationEvents;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Tests.Unit.SeparationEvents
{
	[TestFixture]
	class SeparationEventControllerUnitTest
	{
		private SeparationEventController _uut;

		private ICurrentSeparationEventsManager _fakeCurrentSeparationEventsManager;
		private ISeparationEventGenerator _fakeSeparationEventGenerator;
		private ISeparationEventListFormatter _fakeSeparationEventListFormatter;
		private IPrinter _fakeSeparationEventLogger;

		[SetUp]
		public void Init()
		{
			_fakeCurrentSeparationEventsManager = Substitute.For<ICurrentSeparationEventsManager>();
			_fakeSeparationEventGenerator = Substitute.For<ISeparationEventGenerator>();
			_fakeSeparationEventListFormatter = Substitute.For<ISeparationEventListFormatter>();
			_fakeSeparationEventLogger = Substitute.For<IPrinter>();

			_uut = new SeparationEventController(
				_fakeCurrentSeparationEventsManager,
				_fakeSeparationEventGenerator,
				_fakeSeparationEventListFormatter,
				_fakeSeparationEventLogger);
		}

		[Test]
		public void CheckForSeparationEvents_GivenTrackListIsNull_FindEventOnCurrentEventsManagerNotCalled()
		{
			// Act
			_uut.CheckForSeparationEvents(null);

			// Assert
			_fakeCurrentSeparationEventsManager.DidNotReceive().FindEvent(Arg.Any<string>(), Arg.Any<string>());
		}

		[TestCase(11000, 12000, 13000, 15000, 16000, 12700)]
		[TestCase(14600, 15500, 13000, 11000, 12000, 13300)]
		public void CheckForSeparationEvents_2NonConflictingTracksNotInCurrentEvents_AddEventOnEventsManagerNotCalled(
			int x1, int y1, int z1, int x2, int y2, int z2)
		{
			// Arrange
			const string tag1 = "tag1";
			var track1 = new Track(tag1, new TrackData(tag1, x1, y1, z1, DateTime.Now));

			const string tag2 = "tag2";
			var track2 = new Track(tag2, new TrackData(tag2, x2, y2, z2, DateTime.Now));

			var trackList = new List<Track> { track1, track2 };

			// Act
			_uut.CheckForSeparationEvents(trackList);

			// Assert
			_fakeCurrentSeparationEventsManager.DidNotReceive().AddEvent(Arg.Any<SeparationEvent>());
		}

		[TestCase(11000, 12000, 13000, 15000, 16000, 12700)]
		[TestCase(14600, 15500, 13000, 11000, 12000, 13300)]
		public void CheckForSeparationEvents_2NonConflictingTracksWhoHasCurrentEvent_WriteLineOnEventLoggerCalledWithCorrectString(
			int x1, int y1, int z1, int x2, int y2, int z2)
		{
			// Arrange
			const string tag1 = "tag1";
			var track1 = new Track(tag1, new TrackData(tag1, x1, y1, z1, DateTime.Now));

			const string tag2 = "tag2";
			var track2 = new Track(tag2, new TrackData(tag2, x2, y2, z2, DateTime.Now));

			var trackList = new List<Track> { track1, track2 };

			var separationEvent = new SeparationEvent(tag1, tag2, DateTime.Now);
			_fakeCurrentSeparationEventsManager.FindEvent(tag1, tag2).Returns(separationEvent);

			// Act
			_uut.CheckForSeparationEvents(trackList);

			// Assert
			_fakeSeparationEventLogger.Received().WriteLine(Arg.Is<string>(
				str => str.ToLower().Contains("dropped") && str.Contains(separationEvent.ToString())));
		}

		[TestCase(11000, 12000, 13000, 15000, 16000, 12700)]
		[TestCase(14600, 15500, 13000, 11000, 12000, 13300)]
		public void CheckForSeparationEvents_2NonConflictingTracksWhoHasCurrentEvent_RemoveEventOnEventsManagerCalledWithCorrectTags(
			int x1, int y1, int z1, int x2, int y2, int z2)
		{
			// Arrange
			const string tag1 = "tag1";
			var track1 = new Track(tag1, new TrackData(tag1, x1, y1, z1, DateTime.Now));

			const string tag2 = "tag2";
			var track2 = new Track(tag2, new TrackData(tag2, x2, y2, z2, DateTime.Now));

			var trackList = new List<Track> { track1, track2 };

			var separationEvent = new SeparationEvent(tag1, tag2, DateTime.Now);
			_fakeCurrentSeparationEventsManager.FindEvent(tag1, tag2).Returns(separationEvent);

			// Act
			_uut.CheckForSeparationEvents(trackList);

			// Assert
			_fakeCurrentSeparationEventsManager.Received().RemoveEvent(tag1, tag2);
		}

		[TestCase(11000, 12000, 13000, 12000, 11000, 12800)]
		[TestCase(85000, 86000, 14900, 83000, 84000, 15000)]
		public void CheckForSeparationEvents_2ConflictingTracksNotInCurrentEvents_GenerateOnEventGeneratorCalledWithCorrectTags(
			int x1, int y1, int z1, int x2, int y2, int z2)
		{
			// Arrange
			const string tag1 = "tag1";
			var track1 = new Track(tag1, new TrackData(tag1, x1, y1, z1, DateTime.Now));

			const string tag2 = "tag2";
			var track2 = new Track(tag2, new TrackData(tag2, x2, y2, z2, DateTime.Now));

			var trackList = new List<Track> { track1, track2 };

			// Act
			_uut.CheckForSeparationEvents(trackList);

			// Assert
			_fakeSeparationEventGenerator.Received().GenerateSeparationEvent(track1, track2, Arg.Any<DateTime>());
		}

		[TestCase(11000, 12000, 13000, 12000, 11000, 12800)]
		[TestCase(85000, 86000, 14900, 83000, 84000, 15000)]
		public void CheckForSeparationEvents_2ConflictingTracksNotInCurrentEvents_AddOnEventsManagerCalledWithCorrectEvent(
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
				Arg.Any<Track>(), Arg.Any<Track>(), Arg.Any<DateTime>()).Returns(separationEvent);

			// Act
			_uut.CheckForSeparationEvents(trackList);

			// Assert
			_fakeCurrentSeparationEventsManager.Received().AddEvent(separationEvent);
		}

		[TestCase(11000, 12000, 13000, 12000, 11000, 12800)]
		[TestCase(85000, 86000, 14900, 83000, 84000, 15000)]
		public void CheckForSeparationEvents_2ConflictingTracksNotInCurrentEvents_WriteLineOnEventLoggerCalledWithCorrectString(
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
				Arg.Any<Track>(), Arg.Any<Track>(), Arg.Any<DateTime>()).Returns(separationEvent);

			// Act
			_uut.CheckForSeparationEvents(trackList);

			// Assert
			_fakeSeparationEventLogger.Received().WriteLine(Arg.Is<string>(
				str => str.ToLower().Contains("raised") && str.Contains(separationEvent.ToString())));
		}

		[TestCase(11000, 12000, 13000, 12000, 11000, 12800)]
		[TestCase(85000, 86000, 14900, 83000, 84000, 15000)]
		public void CheckForSeparationEvents_2ConflictingTracksWhoHasCurrentEvent_RemoveEventOnEventsManagerNotCalled(
			int x1, int y1, int z1, int x2, int y2, int z2)
		{
			// Arrange
			const string tag1 = "tag1";
			var track1 = new Track(tag1, new TrackData(tag1, x1, y1, z1, DateTime.Now));

			const string tag2 = "tag2";
			var track2 = new Track(tag2, new TrackData(tag2, x2, y2, z2, DateTime.Now));

			var trackList = new List<Track> { track1, track2 };

			var separationEvent = new SeparationEvent(tag1, tag2, DateTime.Now);
			_fakeCurrentSeparationEventsManager.FindEvent(tag1, tag2).Returns(separationEvent);

			// Act
			_uut.CheckForSeparationEvents(trackList);

			// Assert
			_fakeCurrentSeparationEventsManager.DidNotReceive().RemoveEvent(Arg.Any<string>(), Arg.Any<string>());
		}

		[Test]
		public void CheckForSeparationEvents_CurrentEventsManagerListNumberOfElementsIs0_CorrectCurrentEventsListIsReturned()
		{
			// Arrange
			var separationEventList = new List<SeparationEvent>();

			_fakeCurrentSeparationEventsManager.CurrentEvents.Returns(separationEventList);

			// Act
			var currentEvents = _uut.CheckForSeparationEvents(new List<Track>());

			// Assert
			Assert.That(currentEvents, Is.EqualTo(separationEventList));
		}

		[Test]
		public void CheckForSeparationEvents_CurrentEventsManagerListNumberOfElementsIs5_CorrectCurrentEventsListIsReturned()
		{
			// Arrange
			var separationEventObject1 = new SeparationEvent("tag1", "tag2", DateTime.Now);
			var separationEventObject2 = new SeparationEvent("tag3", "tag4", DateTime.Now);
			var separationEventObject3 = new SeparationEvent("tag5", "tag6", DateTime.Now);
			var separationEventObject4 = new SeparationEvent("tag7", "tag8", DateTime.Now);
			var separationEventObject5 = new SeparationEvent("tag9", "tag10", DateTime.Now);
			var separationEventList = new List<SeparationEvent>
			{
				separationEventObject1,
				separationEventObject2,
				separationEventObject3,
				separationEventObject4,
				separationEventObject5
			};

			_fakeCurrentSeparationEventsManager.CurrentEvents.Returns(separationEventList);

			// Act
			var currentEvents = _uut.CheckForSeparationEvents(new List<Track>());

			// Assert
			Assert.That(currentEvents, Is.EqualTo(separationEventList));
		}

		[Test]
		public void GetFormattedSeparationEvents_CallMethod_FormatOnSeparationEventListFormatterCalled()
		{
			// Act
			_uut.GetFormattedSeparationEvents();

			// Assert
			_fakeSeparationEventListFormatter.Received().Format(Arg.Any<List<SeparationEvent>>());
		}

		[Test]
		public void GetFormattedSeparationEvents_CurrentEventsManagerListNumberOfElementsIs0_CorrectCurrentEventsListIsGivenToFormatter()
		{
			// Arrange
			var separationEventList = new List<SeparationEvent>();

			_fakeCurrentSeparationEventsManager.CurrentEvents.Returns(separationEventList);

			// Act
			_uut.GetFormattedSeparationEvents();

			// Assert
			_fakeSeparationEventListFormatter.Received().Format(separationEventList);
		}

		[Test]
		public void GetFormattedSeparationEvents_CurrentEventsManagerListNumberOfElementsIs5_CorrectCurrentEventsListIsGivenToFormatter()
		{
			// Arrange
			var separationEventObject1 = new SeparationEvent("tag1", "tag2", DateTime.Now);
			var separationEventObject2 = new SeparationEvent("tag3", "tag4", DateTime.Now);
			var separationEventObject3 = new SeparationEvent("tag5", "tag6", DateTime.Now);
			var separationEventObject4 = new SeparationEvent("tag7", "tag8", DateTime.Now);
			var separationEventObject5 = new SeparationEvent("tag9", "tag10", DateTime.Now);
			var separationEventList = new List<SeparationEvent>
			{
				separationEventObject1,
				separationEventObject2,
				separationEventObject3,
				separationEventObject4,
				separationEventObject5
			};

			_fakeCurrentSeparationEventsManager.CurrentEvents.Returns(separationEventList);

			// Act
			_uut.GetFormattedSeparationEvents();

			// Assert
			_fakeSeparationEventListFormatter.Received().Format(separationEventList);
		}
	}
}