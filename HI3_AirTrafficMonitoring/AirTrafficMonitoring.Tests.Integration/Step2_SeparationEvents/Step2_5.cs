using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.Printer;
using AirTrafficMonitoring.Classes.SeparationEvents;
using NUnit.Framework;

namespace AirTrafficMonitoring.Tests.Integration.Step2_SeparationEvents
{
	[TestFixture]
	class Step2_5
	{
		private ISeparationEventController _uut;
		private Track _track1;
		private Track _track2;
		private List<Track> _list;
		private DateTime _dateNow;
		private String _tag1 = "testTag1";
		private String _tag2 = "testTag2";
		private SeparationEvent _testSeprationEvent;

		[SetUp]
		public void Init()
		{
			_uut = new SeparationEventController(
				new CurrentSeparationEventsManager(),
				new SeparationEventGenerator(),
				new SeparationEventListFormatter(),
				new SeparationEventLogger());
			_list = new List<Track>();
			_dateNow = DateTime.Now;

			TrackData td1 = new TrackData(_tag1, 5000, 5000, 1000, _dateNow);
			TrackData td2 = new TrackData(_tag1, 5001, 5001, 1000, _dateNow);
			TrackData td3 = new TrackData(_tag2, 5002, 5002, 1000, _dateNow);
			TrackData td4 = new TrackData(_tag2, 5003, 5003, 1000, _dateNow);
			_testSeprationEvent = new SeparationEvent(_tag1, _tag2, _dateNow);

			_track1 = new Track("testTag1", td1);
			_track1.AddNewTrackData(td2);

			_track2 = new Track("testTag2", td3);
			_track2.AddNewTrackData(td4);

			_list.Add(_track1);
			_list.Add(_track2);


		}

		[Test]
		public void CurrentSeparationEventsManager_Right_Event_returned()
		{
			//Arrange
			var testSeprationEvent = new SeparationEvent(_tag1, _tag2, _dateNow);

			//Ack
			var testSeprationEventList = _uut.CheckForSeparationEvents(_list);

			//Assert
			Assert.AreEqual(testSeprationEventList[0].ToString(), testSeprationEvent.ToString());

		}
	}
}
