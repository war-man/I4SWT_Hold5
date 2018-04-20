using AirTrafficMonitoring.Classes.DataModels;
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

		private const int XCoordinate2 = 6000;
		private const int YCoordinate2 = 7000;
		private const int Altitude2 = 7000;

		private DateTime _timestamp2;
		private TrackData _trackData2;

		#endregion

		#region For Tests

		private const string Coordinates = "5000;6000";
		private const double CalculatedVelocity = 1414.21; //cut at 2 decimal points
		private const double CalculatedDirection = 45;
		private const double CalculatedInverseDirection = 225;

		#endregion

		[SetUp]
		public void Init()
		{

			_trackData = new TrackData(Tag, XCoordinate, YCoordinate, Altitude, _timestamp);

			_uut = new Track(Tag, _trackData);

			_timestamp2 = _timestamp.AddSeconds(DeltaTime);
			_trackData2 = new TrackData(Tag, XCoordinate2, YCoordinate2, Altitude2, _timestamp2);
		}

		[Test]
		public void Ctor_SingleTrackData_TagParsedCorrectly()
		{
			//Assert
			Assert.That(_uut.Tag, Is.EqualTo(Tag));
		}

		[Test]
		public void Ctor_SingleTrackData_CurrentTrackParsedCorrectly()
		{
			//Assert
			Assert.That(_uut.CurrentTrack, Is.EqualTo(_trackData));
		}

		[Test]
		public void Ctor_SingleTrackData_DirectionIsNull()
		{
			//Assert
			Assert.That(_uut.Direction, Is.Null);
		}

		[Test]
		public void Ctor_SingleTrackData_VelocityIsNull()
		{
			//Assert
			Assert.That(_uut.Velocity, Is.Null);
		}

		[Test]
		public void Ctor_SingleTrackData_PreviousTrackIsNull()
		{
			//Assert
			Assert.That(_uut.PreviousTrack, Is.Null);
		}

		[Test]
		public void AddNewTrackData_TagNotChanged()
		{
			//Act
			_uut.AddNewTrackData(_trackData2);

			//Assert
			Assert.That(_uut.Tag, Is.EqualTo(Tag));
		}

		[Test]
		public void AddNewTrackData_OneAdditionalTrackDataAdded_CurrentTrackIsNewTrackData()
		{
			//Act
			_uut.AddNewTrackData(_trackData2);

			//Assert
			Assert.That(_uut.CurrentTrack, Is.EqualTo(_trackData2));
		}

		[Test]
		public void AddNewTrackData_OneAdditionalTrackDataAdded_PreviousTrackIsOldTrackData()
		{
			//Act
			_uut.AddNewTrackData(_trackData2);

			//Assert
			Assert.That(_uut.PreviousTrack, Is.EqualTo(_trackData));
		}

		[Test]
		public void AddNewTrackData_OneAdditionalTrackDataAdded_DirectionCalculatedTo45Degrees()
		{
			//Act
			_uut.AddNewTrackData(_trackData2);

			//Assert
			Assert.That(_uut.Direction, Is.EqualTo(CalculatedDirection));
		}

		[Test]
		public void AddNewTrackData_TrackDataAddedInInverseOrder_DirectionCalculatedTo225Degrees()
		{
			//Arrange
			_uut.AddNewTrackData(_trackData2);

			//Act
			_uut.AddNewTrackData(_trackData);

			//Assert
			Assert.That(_uut.Direction, Is.EqualTo(CalculatedInverseDirection));
		}

		[Test]
		public void AddNewTrackData_OneAdditionalTrackDataAdded_VelocityCalculated()
		{

			//Act
			_uut.AddNewTrackData(_trackData2);

			//Assert
			Assert.That(_uut.Velocity, Is.EqualTo(CalculatedVelocity).Within(0.01));
		}

		[TestCase(XCoordinate)]
		[TestCase(YCoordinate)]
		public void CurrentPosition_ContainsCoordinates(int coordinate)
		{
			//Assert
			StringAssert.Contains(coordinate.ToString().ToLower(), _uut.CurrentPosition.ToLower());

		}

		[TestCase(XCoordinate2)]
		[TestCase(YCoordinate2)]
		public void CurrentPosition_OneAdditionalTrackDataAdded_PositionUpdated(int coordinate)
		{
			//Act
			_uut.AddNewTrackData(_trackData2);

			//Assert
			StringAssert.Contains(coordinate.ToString().ToLower(), _uut.CurrentPosition.ToLower());
		}

		[TestCase(Tag)]
		[TestCase(XCoordinate2)]
		[TestCase(YCoordinate2)]
		[TestCase(Altitude2)]
		[TestCase(CalculatedVelocity)]
		[TestCase(CalculatedDirection)]
		public void ToString_OneAdditionalTrackDataAdded_ContainsValues(object value)
		{
			//Act
			_uut.AddNewTrackData(_trackData2);

			//Assert
			StringAssert.Contains(value.ToString().ToLower(), _uut.ToString().ToLower());
		}

		[TestCase("Tag")]
		[TestCase("Coordinates")]
		[TestCase("Altitude")]
		[TestCase("Velocity")]
		[TestCase("Course")]
		[TestCase("Timestamp")]
		public void ToString_ValidData_ContainsLabels(string label)
		{
			//Act
			_uut.AddNewTrackData(_trackData2);

			//Assert
			StringAssert.Contains(label.ToLower(), _uut.ToString().ToLower());
		}
		}
}
