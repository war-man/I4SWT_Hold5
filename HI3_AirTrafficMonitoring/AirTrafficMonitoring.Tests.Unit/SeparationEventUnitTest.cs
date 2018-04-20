using System;
using AirTrafficMonitoring.Classes.DataModels;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Tests.Unit
{
	[TestFixture]
	class SeparationEventUnitTest
	{
		private SeparationEvent _uut;
		private const string Tag1 = "hej123";
		private const string Tag2 = "jeh321";
		private DateTime _timestamp;

		[SetUp]
		public void Init()
		{
			_timestamp = DateTime.Now;
			_uut = new SeparationEvent(Tag1, Tag2, _timestamp);
		}

		[Test]
		public void Ctor_Tag1ParsedCorrectly()
		{
			//Assert
			Assert.That(_uut.Tag1, Is.EqualTo(Tag1));
		}

		[Test]
		public void Ctor_Tag2ParsedCorrectly()
		{
			//Assert
			Assert.That(_uut.Tag2, Is.EqualTo(Tag2));
		}

		[Test]
		public void Ctor_TimestampParsedCorrectly()
		{
			//Assert
			Assert.That(_uut.Timestamp, Is.EqualTo(_timestamp));
		}

		[TestCase("Track 1")]
		[TestCase("Track 2")]
		[TestCase("Timestamp")]
		[TestCase(Tag1)]
		[TestCase(Tag2)]
		public void ToString_ContainsCorrectData(string label)
		{
			//Assert
			StringAssert.Contains(label.ToLower(), _uut.ToString().ToLower());
		}

		[Test]
		public void ToString_ContainsTimestamp()
		{
			//Assert
			StringAssert.Contains(_timestamp.ToString().ToLower(), _uut.ToString().ToLower());
		}
	}
}
