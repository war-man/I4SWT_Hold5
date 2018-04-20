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
	class Step2_3
	{
		private ISeparationEventListFormatter _uut;
		private SeparationEvent _event1;
		private List<SeparationEvent> _list;

		[SetUp]
		public void Init()
		{
			_uut = new SeparationEventListFormatter();
			_event1 = new SeparationEvent("testTag11", "testTag12",DateTime.Now);
			_list = new List<SeparationEvent>();
			_list.Add(_event1);

		}

		[Test]
		public void Format_correct_string()
		{
			//Arrange
			var test = _event1.ToString();
			//Ack

			//Assert
			StringAssert.Contains(_event1.ToString(), _uut.Format(_list));
		}
	}
}
