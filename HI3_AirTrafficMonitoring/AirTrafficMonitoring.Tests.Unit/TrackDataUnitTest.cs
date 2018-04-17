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
		public void ToString_ContainsTag()
		{
			//Assert
			StringAssert.Contains(Tag,_uut.ToString());
		}

		[Test]
		public void ToString_ContainsXCoordinate()
		{
			//Assert
			StringAssert.Contains(XCoordinate.ToString(), _uut.ToString());
		}

		[Test]
		public void ToString_ContainsYCoordinate()
		{
			//Assert
			StringAssert.Contains(YCoordinate.ToString(), _uut.ToString());
		}

		[Test]
		public void ToString_ContainsAltitude()
		{
			//Assert
			StringAssert.Contains(Altitude.ToString(), _uut.ToString());
		}

		[Test]
		public void ToString_ContainsTimestamp()
		{
			//Assert
			StringAssert.Contains(_timestamp.ToString(), _uut.ToString());
		}
	}
}
