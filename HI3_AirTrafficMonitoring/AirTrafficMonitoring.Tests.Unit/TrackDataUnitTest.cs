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
		public void Ctor_Tag_ParsedCorrectly()
		{
			//Assert
			Assert.That(_uut.Tag, Is.EqualTo(Tag));
		}

		[Test]
		public void Ctor_Xcoordinate_ParsedCorrectly()
		{
			//Assert
			Assert.That(_uut.XCoordinate, Is.EqualTo(XCoordinate));
		}

		[Test]
		public void Ctor_Ycoordinate_ParsedCorrectly()
		{
			//Assert
			Assert.That(_uut.YCoordinate, Is.EqualTo(YCoordinate));
		}

		[Test]
		public void Ctor_Altitude_ParsedCorrectly()
		{
			//Assert
			Assert.That(_uut.Altitude, Is.EqualTo(Altitude));
		}

		[Test]
		public void Ctor_Timestamp_ParsedCorrectly()
		{
			//Assert
			Assert.That(_uut.Timestamp, Is.EqualTo(_timestamp));
		}

		[TestCase(Tag)]
		[TestCase(XCoordinate)]
		[TestCase(YCoordinate)]
		[TestCase(Altitude)]
		public void ToString_ContainsValues(object value)
		{
			//Assert
			StringAssert.Contains(value.ToString().ToLower(), _uut.ToString().ToLower());
		}

		[Test]
		public void ToString_ContainsTimestamp()
		{
			//Assert
			StringAssert.Contains(_timestamp.ToString().ToLower(), _uut.ToString().ToLower());
		}

		[TestCase("Tag")]
		[TestCase("Coordinates")]
		[TestCase("Altitude")]
		[TestCase("Timestamp")]
		public void ToString_ContainsLabels(string label)
		{
			//Assert
			StringAssert.Contains(label.ToLower(), _uut.ToString().ToLower());
		}
	}
}
