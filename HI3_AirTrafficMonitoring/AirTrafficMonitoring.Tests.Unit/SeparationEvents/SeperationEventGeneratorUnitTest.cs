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
			// Act
			var seperationEvent = _uut.GenerateSeparationEvent(Tag1, Tag2, DateTime.Now);

			// Assert
			Assert.That(seperationEvent, Is.Not.EqualTo(null));
		}

		[Test]
		public void GenerateSeparationEvent_ValidInput_SeparationEventCreated()
		{
			// Act
			var seperationEvent = _uut.GenerateSeparationEvent(Tag1, Tag2, DateTime.Now);

			// Assert
			Assert.That(seperationEvent, Is.TypeOf<SeparationEvent>());
		}

		[Test]
		public void GenerateSeparationEvent_Track1IsNull_ReturnsNull()
		{
			// Act
			var seperationEvent = _uut.GenerateSeparationEvent(null, Tag2, DateTime.Now);

			// Assert
			Assert.That(seperationEvent, Is.EqualTo(null));
		}

		[Test]
		public void GenerateSeparationEvent_Track2IsNull_ReturnsNull()
		{
			// Arrange

			// Act
			var seperationEvent = _uut.GenerateSeparationEvent(Tag1, null, DateTime.Now);

			// Assert
			Assert.That(seperationEvent, Is.EqualTo(null));
		}

		[Test]
		public void GenerateSeparationEvent_TimestampIsDefault_ReturnsNull()
		{
			// Act
			var seperationEvent = _uut.GenerateSeparationEvent(Tag1, Tag2, new DateTime());

			// Assert
			Assert.That(seperationEvent, Is.EqualTo(null));

		}

		[Test]
		public void GenerateSeparationEvent_TagsAreTheSame_ReturnsNull()
		{
			// Act
			var seperationEvent = _uut.GenerateSeparationEvent(Tag1, Tag1, DateTime.Now);

			// Assert
			Assert.That(seperationEvent, Is.EqualTo(null));
		}
	}
}