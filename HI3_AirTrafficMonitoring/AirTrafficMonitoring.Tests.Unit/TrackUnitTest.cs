using AirTrafficMonitoring.Classes.TrackDataModels;
using NUnit.Framework;
using System;

namespace AirTrafficMonitoring.Tests.Unit
{
	[TestFixture]
	class TrackUnitTest
	{
		private Track _uut;

		private string _tag = "12345";

		private const double DeltaTime = 1;

		#region First TrackData
		private int _xCoordinate = 5000;
		private int _yCoordinate = 6000;
		private int _altitude = 6000;
		private TrackData _trackData;
		private DateTime _timestamp = DateTime.Now;
		#endregion

		#region Secound TrackData
		private int _xCoordinateSecound = 6000;
		private int _yCoordinateSecound = 7000;
		private int _altitudeSecound = 7000;

		private DateTime _timestampSecound;
		private TrackData _trackDataSecound;
		#endregion

		#region Third TrackData
		private int _xCoordinateThird = 7000;
		private int _yCoordinateThird = 8000;
		private int _altitudeThird = 8000;

		private DateTime _timestampThird;
		private TrackData _trackDataThird;
		#endregion


		[SetUp]
		public void Init()
		{

			_trackData = new TrackData(_tag, _xCoordinate, _yCoordinate, _altitude, _timestamp);

			_uut = new Track(_tag, _trackData);

			_timestampSecound = _timestamp.AddSeconds(DeltaTime);
			_trackDataSecound = new TrackData(_tag, _xCoordinateSecound, _yCoordinateSecound, _altitudeSecound, _timestampSecound);

			_timestampThird = _timestampSecound.AddSeconds(DeltaTime);
			_trackDataThird = new TrackData(_tag, _xCoordinateThird, _yCoordinateThird, _altitudeThird, _timestampThird);

		}

		[Test]
		public void Ctor_Tag_Correct()
		{
			//Arrange
			//Act
			//Assert
			Assert.That(_uut.Tag, Is.EqualTo(_tag));
		}
		[Test]
		public void Ctor_CurrentTrack_Correct()
		{
			//Arrange
			//Act
			//Assert
			Assert.That(_uut.CurrentTrack, Is.EqualTo(_trackData));
		}
		[Test]
		public void Ctor_Direction_Null()
		{
			//Arrange
			//Act
			//Assert
			Assert.That(_uut.Direction, Is.Null);
		}
		[Test]
		public void Ctor_Velocity_Null()
		{
			//Arrange
			//Act
			//Assert
			Assert.That(_uut.Velocity, Is.Null);
		}
		[Test]
		public void Ctor_PreviousTrack_Null()
		{
			//Arrange
			//Act
			//Assert
			Assert.That(_uut.PreviousTrack, Is.Null);
		}
		[Test]
		public void Ctor_ToString_CurrentPos()
		{
			//Arrange
			string testString = _trackData.ToString();
			//Act
			//Assert
			Assert.That(testString, Is.EqualTo(_uut.ToString()));

		}

		[Test]
		public void Ctor_ToString_CurrentPosition()
		{
			//Arrange
			
			//Act
			//Assert
			Assert.That(_uut.CurrentPosition, Is.EqualTo(_trackData.XCoordinate + ";"+ _trackData.YCoordinate));

		}

		[Test]
		public void AddNewTrackData_Tag_notChanged()
		{
			//Arrange
			//Act
			_uut.AddNewTrackData(_trackDataSecound);
			//Assert
			Assert.That(_uut.Tag, Is.EqualTo(_tag));

		}
		[Test]
		public void AddNewTrackData_CurrentTrack_equal_newTrackData()
		{
			//Arrange
			//Act
			_uut.AddNewTrackData(_trackDataSecound);
			//Assert
			Assert.That(_uut.CurrentTrack, Is.EqualTo(_trackDataSecound));
		}
		[Test]
		public void AddNewTrackData_PreviousTrack_equal_oldTrackData()
		{
			//Arrange
			//Act
			_uut.AddNewTrackData(_trackDataSecound);
			//Assert
			Assert.That(_uut.PreviousTrack, Is.EqualTo(_trackData));
		}
		[Test]
		public void AddNewTrackData_Direction_calculated45degres()
		{
			//Arrange
			double xDiff = _xCoordinateSecound - _xCoordinate;
			double yDiff = _yCoordinateSecound - _yCoordinate;

			double direktion = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
			if (direktion < 0) direktion += 360;
			//Act
			_uut.AddNewTrackData(_trackDataSecound);
			//Assert
			Assert.That(_uut.Direction, Is.EqualTo(direktion));
			//Assert.That(_uut.Direction, Is.EqualTo(45));
		}
		[Test]
		public void AddNewTrackData_Direction_calculated225degres()
		{
			//Arrange
			double xDiff = _xCoordinate - _xCoordinateSecound;
			double yDiff = _yCoordinate - _yCoordinateSecound;

			double direktion = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
			if (direktion < 0) direktion += 360;

			//for going the oppiset way of 45 degres start by going to point secound 
			_uut.AddNewTrackData(_trackDataSecound);
			//Act
			//go back
			_uut.AddNewTrackData(_trackData);
			//Assert
			Assert.That(_uut.Direction, Is.EqualTo(direktion));
			//Assert.That(_uut.Direction, Is.EqualTo(225));
		}
		[Test]
		public void AddNewTrackData_Velocity_calculated()
		{
			//Arrange
			double xDiff = _xCoordinateSecound - _xCoordinate;
			double yDiff = _yCoordinateSecound - _yCoordinate;
			double speed = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)) / DeltaTime;
			//Act
			_uut.AddNewTrackData(_trackDataSecound);
			//Assert
			Assert.That(_uut.Velocity, Is.EqualTo(speed));
		}
		[Test]
		public void AddNewTrackData_ToString_CurrentPosition()
		{
			//Arrange

			//Act
			_uut.AddNewTrackData(_trackDataSecound);
			//Assert
			Assert.That(_uut.CurrentPosition, Is.EqualTo(_trackDataSecound.XCoordinate + ";" + _trackDataSecound.YCoordinate));

		}
	}
}
