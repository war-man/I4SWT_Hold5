using System;
using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.Printer;
using AirTrafficMonitoring.Classes.SeparationEvents;
using NUnit.Framework;

namespace AirTrafficMonitoring.Tests.Integration.Step2_SeparationEvents
{
	[TestFixture]
	class Step2_1
	{
		private ISeparationEventGenerator _uut;

		[SetUp]
		public void Init()
		{
			_uut = new SeparationEventGenerator(); 
													
		}

		[Test]
		public void GenerateSeparationEvent_Check_Correct_event()
		{
			//Arrane
			string Tag1 = "testTag1";
			string Tag2 = "testTag2";
			var date = DateTime.Now;
			var testSeparationEvent = new SeparationEvent(Tag1, Tag2, date);

			// Act
			var seperationEvent = _uut.GenerateSeparationEvent(Tag1, Tag2, date);

			// Assert
			Assert.That(seperationEvent.ToString(), Is.EqualTo(testSeparationEvent.ToString()));
		}
	}
}
