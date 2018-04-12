using System;
using System.IO;

namespace AirTrafficMonitoring.Classes.Printer
{
	public class EventLogger : IPrinter
	{
		private string _filename;
		private string _completePath;

		public EventLogger()
		{
			_filename = "\\logfile.txt";
			Directory = Environment.CurrentDirectory;
			_completePath = Directory + _filename;
		}

		public static string Directory { get; set; }

		public string Filename
		{
			get => _filename;
			set
			{
				_filename = value;
				_completePath = Environment.CurrentDirectory + _filename;
			}
		}

		public void WriteLine(string logString)
		{
			if (!File.Exists(_completePath)) File.Create(_completePath);

			using (StreamWriter file = new StreamWriter(_completePath))
			{
				DateTime time = DateTime.Now;
				file.WriteLine($"{time:yyyy - MM - dd HH:ss} : {logString}");
			}
		}

		public void Clear()
		{
			File.WriteAllText(_completePath, string.Empty);
		}
	}
}
