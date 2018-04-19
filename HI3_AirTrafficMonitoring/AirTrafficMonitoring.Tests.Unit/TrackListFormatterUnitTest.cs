using AirTrafficMonitoring.Classes.DataModels;
using AirTrafficMonitoring.Classes.Tracks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirTrafficMonitoring.Tests.Unit
{
	[TestFixture]
	class TrackListFormatterUnitTest
	{
		private TrackListFormatter _uut;

		private Track _track1;
		private Track _track2;

		private List<Track> _trackList;

		[SetUp]
		public void Init()
		{
			_uut = new TrackListFormatter();

			_track1 = new Track("tag1", new TrackData("tag1", 0, 0, 0, DateTime.Now));
			_track2 = new Track("tag2", new TrackData("tag2", 0, 0, 0, DateTime.Now));

			_trackList = new List<Track>();
		}

		[Test]
		public void Format_EmptyList_OutputIsEmpty()
		{
			// Assert
			Assert.That(_uut.Format(_trackList), Is.EqualTo(""));
		}

		[Test]
		public void Format_OneElement_CountIsCorrect()
		{
			// Arrange
			_trackList.Add(_track1);

			// Assert
			StringAssert.Contains("track count: 1", _uut.Format(_trackList).ToLower());
		}

		[Test]
		public void Format_TwoElements_CountIsCorrect()
		{
			// Arrange
			_trackList.Add(_track1);
			_trackList.Add(_track2);

			// Assert
			StringAssert.Contains("track count: 2", _uut.Format(_trackList).ToLower());
		}

		[Test]
		public void Format_TwoElements_CountWrittenOnce()
		{
			// Arrange
			_trackList.Add(_track1);
			_trackList.Add(_track2);

			// Act
			var formattedString = _uut.Format(_trackList);
			var tagCount = formattedString.Select((c, i) => formattedString.Substring(i)).Count(sub => sub.StartsWith("track count:"));

			// Assert
			Assert.AreEqual(tagCount, 1);
		}

		[Test]
		public void Format_OneElement_OneElementPrinted()
		{
			// Arrange
			_trackList.Add(_track1);

			// Act
			var formattedString = _uut.Format(_trackList);
			var tagCount = formattedString.Select((c, i) => formattedString.Substring(i)).Count(sub => sub.StartsWith("tag"));

			// Assert
			Assert.AreEqual(tagCount, 1);
		}

		[Test]
		public void Format_TwoElements_TwoElementsPrinted()
		{
			// Arrange
			_trackList.Add(_track1);
			_trackList.Add(_track2);

			// Act
			var formattedString = _uut.Format(_trackList);
			var tagCount = formattedString.Select((c, i) => formattedString.Substring(i)).Count(sub => sub.StartsWith("tag"));

			// Assert
			Assert.AreEqual(tagCount, 2);
		}

		[TestCase("tag1")]
		[TestCase("tag2")]
		public void Format_TwoElements_BothTagsPrinted(string tag)
		{
			// Arrange
			_trackList.Add(_track1);
			_trackList.Add(_track2);

			// Assert
			StringAssert.Contains(tag.ToLower(), _uut.Format(_trackList));
		}

	}
}