using AirTrafficMonitoring.Classes.AirTrafficController;
using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Printer;
using AirTrafficMonitoring.Classes.SeparationEvents;
using AirTrafficMonitoring.Classes.Tracks;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TransponderReceiver;

namespace AirTrafficMonitoring.Tests.Unit.AirTrafficController
{
	[TestFixture]
	class AirTrafficControllerUnitTest
	{
		private IAirTrafficController _uut;

		private ITransponderReceiver _fakeTransponderReceiver;
		private ITrackDataObjectifier _fakeTrackDataObjectifier;
		private ISeparationEventController _fakeSeparationEventController;
		private ITrackController _fakeTrackController;
		private IPrinter _fakeConsolePrinter;

		[SetUp]
		public void Init()
		{
			_fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
			_fakeTrackDataObjectifier = Substitute.For<ITrackDataObjectifier>();
			_fakeSeparationEventController = Substitute.For<ISeparationEventController>();
			_fakeTrackController = Substitute.For<ITrackController>();
			_fakeConsolePrinter = Substitute.For<IPrinter>();

			_uut = new Classes.AirTrafficController.AirTrafficController(
				_fakeTransponderReceiver,
				_fakeTrackDataObjectifier,
				_fakeSeparationEventController,
				_fakeTrackController,
				_fakeConsolePrinter);
		}

		[Test]
		public void OnTransponderDataReady_GivenEventArgsIsNull_ObjectifierNotCalled()
		{
			// Act
			_uut.OnTransponderDataReady(this, null);

			// Assert
			_fakeTrackDataObjectifier.DidNotReceive().Objectify(Arg.Any<List<string>>());
		}

		[Test]
		public void OnTransponderDataReady_GivenTransponderDataListIsNull_ObjectifierCalledWithNullTransponderData()
		{
			// Act
			var args = new RawTransponderDataEventArgs(null);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeTrackDataObjectifier.Received().Objectify(null);
		}

		[Test]
		public void OnTransponderDataReady_GivenEventArgsIsActualList_ObjectifierCalledWithCorrectTransponderData()
		{
			// Arrange
			var transponderDataList = new List<string>();

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeTrackDataObjectifier.Received().Objectify(transponderDataList);
		}

		[Test]
		public void OnTransponderDataReady_ObjectifierReturnsNullTrackDataList_TrackControllerIsCalledWithNullTrackDataList()
		{
			// Arrange
			var transponderDataList = new List<string>();
			_fakeTrackDataObjectifier.Objectify(Arg.Any<List<string>>()).ReturnsNull();

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeTrackController.Received().AddTrackDataObjects(null);
		}

		[Test]
		public void OnTransponderDataReady_ObjectifierReturnsActualTrackDataList_TrackControllerIsCalledWithCorrectTrackDataList()
		{
			// Arrange
			var transponderDataList = new List<string>();

			var trackDataList = new List<TrackData>();
			_fakeTrackDataObjectifier.Objectify(Arg.Any<List<string>>()).Returns(trackDataList);

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeTrackController.Received().AddTrackDataObjects(trackDataList);
		}

		[Test]
		public void OnTransponderDataReady_TrackControllerReturnsNullCurrentTracksList_SeparationEventControllerIsCalledWithNullTrackList()
		{
			// Arrange
			var transponderDataList = new List<string>();

			_fakeTrackController.AddTrackDataObjects(Arg.Any<List<TrackData>>()).ReturnsNull();

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeSeparationEventController.Received().CheckForSeparationEvents(null);
		}

		[Test]
		public void OnTransponderDataReady_TrackControllerReturnsNullCurrentTracksList_ClearOnConsolePrinterNotCalled()
		{
			// Arrange
			var transponderDataList = new List<string>();

			_fakeTrackController.AddTrackDataObjects(Arg.Any<List<TrackData>>()).ReturnsNull();

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeConsolePrinter.DidNotReceive().Clear();
		}

