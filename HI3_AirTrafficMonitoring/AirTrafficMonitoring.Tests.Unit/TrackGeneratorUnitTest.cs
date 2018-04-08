using AirTrafficMonitoring.Classes.TrackDataModels;
using AirTrafficMonitoring.Classes.TrackGenerator;
using NUnit.Framework;
using System;

namespace AirTrafficMonitoring.Tests.Unit
{
	[TestFixture]
	class TrackGeneratorUnitTest
	{
		private TrackGenerator _uut;

		[SetUp]
		public void Init()
		{
			_uut = new TrackGenerator();
		}

		[Test]
		public void GenerateTrack_GivenTrackDataIsNull_NullIsReturned()
		{
			// Act
			var result = _uut.GenerateTrack(null);

			// Assert
			Assert.Null(result);
		}

		[Test]
		public void GenerateTrack_GivenTrackDataIsActualObject_TrackReturnedHasCorrectTag()
		{
			// Arrange
			var tag = "someTag";
			var trackDataObject = new TrackData(tag, 0, 0, 0, DateTime.Now);

			// Act
			var result = _uut.GenerateTrack(trackDataObject);

			// Assert
			Assert.That(result.Tag, Is.EqualTo(tag));
		}

		[Test]
		public void GenerateTrack_GivenTrackDataIsActualObject_TrackReturnedHasCorrectCurrentTrack()
		{
			// Arrange
			var trackDataObject = new TrackData("someTag", 0, 0, 0, DateTime.Now);

			// Act
			var result = _uut.GenerateTrack(trackDataObject);

			// Assert
			Assert.That(result.CurrentTrack, Is.EqualTo(trackDataObject));
		}
	}
}
