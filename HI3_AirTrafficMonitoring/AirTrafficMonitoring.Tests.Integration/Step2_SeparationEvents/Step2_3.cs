using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.Printer;
using AirTrafficMonitoring.Classes.SeparationEvents;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Globalization;

namespace AirTrafficMonitoring.Tests.Integration.Step2_SeparationEvents
{
	[TestFixture]
	class Step2_3
	{
		// Actual classes
		private ISeparationEventController _separationEventController;
		private ICurrentSeparationEventsManager _currentSeparationEventsManager;
		private ISeparationEventGenerator _separationEventGenerator;
		private ISeparationEventListFormatter _separationEventListFormatter;

		// Fakes
		private IPrinter _fakeEventLogger;

		[SetUp]
		public void Init()
		{
			_fakeEventLogger = Substitute.For<IPrinter>();

			_currentSeparationEventsManager = new CurrentSeparationEventsManager();
			_separationEventGenerator = new SeparationEventGenerator();
			_separationEventListFormatter = new SeparationEventListFormatter();

			_separationEventController = new SeparationEventController(
				_currentSeparationEventsManager,
				_separationEventGenerator,
				_separationEventListFormatter,
				_fakeEventLogger);
		}

		[Test]
		public void GetFormattedSeparationEvents_OneEventInCurrentEventsList_FormattedListContainsTag1()
		{
			// Arrange
			const string tag1 = "qwerty";
			const string tag2 = "asdfgh";
			var timestamp = DateTime.Now;

			_currentSeparationEventsManager.AddEvent(new SeparationEvent(tag1, tag2, timestamp));

			// Act
			var formattedSeparationEvents = _separationEventController.GetFormattedSeparationEvents();

			// Assert
			Assert.That(formattedSeparationEvents, Contains.Substring(tag1));
		}

		[Test]
		public void GetFormattedSeparationEvents_OneEventInCurrentEventsList_FormattedListContainsTag2()
		{
			// Arrange
			const string tag1 = "qwerty";
			const string tag2 = "asdfgh";
			var timestamp = DateTime.Now;

			_currentSeparationEventsManager.AddEvent(new SeparationEvent(tag1, tag2, timestamp));

			// Act
			var formattedSeparationEvents = _separationEventController.GetFormattedSeparationEvents();

			// Assert
			Assert.That(formattedSeparationEvents, Contains.Substring(tag2));
		}

		[Test]
		public void GetFormattedSeparationEvents_OneEventInCurrentEventsList_FormattedListContainsTimestamp()
		{
			// Arrange
			const string tag1 = "qwerty";
			const string tag2 = "asdfgh";
			var timestamp = DateTime.Now;

			_currentSeparationEventsManager.AddEvent(new SeparationEvent(tag1, tag2, timestamp));

			// Act
			var formattedSeparationEvents = _separationEventController.GetFormattedSeparationEvents();

			// Assert
			Assert.That(formattedSeparationEvents, Contains.Substring(timestamp.ToString(CultureInfo.CurrentCulture)));
		}
	}
}
