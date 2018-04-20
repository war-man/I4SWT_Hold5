using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.SeparationEvents;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Tests.Unit.SeparationEvents
{
	[TestFixture]
	class CurrentSeparationEventsManagerUnitTest
	{
		private CurrentSeparationEventsManager _uut;
		private SeparationEvent _eventOne;
		private SeparationEvent _eventTwo;

		[SetUp]
		public void Init()
		{
			_uut = new CurrentSeparationEventsManager();
			_eventOne = new SeparationEvent("tag1", "tag2", new DateTime(1995, 08, 09));
			_eventTwo = new SeparationEvent("tag1e2", "tag2e2", new DateTime(1995, 04, 09));

		}

		[Test]
		public void Ctor_CurrentEvents_List_empty()
		{
			List<SeparationEvent> eventlist = _uut.CurrentEvents;

			Assert.That(eventlist, Is.Empty);
		}

		[Test]
		public void AddEvent_list_contains_one_Event()
		{
			_uut.AddEvent(_eventOne);
			List<SeparationEvent> eventlist = _uut.CurrentEvents;

			Assert.That(eventlist.Count, Is.EqualTo(1));
		}

		[Test]
		public void AddEvent_list_contains_right_event()
		{
			_uut.AddEvent(_eventOne);
			List<SeparationEvent> eventlist = _uut.CurrentEvents;

			Assert.That(eventlist[0], Is.EqualTo(_eventOne));
		}

		[Test]
		public void FindEvent_Add_Two_event_find_first_event()
		{
			_uut.AddEvent(_eventOne);
			_uut.AddEvent(_eventTwo);

			Assert.That(_uut.FindEvent(_eventTwo.Tag1, _eventTwo.Tag2), Is.EqualTo(_eventTwo));
		}

		[Test]
		public void FindEvent_Add_Two_event_find_secound_event()
		{
			_uut.AddEvent(_eventOne);
			_uut.AddEvent(_eventTwo);

			Assert.That(_uut.FindEvent("tag1e2", "tag2e2"), Is.EqualTo(_eventTwo));
		}

		[Test]
		public void FindEvent_Find_event_not_present()
		{
			_uut.AddEvent(_eventOne);
			_uut.AddEvent(_eventTwo);

			Assert.That(_uut.FindEvent("tag1e2", "NotThere"), Is.EqualTo(null));
		}

		[Test]
		public void FindEvent_same_tag()
		{
			Assert.That(null, Is.EqualTo(_uut.FindEvent(_eventOne.Tag1, _eventOne.Tag1)));
		}

		[Test]
		public void FindEvent_Add_Two_event_find_tag_switched()
		{
			_uut.AddEvent(_eventOne);
			_uut.AddEvent(_eventTwo);

			Assert.That(_uut.FindEvent("tag2e2", "tag1e2"), Is.EqualTo(_eventTwo));
		}

		[Test]
		public void GetEventCount_Ctor_returns_Zero()
		{
			Assert.AreEqual(0, _uut.GetEventCount());
		}

		[TestCase(1)]
		[TestCase(3)]
		[TestCase(10)]
		[TestCase(50)]
		public void GetEventCount_get_right_event(int number)
		{

			for (int i = 0; i < number; i++)
			{
				_uut.AddEvent(new SeparationEvent("tag1e" + i, "tag2e" + i, new DateTime(1995 + i, 08, 09)));
			}
			Assert.AreEqual(number, _uut.GetEventCount());
		}

		[Test]
		public void GetEventCount_remove_on_Empty_list_returns_Zero()
		{
			_uut.RemoveEvent("Tag1", "Tag2");
			Assert.AreEqual(0, _uut.GetEventCount());
		}

		[Test]
		public void GetEventCount_Add_and_remove_Zero()
		{
			_uut.AddEvent(_eventOne);
			_uut.RemoveEvent(_eventOne.Tag1, _eventOne.Tag2);

			Assert.AreEqual(0, _uut.GetEventCount());
		}

		[Test]
		public void RemoveEvent_event_not_there_everything_else_stay_the_samme()
		{
			_uut.AddEvent(_eventOne);
			_uut.AddEvent(_eventTwo);
			List<SeparationEvent> beforlist = _uut.CurrentEvents;

			//Act
			_uut.RemoveEvent("not", "There");

			//Assert
			Assert.That(beforlist, Is.EqualTo(_uut.CurrentEvents));
		}

		[Test]
		public void RemoveEvent_right()
		{
			_uut.AddEvent(_eventOne);
			_uut.AddEvent(_eventTwo);

			List<SeparationEvent> testlist = new List<SeparationEvent>();
			testlist.Add(_eventTwo);


			//Act
			_uut.RemoveEvent(_eventOne.Tag1, _eventOne.Tag2);

			//Assert
			Assert.That(testlist, Is.EquivalentTo(_uut.CurrentEvents));

		}

	}
}
