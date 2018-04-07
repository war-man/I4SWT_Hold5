using AirTrafficMonitoring.Classes.TrackDataModels;
using NUnit.Framework;
using System;

namespace AirTrafficMonitoring.Tests.Unit
{
	[TestFixture]
	class TrackUnitTest
	{
		private Track _uut;

		private string _tag = "12345";
		private int _xCoordinate = 5000;
		private int _yCoordinate = 6000;
		private int _altitude = 6000;
		private DateTime _timestamp = DateTime.Now;
		private TrackData _trackData;

		[SetUp]
		public void Init()
		{
			_trackData = new TrackData(_tag, _xCoordinate, _yCoordinate, _altitude, _timestamp);

			_uut = new Track(_tag, _trackData);
		}
	}
}
