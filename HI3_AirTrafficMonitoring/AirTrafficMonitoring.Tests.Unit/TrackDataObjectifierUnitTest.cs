using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Tracks;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TransponderReceiver;

namespace AirTrafficMonitoring.Tests.Unit
{
	[TestFixture]
	class TrackDataObjectifierUnitTest
	{
		private TrackDataObjectifier _uut;

		private ITransponderReceiver _fakeTransponderReceiver;
		private ITrackController _fakeTrackController;

		private string _tag = "ABC123";
		private int _xCoordinate = 5000;
		private int _yCoordinate = 6000;
		private int _altitude = 6000;
		private DateTime _timestamp = new DateTime(2015, 10, 6, 21, 34, 56, 789);

		[SetUp]
		public void Init()
		{
			_fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
			_fakeTrackController = Substitute.For<ITrackController>();

			_uut = new TrackDataObjectifier();
		}

		[Test]
		public void OnTransponderDataReady_CorrectInput_TrackManagerCalled()
		{
			//Arrange
			var list = new List<string>();
			list.Add("ABC123;5000;6000;6000;20151006213456789");

			//Act
			_fakeTransponderReceiver.TransponderDataReady +=
				Raise.EventWith(_fakeTransponderReceiver, new RawTransponderDataEventArgs(list));

			//Assert
			_fakeTrackController.Received(1).AddTrackDataObjects(Arg.Any<List<TrackData>>());
		}

		[Test]
		public void OnTransponderDataReady_CorrectInput_TagParsedCorrectly()
		{
			//Arrange
			var list = new List<string>();
			list.Add("ABC123;5000;6000;6000;20151006213456789");

			List<TrackData> parameterList = new List<TrackData>();
			_fakeTrackController.AddTrackDataObjects(Arg.Do<List<TrackData>>(
				ls => parameterList = ls));

			//Act
			_fakeTransponderReceiver.TransponderDataReady +=
				Raise.EventWith(_fakeTransponderReceiver, new RawTransponderDataEventArgs(list));

			//Assert
			var result = parameterList.First();

			Assert.That(result.Tag, Is.EqualTo(_tag));
		}

		[Test]
		public void OnTransponderDataReady_CorrectInput_XCoordinateParsedCorrectly()
		{
			//Arrange
			var list = new List<string>();
			list.Add("ABC123;5000;6000;6000;20151006213456789");

			List<TrackData> parameterList = new List<TrackData>();
			_fakeTrackController.AddTrackDataObjects(Arg.Do<List<TrackData>>(
				ls => parameterList = ls));

			//Act
			_fakeTransponderReceiver.TransponderDataReady +=
				Raise.EventWith(_fakeTransponderReceiver, new RawTransponderDataEventArgs(list));

			//Assert
			var result = parameterList.First();

			Assert.That(result.XCoordinate, Is.EqualTo(_xCoordinate));
		}

		[Test]
		public void OnTransponderDataReady_CorrectInput_YCoordinateParsedCorrectly()
		{
			//Arrange
			var list = new List<string>();
			list.Add("ABC123;5000;6000;6000;20151006213456789");

			List<TrackData> parameterList = new List<TrackData>();
			_fakeTrackController.AddTrackDataObjects(Arg.Do<List<TrackData>>(
				ls => parameterList = ls));

			//Act
			_fakeTransponderReceiver.TransponderDataReady +=
				Raise.EventWith(_fakeTransponderReceiver, new RawTransponderDataEventArgs(list));

			//Assert
			var result = parameterList.First();

			Assert.That(result.YCoordinate, Is.EqualTo(_yCoordinate));
		}

		[Test]
		public void OnTransponderDataReady_CorrectInput_AltitudeParsedCorrectly()
		{
			//Arrange
			var list = new List<string>();
			list.Add("ABC123;5000;6000;6000;20151006213456789");

			List<TrackData> parameterList = new List<TrackData>();
			_fakeTrackController.AddTrackDataObjects(Arg.Do<List<TrackData>>(
				ls => parameterList = ls));

			//Act
			_fakeTransponderReceiver.TransponderDataReady +=
				Raise.EventWith(_fakeTransponderReceiver, new RawTransponderDataEventArgs(list));

			//Assert
			var result = parameterList.First();

			Assert.That(result.Altitude, Is.EqualTo(_altitude));
		}

		[Test]
		public void OnTransponderDataReady_CorrectInput_TimestampParsedCorrectly()
		{
			//Arrange
			var list = new List<string>();
			list.Add("ABC123;5000;6000;6000;20151006213456789");

			List<TrackData> parameterList = new List<TrackData>();
			_fakeTrackController.AddTrackDataObjects(Arg.Do<List<TrackData>>(
				ls => parameterList = ls));

			//Act
			_fakeTransponderReceiver.TransponderDataReady +=
				Raise.EventWith(_fakeTransponderReceiver, new RawTransponderDataEventArgs(list));

			//Assert
			var result = parameterList.First();

			Assert.That(result.Timestamp, Is.EqualTo(_timestamp));
		}

		[Test]
		public void OnTransponderDataReady_CorrectInputLengthIsTwo_TwoObjectsCreated()
		{
			//Arrange
			var list = new List<string>();
			list.Add("ABC123;5000;6000;6000;20151006213456789");
			list.Add("CBA321;5000;6000;6000;20151006213456789");

			List<TrackData> parameterList = new List<TrackData>();
			_fakeTrackController.AddTrackDataObjects(Arg.Do<List<TrackData>>(
				ls => parameterList = ls));

			//Act
			_fakeTransponderReceiver.TransponderDataReady +=
				Raise.EventWith(_fakeTransponderReceiver, new RawTransponderDataEventArgs(list));

			//Assert
			_fakeTrackController.Received(1).AddTrackDataObjects(Arg.Is<List<TrackData>>(
				l => l.Count == 2
			));
		}

		[Test]
		public void OnTransponderDataReady_ListIsEmpty_TrackManagerReceivesEmptyList()
		{
			//Arrange
			var list = new List<string>();

			//Act
			_fakeTransponderReceiver.TransponderDataReady +=
				Raise.EventWith(_fakeTransponderReceiver, new RawTransponderDataEventArgs(list));

			//Assert
			_fakeTrackController.Received(1).AddTrackDataObjects(Arg.Is<List<TrackData>>(
				l => l.Count == 0
			));
		}
	}
}