		[Test]
		public void OnTransponderDataReady_TrackControllerReturnsNullCurrentTracksList_WriteLineOnConsolePrinterNotCalled()
		{
			// Arrange
			var transponderDataList = new List<string>();

			_fakeTrackController.AddTrackDataObjects(Arg.Any<List<TrackData>>()).ReturnsNull();

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeConsolePrinter.DidNotReceive().WriteLine(Arg.Any<string>());
		}

		[Test]
		public void OnTransponderDataReady_TrackControllerReturnsActualCurrentTracksList_SeparationEventControllerIsCalledWithCorrectTrackList()
		{
			// Arrange
			var transponderDataList = new List<string>();

			var trackList = new List<Track>();
			_fakeTrackController.AddTrackDataObjects(Arg.Any<List<TrackData>>()).Returns(trackList);

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeSeparationEventController.Received().CheckForSeparationEvents(trackList);
		}

		[Test]
		public void OnTransponderDataReady_TrackControllerReturnsTracksListWith0Elements_ClearOnConsolePrinterNotCalled()
		{
			// Arrange
			var transponderDataList = new List<string>();

			var trackList = new List<Track>();
			_fakeTrackController.AddTrackDataObjects(Arg.Any<List<TrackData>>()).Returns(trackList);

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeConsolePrinter.DidNotReceive().Clear();
		}

		[Test]
		public void OnTransponderDataReady_TrackControllerReturnsTracksListWith0Elements_WriteLineOnConsolePrinterNotCalled()
		{
			// Arrange
			var transponderDataList = new List<string>();

			var trackList = new List<Track>();
			_fakeTrackController.AddTrackDataObjects(Arg.Any<List<TrackData>>()).Returns(trackList);

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeConsolePrinter.DidNotReceive().WriteLine(Arg.Any<string>());
		}

		[Test]
		public void OnTransponderDataReady_TrackControllerReturnsTracksListWith1Element_ClearOnConsolePrinterCalled()
		{
			// Arrange
			var transponderDataList = new List<string>();

			var trackList = new List<Track>
			{
				new Track("tag1", new TrackData("tag1", 0, 0, 0, DateTime.Now))
			};
			_fakeTrackController.AddTrackDataObjects(Arg.Any<List<TrackData>>()).Returns(trackList);

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeConsolePrinter.Received().Clear();
		}

		[Test]
		public void OnTransponderDataReady_TrackControllerReturnsTracksListWith1Element_GetFormattedTracksCalledOnTrackControllerCalled()
		{
			// Arrange
			var transponderDataList = new List<string>();

			var trackList = new List<Track>
			{
				new Track("tag1", new TrackData("tag1", 0, 0, 0, DateTime.Now))
			};
			_fakeTrackController.AddTrackDataObjects(Arg.Any<List<TrackData>>()).Returns(trackList);

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeTrackController.Received().GetFormattedCurrentTracks();
		}

		[Test]
		public void OnTransponderDataReady_TrackControllerReturnsTracksListWith1Element_WriteLineOnConsolePrinterCalledWithCorectString()
		{
			// Arrange
			var transponderDataList = new List<string>();

			var trackList = new List<Track>
			{
				new Track("tag1", new TrackData("tag1", 0, 0, 0, DateTime.Now))
			};
			_fakeTrackController.AddTrackDataObjects(Arg.Any<List<TrackData>>()).Returns(trackList);

			const string expectedString = "someListOfTracks";
			_fakeTrackController.GetFormattedCurrentTracks().Returns(expectedString);

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeConsolePrinter.Received().WriteLine(expectedString);
		}

