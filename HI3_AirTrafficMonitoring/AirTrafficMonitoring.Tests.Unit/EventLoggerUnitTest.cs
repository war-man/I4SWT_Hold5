using AirTrafficMonitoring.Classes.Printer;
using NUnit.Framework;
using System;
using System.IO;

namespace AirTrafficMonitoring.Tests.Unit
{
	[TestFixture]
	class EventloggerUnitTest
	{
		private EventLogger _uut;
		private string _filename;
		private string _complete;
		private string _teststring;

		[SetUp]
		public void Init()
		{
			_filename = "\\testlogfile.txt";
			_complete = Environment.CurrentDirectory + _filename;
			_teststring = "testline";

			_uut = new EventLogger { Filename = _filename };
		}

		[Test]
		public void Clear_file_Empty()
		{
			//Act
			_uut.WriteLine(_teststring + "1");
			_uut.WriteLine(_teststring + "2");
			_uut.WriteLine(_teststring + "3");
			//Arrange
			_uut.Clear();
			string fileContent = System.IO.File.ReadAllText(_complete);

			//Assert
			Assert.AreEqual(fileContent, string.Empty);
		}
		[Test]
		public void Wrileline_datatime_corret()
		{
			//Arrange
			_uut.Clear();
			_uut.WriteLine(_teststring);
			DateTime time = DateTime.Now;

			//Act
			string fileContent = File.ReadAllText(_complete);

			string timestring = $"{time:yyyy - MM - dd HH: ss}";
			//Assert
			StringAssert.Contains(timestring, fileContent);
		}

		[Test]
		public void Writeline_string_correct()
		{
			//Arrange
			_uut.Clear();
			//Act
			_uut.WriteLine(_teststring);
			string fileContent = System.IO.File.ReadAllText(_complete);

			//Assert
			StringAssert.Contains(_teststring, fileContent);
		}

	}
}
