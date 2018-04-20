using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.SeparationEvents;
using NUnit.Framework;
using System;

namespace AirTrafficMonitoring.Tests.Unit.SeparationEvents
{
	[TestFixture]
	class SeparationEventGeneratorUnitTest
	{
		private SeparationEventGenerator _uut;

		private const string Tag1 = "hej123";
		private const string Tag2 = "jeh321";

		[SetUp]
		public void Init()
		{
			_uut = new SeparationEventGenerator();
		}

		[Test]
		public void GenerateSeparationEvent_ValidInput_DoesNotReturnNull()
		{
			// Arrange
			Track track1 = new Track(Tag1, new TrackData(Tag1, 1000, 5000, 600, DateTime.Now));
			Track track2 = new Track(Tag2, new TrackData(Tag2, 2000, 2000, 2000, DateTime.Now));

			// Act
			var seperationEvent = _uut.GenerateSeparationEvent(track1, track2, DateTime.Now);

			// Assert
			Assert.That(seperationEvent, Is.Not.EqualTo(null));
		}

		[Test]
		public void GenerateSeparationEvent_ValidInput_SeparationEventCreated()
		{
			// Arrange
			Track track1 = new Track(Tag1, new TrackData(Tag1, 1000, 5000, 600, DateTime.Now));
			Track track2 = new Track(Tag2, new TrackData(Tag2, 2000, 2000, 2000, DateTime.Now));

			// Act
			var seperationEvent = _uut.GenerateSeparationEvent(track1, track2, DateTime.Now);

			// Assert
			Assert.That(seperationEvent, Is.TypeOf<SeparationEvent>());
		}

		[Test]
		public void GenerateSeparationEvent_Track1IsNull_ReturnsNull()
		{
			// Arrange
			Track track1 = null;
			Track track2 = new Track(Tag2, new TrackData(Tag2, 2000, 2000, 2000, DateTime.Now));

			// Act
			var seperationEvent = _uut.GenerateSeparationEvent(track1, track2, DateTime.Now);

			// Assert
			Assert.That(seperationEvent, Is.EqualTo(null));
		}

		[Test]
		public void GenerateSeparationEvent_Track2IsNull_ReturnsNull()
		{
			// Arrange
			Track track1 = new Track(Tag1, new TrackData(Tag1, 1000, 5000, 600, DateTime.Now));
			Track track2 = null;

			// Act
			var seperationEvent = _uut.GenerateSeparationEvent(track1, track2, DateTime.Now);

			// Assert
			Assert.That(seperationEvent, Is.EqualTo(null));
		}

		[Test]
		public void GenerateSeparationEvent_TimestampIsDefault_ReturnsNull()
		{
			// Arrange
			Track track1 = new Track(Tag1, new TrackData(Tag1, 1000, 5000, 600, DateTime.Now));
			Track track2 = new Track(Tag2, new TrackData(Tag2, 2000, 2000, 2000, DateTime.Now));

			// Act
			var seperationEvent = _uut.GenerateSeparationEvent(track1, track2, new DateTime());

			// Assert
			Assert.That(seperationEvent, Is.EqualTo(null));

		}

		[Test]
		public void GenerateSeparationEvent_TagsAreTheSame_ReturnsNull()
		{
			// Arrange
			Track track1 = new Track(Tag1, new TrackData(Tag1, 1000, 5000, 600, DateTime.Now));
			Track track2 = new Track(Tag1, new TrackData(Tag1, 2000, 2000, 2000, DateTime.Now));

			// Act
			var seperationEvent = _uut.GenerateSeparationEvent(track1, track2, DateTime.Now);

			// Assert
			Assert.That(seperationEvent, Is.EqualTo(null));
		}
	}
}