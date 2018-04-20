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

		private string _correctInput1 = "ABC123;5000;6000;6000;20151006213456789";
		private string _correctInput2 = "CBA321;5000;6000;6000;20151006213456789";


		private string _tag1 = "ABC123";
		private string _tag2 = "CBA321";
		private int _xCoordinate = 5000;
		private int _yCoordinate = 6000;
		private int _altitude = 6000;
		private DateTime _timestamp = new DateTime(2015, 10, 6, 21, 34, 56, 789);

		[SetUp]
		public void Init()
		{
			_uut = new TrackDataObjectifier();
		}

		[Test]
		public void Objectify_CorrectInput_TagParsedCorrectly()
		{
			//Arrange
			var list = new List<string>();
			list.Add(_correctInput1);

			//Act
			List<TrackData> result = _uut.Objectify(list);

			//Assert
			Assert.That(result[0].Tag, Is.EqualTo(_tag1));
		}

		[Test]
		public void Objectify_CorrectInput_XCoordinateParsedCorrectly()
		{
			//Arrange
			var list = new List<string>();
			list.Add(_correctInput1);

			//Act
			List<TrackData> result = _uut.Objectify(list);

			//Assert
			Assert.That(result[0].XCoordinate, Is.EqualTo(_xCoordinate));
		}

		[Test]
		public void Objectify_CorrectInput_YCoordinateParsedCorrectly()
		{
			//Arrange
			var list = new List<string>();
			list.Add(_correctInput1);

			//Act
			List<TrackData> result = _uut.Objectify(list);

			//Assert
			Assert.That(result[0].YCoordinate, Is.EqualTo(_yCoordinate));
		}

		[Test]
		public void Objectify_CorrectInput_AltitudeParsedCorrectly()
		{
			//Arrange
			var list = new List<string>();
			list.Add(_correctInput1);

			//Act
			List<TrackData> result = _uut.Objectify(list);

			//Assert
			Assert.That(result[0].Altitude, Is.EqualTo(_altitude));
		}

		[Test]
		public void Objectify_CorrectInput_TimestampParsedCorrectly()
		{
			//Arrange
			var list = new List<string>();
			list.Add(_correctInput1);

			//Act
			List<TrackData> result = _uut.Objectify(list);

			//Assert
			Assert.That(result[0].Timestamp, Is.EqualTo(_timestamp));
		}

		[Test]
		public void Objectify_CorrectInputCountIsTwo_TwoObjectsCreated()
		{
			//Arrange
			var list = new List<string>();
			list.Add(_correctInput1);
			list.Add(_correctInput2);

			//Act
			List<TrackData> result = _uut.Objectify(list);

			//Assert
			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void Objectify_ListIsEmpty_EmptyTrackListReturned()
		{
			//Arrange
			var list = new List<string>();

			//Act
			List<TrackData> result = _uut.Objectify(list);

			//Assert
			Assert.That(result.Count, Is.EqualTo(0));
		}
	}
}
