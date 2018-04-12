using NUnit.Framework;
using System;
using AirTrafficMonitoring.Classes.DataModels;

namespace AirTrafficMonitoring.Tests.Unit
{
	[TestFixture]
	class TrackDataUnitTest
	{
		private TrackData _uut;

		private const string Tag = "12345";
		private const int XCoordinate = 5000;
		private const int YCoordinate = 6000;
		private const int Altitude = 6000;
		private readonly DateTime _timestamp = DateTime.Now;

		[SetUp]
		public void Init()
		{
			_uut = new TrackData(Tag, XCoordinate, YCoordinate, Altitude, _timestamp);
		}

		[Test]
		public void Ctor_Tag_correct()
		{
			//Assert
			Assert.That(_uut.Tag, Is.EqualTo(Tag));
		}

		[Test]
		public void Ctor_Xcoordinate_correct()
		{
			//Assert
			Assert.That(_uut.XCoordinate, Is.EqualTo(XCoordinate));
		}

		[Test]
		public void Ctor_Ycoordinate_correct()
		{
			//Assert
			Assert.That(_uut.YCoordinate, Is.EqualTo(YCoordinate));
		}

		[Test]
		public void Ctor_Altitude_correct()
		{
			//Assert
			Assert.That(_uut.Altitude, Is.EqualTo(Altitude));
		}

		[Test]
		public void Ctor_Timestamp_correct()
		{
			//Assert
			Assert.That(_uut.Timestamp, Is.EqualTo(_timestamp));
		}

		[Test]
		public void ToString_correct()
		{
			//Arrange
			string testString = $"Tag: { Tag}\n" +
								$"X-Coordinate: {XCoordinate}\n" +
								$"Y-Coordinate: {YCoordinate}\n" +
								$"Altitude: {Altitude}\n" +
								$"Timestamp: {_timestamp}";

			//Assert
			Assert.That(_uut.ToString(), Is.EqualTo(testString));
		}
	}
}