		[Test]
		public void OnTransponderDataReady_TrackControllerReturnsTracksListWith5Elements_ClearOnConsolePrinterCalled()
		{
			// Arrange
			var transponderDataList = new List<string>();

			var trackList = new List<Track>
			{
				new Track("tag1", new TrackData("tag1", 0, 0, 0, DateTime.Now)),
				new Track("tag2", new TrackData("tag2", 0, 0, 0, DateTime.Now)),
				new Track("tag3", new TrackData("tag3", 0, 0, 0, DateTime.Now)),
				new Track("tag4", new TrackData("tag4", 0, 0, 0, DateTime.Now)),
				new Track("tag5", new TrackData("tag5", 0, 0, 0, DateTime.Now))
			};
			_fakeTrackController.AddTrackDataObjects(Arg.Any<List<TrackData>>()).Returns(trackList);

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeConsolePrinter.Received().Clear();
		}

		[Test]
		public void OnTransponderDataReady_TrackControllerReturnsTracksListWith5Elements_GetFormattedTracksCalledOnTrackControllerCalled()
		{
			// Arrange
			var transponderDataList = new List<string>();

			var trackList = new List<Track>
			{
				new Track("tag1", new TrackData("tag1", 0, 0, 0, DateTime.Now)),
				new Track("tag2", new TrackData("tag2", 0, 0, 0, DateTime.Now)),
				new Track("tag3", new TrackData("tag3", 0, 0, 0, DateTime.Now)),
				new Track("tag4", new TrackData("tag4", 0, 0, 0, DateTime.Now)),
				new Track("tag5", new TrackData("tag5", 0, 0, 0, DateTime.Now))
			};
			_fakeTrackController.AddTrackDataObjects(Arg.Any<List<TrackData>>()).Returns(trackList);

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeTrackController.Received().GetFormattedCurrentTracks();
		}

		[Test]
		public void OnTransponderDataReady_TrackControllerReturnsTracksListWith5Elements_WriteLineOnConsolePrinterCalledWithCorectString()
		{
			// Arrange
			var transponderDataList = new List<string>();

			var trackList = new List<Track>
			{
				new Track("tag1", new TrackData("tag1", 0, 0, 0, DateTime.Now)),
				new Track("tag2", new TrackData("tag2", 0, 0, 0, DateTime.Now)),
				new Track("tag3", new TrackData("tag3", 0, 0, 0, DateTime.Now)),
				new Track("tag4", new TrackData("tag4", 0, 0, 0, DateTime.Now)),
				new Track("tag5", new TrackData("tag5", 0, 0, 0, DateTime.Now))
			};
			_fakeTrackController.AddTrackDataObjects(Arg.Any<List<TrackData>>()).Returns(trackList);

			const string expectedString = "someListOfTracks";
			_fakeTrackController.GetFormattedCurrentTracks().Returns(expectedString);

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeConsolePrinter.Received().WriteLine(expectedString);
		}

		[Test]
		public void OnTransponderDataReady_EventControllerReturnsNullSeparationEventsList_ClearOnConsolePrinterNotCalled()
		{
			// Arrange
			var transponderDataList = new List<string>();

			_fakeSeparationEventController.CheckForSeparationEvents(Arg.Any<List<Track>>()).ReturnsNull();

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeConsolePrinter.DidNotReceive().Clear();
		}

		[Test]
		public void OnTransponderDataReady_EventControllerReturnsNullSeparationEventsList_WriteLineOnConsolePrinterNotCalled()
		{
			// Arrange
			var transponderDataList = new List<string>();

			_fakeSeparationEventController.CheckForSeparationEvents(Arg.Any<List<Track>>()).ReturnsNull();

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeConsolePrinter.DidNotReceive().WriteLine(Arg.Any<string>());
		}

		[Test]
		public void OnTransponderDataReady_EventControllerReturnsEventsListWith0Elements_ClearOnConsolePrinterNotCalled()
		{
			// Arrange
			var transponderDataList = new List<string>();

			var separationEventList = new List<SeparationEvent>();
			_fakeSeparationEventController.CheckForSeparationEvents(Arg.Any<List<Track>>()).Returns(separationEventList);

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeConsolePrinter.DidNotReceive().Clear();
		}

