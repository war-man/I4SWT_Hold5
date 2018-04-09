using AirTrafficMonitoring.Classes.TrackDataModels;
using NUnit.Framework;
using System;

namespace AirTrafficMonitoring.Tests.Unit
{
	[TestFixture]
	class TrackUnitTest
	{
		private Track _uut;

		private const string Tag = "12345";

		private const double DeltaTime = 1;

		#region First TrackData

		private const int XCoordinate = 5000;
		private const int YCoordinate = 6000;
		private const int Altitude = 6000;
		private TrackData _trackData;
		private DateTime _timestamp = DateTime.Now;

		#endregion

		#region Second TrackData

		private const int XCoordinateSecound = 6000;
		private const int YCoordinateSecound = 7000;
		private const int AltitudeSecound = 7000;

		private DateTime _timestampSecound;
		private TrackData _trackDataSecound;

		#endregion

		[SetUp]
		public void Init()
		{

			_trackData = new TrackData(Tag, XCoordinate, YCoordinate, Altitude, _timestamp);

			_uut = new Track(Tag, _trackData);

			_timestampSecound = _timestamp.AddSeconds(DeltaTime);
			_trackDataSecound = new TrackData(Tag, XCoordinateSecound, YCoordinateSecound, AltitudeSecound, _timestampSecound);
		}

		[Test]
		public void Ctor_Tag_Correct()
		{
			//Assert
			Assert.That(_uut.Tag, Is.EqualTo(Tag));
		}

		[Test]
		public void Ctor_CurrentTrack_Correct()
		{
			//Assert
			Assert.That(_uut.CurrentTrack, Is.EqualTo(_trackData));
		}

		[Test]
		public void Ctor_Direction_Null()
		{
			//Assert
			Assert.That(_uut.Direction, Is.Null);
		}

		[Test]
		public void Ctor_Velocity_Null()
		{
			//Assert
			Assert.That(_uut.Velocity, Is.Null);
		}

		[Test]
		public void Ctor_PreviousTrack_Null()
		{
			//Assert
			Assert.That(_uut.PreviousTrack, Is.Null);
		}

		[Test]
		public void Ctor_ToString_CurrentPos()
		{
			//Arrange
			string testString = _trackData.ToString();

			//Assert
			Assert.That(testString, Is.EqualTo(_uut.ToString()));

		}

		[Test]
		public void Ctor_ToString_CurrentPosition()
		{
			//Assert
			Assert.That(_uut.CurrentPosition, Is.EqualTo(_trackData.XCoordinate + ";" + _trackData.YCoordinate));
		}

		[Test]
		public void AddNewTrackData_Tag_notChanged()
		{
			//Act
			_uut.AddNewTrackData(_trackDataSecound);

			//Assert
			Assert.That(_uut.Tag, Is.EqualTo(Tag));
		}

		[Test]
		public void AddNewTrackData_CurrentTrack_equal_newTrackData()
		{
			//Act
			_uut.AddNewTrackData(_trackDataSecound);

			//Assert
			Assert.That(_uut.CurrentTrack, Is.EqualTo(_trackDataSecound));
		}

		[Test]
		public void AddNewTrackData_PreviousTrack_equal_oldTrackData()
		{
			//Act
			_uut.AddNewTrackData(_trackDataSecound);

			//Assert
			Assert.That(_uut.PreviousTrack, Is.EqualTo(_trackData));
		}

		[Test]
		public void AddNewTrackData_Direction_calculated45degres()
		{
			//Arrange
			double xDiff = XCoordinateSecound - XCoordinate;
			double yDiff = YCoordinateSecound - YCoordinate;

			double direction = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
			if (direction < 0) direction += 360;

			//Act
			_uut.AddNewTrackData(_trackDataSecound);

			//Assert
			Assert.That(_uut.Direction, Is.EqualTo(direction));
		}

		[Test]
		public void AddNewTrackData_Direction_calculated225degres()
		{
			//Arrange
			double xDiff = XCoordinate - XCoordinateSecound;
			double yDiff = YCoordinate - YCoordinateSecound;

			double direction = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
			if (direction < 0) direction += 360;

			//for going the oppiset way of 45 degres start by going to point secound
			_uut.AddNewTrackData(_trackDataSecound);

			//Act
			//go back
			_uut.AddNewTrackData(_trackData);

			//Assert
			Assert.That(_uut.Direction, Is.EqualTo(direction));
		}

		[Test]
		public void AddNewTrackData_Velocity_calculated()
		{
			//Arrange
			double xDiff = XCoordinateSecound - XCoordinate;
			double yDiff = YCoordinateSecound - YCoordinate;
			double speed = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)) / DeltaTime;

			//Act
			_uut.AddNewTrackData(_trackDataSecound);

			//Assert
			Assert.That(_uut.Velocity, Is.EqualTo(speed));
		}

		[Test]
		public void AddNewTrackData_ToString_CurrentPosition()
		{
			//Act
			_uut.AddNewTrackData(_trackDataSecound);

			//Assert
			Assert.That(_uut.CurrentPosition, Is.EqualTo(_trackDataSecound.XCoordinate + ";" + _trackDataSecound.YCoordinate));
		}
	}
}
