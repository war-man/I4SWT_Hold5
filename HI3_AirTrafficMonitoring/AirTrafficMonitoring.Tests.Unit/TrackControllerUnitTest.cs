﻿using AirTrafficMonitoring.Classes.CurrentTracksManager;
using AirTrafficMonitoring.Classes.TrackController;
using AirTrafficMonitoring.Classes.TrackDataModels;
using AirTrafficMonitoring.Classes.TrackGenerator;
using AirTrafficMonitoring.Classes.TrackPrinter;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Tests.Unit
{
	[TestFixture]
	class TrackControllerUnitTest
	{
		private TrackController _uut;

		private ICurrentTracksManager _fakeCurrentTracksManager;
		private ITrackGenerator _fakeTrackGenerator;
		private ITrackPrinter _fakeTrackPrinter;

		[SetUp]
		public void Init()
		{
			_fakeCurrentTracksManager = Substitute.For<ICurrentTracksManager>();
			_fakeTrackGenerator = Substitute.For<ITrackGenerator>();
			_fakeTrackPrinter = Substitute.For<ITrackPrinter>();

			_uut = new TrackController(_fakeCurrentTracksManager, _fakeTrackGenerator, _fakeTrackPrinter);
		}

		[Test]
		public void AddTrackDataObjects_GivenListIsNull_CurrentTracksManagerTrackListIsNotRequested()
		{
			// Act
			_uut.AddTrackDataObjects(null);

			// Assert
			// Note that the "temp" variable is only there to make the compiler "happy"
			var temp = _fakeCurrentTracksManager.DidNotReceive().CurrentTracks;
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

		[Test]
		public void AddTrackDataObjects_GivenListContainsTrackNotInCurrentTracksList_GenerateOnTrackGeneratorCalledWithCorrectTrackData()
		{
			// Arrange
			var trackList = new List<Track>();
			_fakeCurrentTracksManager.CurrentTracks.Returns(trackList);

			var trackDataObject = new TrackData("tag", 10, 10, 10, DateTime.Now);
			var trackDataList = new List<TrackData> { trackDataObject };

			// Act
			_uut.AddTrackDataObjects(trackDataList);

			// Assert
			_fakeTrackGenerator.Received().GenerateTrack(trackDataObject);
		}

		[Test]
		public void AddTrackDataObjects_GivenListContainsTrackNotInCurrentTracksList_AddOnCurrentTracksManagerCalledWithCorrectTrack()
		{
			// Arrange
			var trackList = new List<Track>();
			_fakeCurrentTracksManager.CurrentTracks.Returns(trackList);

			var trackObject = new Track("tag", new TrackData("tag", 0, 0, 0, DateTime.Now));
			_fakeTrackGenerator.GenerateTrack(Arg.Any<TrackData>()).Returns(trackObject);

			var trackDataObject = new TrackData("tag2", 10, 10, 10, DateTime.Now);
			var trackDataList = new List<TrackData> { trackDataObject };

			// Act
			_uut.AddTrackDataObjects(trackDataList);

			// Assert
			_fakeCurrentTracksManager.Received().AddTrack(trackObject);
		}

		[Test]
		public void AddTrackDataObjects_GivenListContainsTrackInCurrentTracksList_UpdateOnCurrentTracksManagerCalledWithCorrectTrackData()
		{
			// Arrange
			var trackObject = new Track("tag", new TrackData("tag", 0, 0, 0, DateTime.Now));
			var trackList = new List<Track> { trackObject };
			_fakeCurrentTracksManager.CurrentTracks.Returns(trackList);
			_fakeCurrentTracksManager.FindTrack(Arg.Any<string>()).Returns(trackObject);

			var trackDataObject = new TrackData("tag", 10, 10, 10, DateTime.Now);
			var trackDataList = new List<TrackData> { trackDataObject };

			// Act
			_uut.AddTrackDataObjects(trackDataList);

			// Assert
			_fakeCurrentTracksManager.Received().UpdateTrack(trackDataObject);
		}

		[Test]
		public void AddTrackDataObjects_CurrentTracksManagerListNumberOfElementsIs0_PrintOnTrackPrinterNotCalled()
		{
			// Arrange
			var trackList = new List<Track>();
			_fakeCurrentTracksManager.CurrentTracks.Returns(trackList);
			_fakeCurrentTracksManager.GetTrackCount().Returns(0);

			// Act
			_uut.AddTrackDataObjects(new List<TrackData>());

			// Assert
			_fakeTrackPrinter.DidNotReceive().Print(Arg.Any<List<Track>>());
		}

		[Test]
		public void AddTrackDataObjects_CurrentTracksManagerListNumberOfElementsIs1_PrintOnTrackPrinterCalledWithCorrectTrackList()
		{
			// Arrange
			var trackObject = new Track("tag", new TrackData("tag", 0, 0, 0, DateTime.Now));
			var trackList = new List<Track> { trackObject };
			_fakeCurrentTracksManager.CurrentTracks.Returns(trackList);
			_fakeCurrentTracksManager.GetTrackCount().Returns(1);

			// Act
			_uut.AddTrackDataObjects(new List<TrackData>());

			// Assert
			_fakeTrackPrinter.Received().Print(trackList);
		}

		[Test]
		public void AddTrackDataObjects_CurrentTracksManagerListNumberOfElementsIs5_PrintOnTrackPrinterCalledWithCorrectTrackList()
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
			_uut.AddTrackDataObjects(new List<TrackData>());

			// Assert
			_fakeTrackPrinter.Received().Print(trackList);
		}
	}
}
