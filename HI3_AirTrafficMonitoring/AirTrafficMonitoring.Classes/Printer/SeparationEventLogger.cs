using System;
using System.IO;

namespace AirTrafficMonitoring.Classes.Printer
{
	public class SeparationEventLogger : IPrinter
	{
		private string _fileName;
		private string _completePath;

		public SeparationEventLogger()
		{
			_fileName = "\\separationEvents.txt";
			Directory = Environment.CurrentDirectory;
			_completePath = Directory + _fileName;
		}

		public static string Directory { get; set; }

		public string FileName
		{
			get => _fileName;
			set
			{
				_fileName = value;
				_completePath = Environment.CurrentDirectory + _fileName;
			}
		}

		public void WriteLine(string line)
		{
			if (!File.Exists(_completePath))
				File.Create(_completePath).Close();

			using (var file = new StreamWriter(_completePath, true))
			{
				DateTime time = DateTime.Now;
				file.WriteLine($"{time} {line}");
			}
		}

		public void Clear()
		{
			File.WriteAllText(_completePath, string.Empty);
		}
	}
}
