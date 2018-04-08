using AirTrafficMonitoring.Classes.TrackDataModels;
using NUnit.Framework;
using System;
using NUnit.Framework.Internal;

namespace AirTrafficMonitoring.Tests.Unit
{
	[TestFixture]
	class TrackDataUnitTest
	{
		private TrackData _uut;

		private string _tag = "12345";
		private int _xCoordinate = 5000;
		private int _yCoordinate = 6000;
		private int _altitude = 6000;
		private DateTime _timestamp = DateTime.Now;

		[SetUp]
		public void Init()
		{
			_uut = new TrackData(_tag, _xCoordinate, _yCoordinate, _altitude, _timestamp);
		}

		[Test]
		public void Ctor_Tag_correct()
		{
			//Arrange
			//Act
			//Assert
			Assert.That(_tag, Is.EqualTo(_uut.Tag));
		}
		[Test]
		public void Ctor_Xcoordinate_correct()
		{
			//Arrange
			//Act
			//Assert
			Assert.That(_xCoordinate, Is.EqualTo(_uut.XCoordinate));
		}
		[Test]
		public void Ctor_Ycoordinate_correct()
		{
			//Arrange
			//Act
			//Assert
			Assert.That(_yCoordinate, Is.EqualTo(_uut.YCoordinate));
		}
		[Test]
		public void Ctor_Altitude_correct()
		{
			//Arrange
			//Act
			//Assert
			Assert.That(_altitude, Is.EqualTo(_uut.Altitude));
		}
		[Test]
		public void Ctor_Timestamp_correct()
		{
			//Arrange
			//Act
			//Assert
			Assert.That(_timestamp, Is.EqualTo(_uut.Timestamp));
		}

		[Test]
		public void ToString_correct()
		{
			//Arrange
			string testString = $"Tag: { _tag}\n" +
			                    $"X-Coordinate: {_xCoordinate}\n" +
			                    $"Y-Coordinate: {_yCoordinate}\n" +
			                    $"Altitude: {_altitude}\n" +
			                    $"Timestamp: {_timestamp}";
			//Act
			//Assert
			Assert.That(testString, Is.EqualTo(_uut.ToString()));
		}
	}
}
