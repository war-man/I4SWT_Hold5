using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.SeparationEvents;
using NUnit.Framework;

namespace AirTrafficMonitoring.Tests.Integration.Step2_SeparationEvents
{
	[TestFixture]
	class Step2_2
	{
		private ICurrentSeparationEventsManager _uut;
		private String _tag1;
		private String _tag2;
		private DateTime _date;
		private SeparationEvent _event;

		[SetUp]
		public void Init()
		{
			_uut = new CurrentSeparationEventsManager();
			_tag1 = "testTag1";
			_tag2 = "testTag2";
			_date = DateTime.Now;
			_event = new SeparationEvent(_tag1,_tag2,_date);

		}

		[Test]
		public void Add_Check_Correct_event()
		{
			//Arrange
			//Act
			_uut.AddEvent(_event);

			//Assert
			Assert.That(_event.ToString(),Is.EqualTo(_uut.FindEvent(_tag1,_tag2).ToString()));
		}

	}
}
