using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.SeparationEvents;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirTrafficMonitoring.Tests.Unit.SeparationEvents
{
	[TestFixture]
	class SeparationEventListFormatterUnitTest
	{
		private SeparationEventListFormatter _uut;

		private SeparationEvent _event1;
		private SeparationEvent _event2;

		private List<SeparationEvent> _eventList;

		[SetUp]
		public void Init()
		{
			_uut = new SeparationEventListFormatter();

			_event1 = new SeparationEvent("hej123", "jeh321", DateTime.Now);
			_event2 = new SeparationEvent("abc456", "cba654", DateTime.Now);

			_eventList = new List<SeparationEvent>();
		}

		[Test]
		public void Format_EmptyList_OutputIsEmpty()
		{
			// Assert
			Assert.That(_uut.Format(_eventList), Is.EqualTo(""));
		}

		[Test]
		public void Format_OneElement_CountIsCorrect()
		{
			// Arrange
			_eventList.Add(_event1);

			// Assert
			StringAssert.Contains("separation events: 1", _uut.Format(_eventList).ToLower());
		}

		[Test]
		public void Format_TwoElements_CountIsCorrect()
		{
			// Arrange
			_eventList.Add(_event1);
			_eventList.Add(_event2);

			// Assert
			StringAssert.Contains("separation events: 2", _uut.Format(_eventList).ToLower());
		}

		[Test]
		public void Format_TwoElements_CountWrittenOnce()
		{
			// Arrange
			_eventList.Add(_event1);
			_eventList.Add(_event2);

			// Act
			var formattedString = _uut.Format(_eventList);
			var tagCount = formattedString.Select((c, i) => formattedString.Substring(i)).Count(sub => sub.StartsWith("separation events:"));

			// Assert
			Assert.AreEqual(tagCount, 1);
		}

		[Test]
		public void Format_OneElement_OneElementPrinted()
		{
			// Arrange
			_eventList.Add(_event1);

			// Act
			var formattedString = _uut.Format(_eventList);
			var tagCount = formattedString.Select((c, i) => formattedString.Substring(i)).Count(sub => sub.StartsWith("Track 1"));

			// Assert
			Assert.AreEqual(tagCount, 1);
		}

		[Test]
		public void Format_TwoElements_TwoElementsPrinted()
		{
			// Arrange
			_eventList.Add(_event1);
			_eventList.Add(_event2);

			// Act
			var formattedString = _uut.Format(_eventList);
			var tagCount = formattedString.Select((c, i) => formattedString.Substring(i)).Count(sub => sub.StartsWith("Track 1"));

			// Assert
			Assert.AreEqual(tagCount, 2);
		}

	}
}