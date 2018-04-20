using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.Tracks;
using NUnit.Framework;
using System;

namespace AirTrafficMonitoring.Tests.Unit.Tracks
{
	[TestFixture]
	class CurrentTracksManagerUnitTest
	{
		private CurrentTracksManager _uut;

		[SetUp]
		public void Init()
		{
			_uut = new CurrentTracksManager();
		}

		[Test]
		public void AddTrack_GivenTrackIsNull_CurrentTracksCountIsNotChanged()
		{
			// Arrange
			const string tag1 = "someTag";
			const string tag2 = "someOtherTag";
			var trackObject1 = new Track(tag1, new TrackData(tag1, 0, 0, 0, DateTime.Now));
			var trackObject2 = new Track(tag2, new TrackData(tag2, 10, 10, 10, DateTime.Now));
			_uut.AddTrack(trackObject1);
			_uut.AddTrack(trackObject2);

			// Act
			_uut.AddTrack(null);

			// Assert
			Assert.That(_uut.GetTrackCount(), Is.EqualTo(2));
		}

		[Test]
		public void AddTrack_GivenTrackIsActualObject_TrackIsAddedToList()
		{
			// Arrange
			const string tag1 = "someTag";
			var trackObject = new Track(tag1, new TrackData(tag1, 0, 0, 0, DateTime.Now));

			// Act
			_uut.AddTrack(trackObject);

			// Assert
			Assert.That(_uut.FindTrack(tag1), Is.EqualTo(trackObject));
		}

		[Test]
		public void FindTrack_GivenTrackIsNull_NullIsReturned()
		{
			// Act
			var foundTrack = _uut.FindTrack(null);

			// Assert
			Assert.Null(foundTrack);
		}

		[Test]
		public void FindTrack_CurrentTracksListContainsTrackWithGivenTag_CorrectObjectIsReturned()
		{
			// Arrange
			const string tag1 = "someTag";
			var trackObject = new Track(tag1, new TrackData(tag1, 0, 0, 0, DateTime.Now));
			_uut.AddTrack(trackObject);

			// Act
			var foundTrack = _uut.FindTrack(tag1);

			// Assert
			Assert.That(foundTrack, Is.EqualTo(trackObject));
		}

		[Test]
		public void FindTrack_CurrentTracksListDoesNotContainTrackWithGivenTag_NullIsReturned()
		{
			// Act
			var foundTrack = _uut.FindTrack("someTag");

			// Assert
			Assert.Null(foundTrack);
		}

		[TestCase(1)]
		[TestCase(5)]
		[TestCase(10)]
		public void GetTrackCount_CallWithVariousAmountOfTracksAdded_CorrectTrackCountIsReturned(int expectedTrackCount)
		{
			// Arrange
			for (int i = 0; i < expectedTrackCount; i++)
			{
				_uut.AddTrack(new Track("someTag", new TrackData("someTag", 0, 0, 0, DateTime.Now)));
			}

			// Act
			var trackCount = _uut.GetTrackCount();

			// Assert
			Assert.That(trackCount, Is.EqualTo(expectedTrackCount));
		}

		[Test]
		public void RemoveTrack_GivenTrackIsNull_CurrentTracksCountIsNotChanged()
		{
			// Arrange
			const string tag1 = "someTag";
			const string tag2 = "someOtherTag";
			var trackObject1 = new Track(tag1, new TrackData(tag1, 0, 0, 0, DateTime.Now));
			var trackObject2 = new Track(tag2, new TrackData(tag2, 10, 10, 10, DateTime.Now));
			_uut.AddTrack(trackObject1);
			_uut.AddTrack(trackObject2);

			// Act
			_uut.RemoveTrack(null);

			// Assert
			Assert.That(_uut.GetTrackCount(), Is.EqualTo(2));
		}

		[Test]
		public void RemoveTrack_GivenTrackIsActualObjectWhichIsInCurrentTracksList_TrackIsRemovedFromList()
		{
			// Arrange
			const string tag1 = "someTag";
			var trackObject = new Track(tag1, new TrackData(tag1, 0, 0, 0, DateTime.Now));
			_uut.AddTrack(trackObject);

			// Act
			_uut.RemoveTrack(trackObject);

			// Assert
			Assert.Null(_uut.FindTrack(tag1));
		}

		[Test]
		public void RemoveTrack_GivenTrackIsActualObjectWhichIsNotInCurrentTracksList_CurrentTracksCountIsNotChanged()
		{
			// Arrange
			const string tag1 = "someTag";
			const string tag2 = "someOtherTag";
			var trackObject1 = new Track(tag1, new TrackData(tag1, 0, 0, 0, DateTime.Now));
			var trackObject2 = new Track(tag2, new TrackData(tag2, 10, 10, 10, DateTime.Now));
			_uut.AddTrack(trackObject1);

			// Act
			_uut.RemoveTrack(trackObject2);

			// Assert
			Assert.That(_uut.GetTrackCount(), Is.EqualTo(1));
		}

		[Test]
		public void UpdateTrackData_GivenTrackDataIsNull_MethodDoesNotThrowException()
		{
			// Assert
			Assert.DoesNotThrow(() => _uut.UpdateTrack(null));
		}

		[Test]
		public void UpdateTrackData_GivenTrackDataIsNull_CurrentTracksCountIsNotChanged()
		{
			// Arrange
			const string tag1 = "someTag";
			const string tag2 = "someOtherTag";
			var trackObject1 = new Track(tag1, new TrackData(tag1, 0, 0, 0, DateTime.Now));
			var trackObject2 = new Track(tag2, new TrackData(tag2, 10, 10, 10, DateTime.Now));
			_uut.AddTrack(trackObject1);
			_uut.AddTrack(trackObject2);

			// Act
			_uut.UpdateTrack(null);

			// Assert
			Assert.That(_uut.GetTrackCount(), Is.EqualTo(2));
		}

		[Test]
		public void UpdateTrackData_GivenTrackDataHasTagNotInCurrentTracksList_CurrentTracksListHasNoTrackWithGivenTag()
		{
			// Arrange
			const string tag1 = "someTag";

			// Act
			_uut.UpdateTrack(new TrackData(tag1, 0, 0, 0, DateTime.Now));

			// Assert
			Assert.Null(_uut.FindTrack(tag1));
		}

		[Test]
		public void UpdateTrackData_GivenTrackDataHasTagInCurrentTracksList_TrackHasItsCurrentTrackUpdatedCorrectly()
		{
			// Arrange
			const string tag1 = "someTag";
			var trackObject = new Track(tag1, new TrackData(tag1, 0, 0, 0, DateTime.Now));
			_uut.AddTrack(trackObject);

			var newTrackData = new TrackData(tag1, 1000, 1000, 10000, DateTime.Now);

			// Act
			_uut.UpdateTrack(newTrackData);

			// Assert
			Assert.That(_uut.FindTrack(tag1).CurrentTrack, Is.EqualTo(newTrackData));
		}
	}
}
