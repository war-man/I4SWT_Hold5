using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.Objectifier;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Tests.Unit.Objectifier
{
	[TestFixture]
	class TrackDataObjectifierUnitTest
	{
		private TrackDataObjectifier _uut;

		private const string CorrectInput1 = "ABC123;5000;6000;6000;20151006213456789";
		private const string CorrectInput2 = "CBA321;5000;6000;6000;20151006213456789";


		private const string Tag1 = "ABC123";
		private const int XCoordinate = 5000;
		private const int YCoordinate = 6000;
		private const int Altitude = 6000;
		private readonly DateTime _timestamp = new DateTime(2015, 10, 6, 21, 34, 56, 789);

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
			list.Add(CorrectInput1);

			//Act
			List<TrackData> result = _uut.Objectify(list);

			//Assert
			Assert.That(result[0].Tag, Is.EqualTo(Tag1));
		}

		[Test]
		public void Objectify_CorrectInput_XCoordinateParsedCorrectly()
		{
			//Arrange
			var list = new List<string>();
			list.Add(CorrectInput1);

			//Act
			List<TrackData> result = _uut.Objectify(list);

			//Assert
			Assert.That(result[0].XCoordinate, Is.EqualTo(XCoordinate));
		}

		[Test]
		public void Objectify_CorrectInput_YCoordinateParsedCorrectly()
		{
			//Arrange
			var list = new List<string>();
			list.Add(CorrectInput1);

			//Act
			List<TrackData> result = _uut.Objectify(list);

			//Assert
			Assert.That(result[0].YCoordinate, Is.EqualTo(YCoordinate));
		}

		[Test]
		public void Objectify_CorrectInput_AltitudeParsedCorrectly()
		{
			//Arrange
			var list = new List<string>();
			list.Add(CorrectInput1);

			//Act
			List<TrackData> result = _uut.Objectify(list);

			//Assert
			Assert.That(result[0].Altitude, Is.EqualTo(Altitude));
		}

		[Test]
		public void Objectify_CorrectInput_TimestampParsedCorrectly()
		{
			//Arrange
			var list = new List<string>();
			list.Add(CorrectInput1);

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
			list.Add(CorrectInput1);
			list.Add(CorrectInput2);

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