		[Test]
		public void OnTransponderDataReady_EventControllerReturnsEventsListWith0Elements_WriteLineOnConsolePrinterNotCalled()
		{
			// Arrange
			var transponderDataList = new List<string>();

			var separationEventList = new List<SeparationEvent>();
			_fakeSeparationEventController.CheckForSeparationEvents(Arg.Any<List<Track>>()).Returns(separationEventList);

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeConsolePrinter.DidNotReceive().WriteLine(Arg.Any<string>());
		}

		[Test]
		public void OnTransponderDataReady_EventControllerReturnsEventsListWith1Element_GetFormattedTracksCalledOnEventControllerCalled()
		{
			// Arrange
			var transponderDataList = new List<string>();

			var separationEventList = new List<SeparationEvent>
			{
				new SeparationEvent("tag1", "tag2", DateTime.Now)
			};
			_fakeSeparationEventController.CheckForSeparationEvents(Arg.Any<List<Track>>()).Returns(separationEventList);

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeSeparationEventController.Received().GetFormattedSeparationEvents();
		}

		[Test]
		public void OnTransponderDataReady_EventControllerReturnsEventsListWith1Element_WriteLineOnConsolePrinterCalledWithCorectString()
		{
			// Arrange
			var transponderDataList = new List<string>();

			var separationEventList = new List<SeparationEvent>
			{
				new SeparationEvent("tag1", "tag2", DateTime.Now)
			};
			_fakeSeparationEventController.CheckForSeparationEvents(Arg.Any<List<Track>>()).Returns(separationEventList);

			const string expectedString = "someListOfSeparationEvents";
			_fakeSeparationEventController.GetFormattedSeparationEvents().Returns(expectedString);

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeConsolePrinter.Received().WriteLine(expectedString);
		}

		[Test]
		public void OnTransponderDataReady_EventControllerReturnsEventsListWith5Elements_GetFormattedTracksCalledOnEventControllerCalled()
		{
			// Arrange
			var transponderDataList = new List<string>();

			var separationEventList = new List<SeparationEvent>
			{
				new SeparationEvent("tag1", "tag2", DateTime.Now),
				new SeparationEvent("tag3", "tag4", DateTime.Now),
				new SeparationEvent("tag5", "tag6", DateTime.Now),
				new SeparationEvent("tag7", "tag8", DateTime.Now),
				new SeparationEvent("tag9", "tag10", DateTime.Now)
			};
			_fakeSeparationEventController.CheckForSeparationEvents(Arg.Any<List<Track>>()).Returns(separationEventList);

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeSeparationEventController.Received().GetFormattedSeparationEvents();
		}

		[Test]
		public void OnTransponderDataReady_EventControllerReturnsEventsListWith5Elements_WriteLineOnConsolePrinterCalledWithCorectString()
		{
			// Arrange
			var transponderDataList = new List<string>();

			var separationEventList = new List<SeparationEvent>
			{
				new SeparationEvent("tag1", "tag2", DateTime.Now),
				new SeparationEvent("tag3", "tag4", DateTime.Now),
				new SeparationEvent("tag5", "tag6", DateTime.Now),
				new SeparationEvent("tag7", "tag8", DateTime.Now),
				new SeparationEvent("tag9", "tag10", DateTime.Now)
			};
			_fakeSeparationEventController.CheckForSeparationEvents(Arg.Any<List<Track>>()).Returns(separationEventList);

			const string expectedString = "someListOfSeparationEvents";
			_fakeSeparationEventController.GetFormattedSeparationEvents().Returns(expectedString);

			// Act
			var args = new RawTransponderDataEventArgs(transponderDataList);
			_fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(args);

			// Assert
			_fakeConsolePrinter.Received().WriteLine(expectedString);
		}
	}
}
