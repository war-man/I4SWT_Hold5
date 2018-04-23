using System;
using System.IO;

namespace AirTrafficMonitoring.Classes.Printer
{
	public class SeparationEventLogger : IPrinter
	{
		private string _filename;
		private string _completePath;

		public SeparationEventLogger()
		{
			_filename = "\\separationEvents.txt";
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

		public void WriteLine(string stringToPrint)
		{
			if (!File.Exists(_completePath))
				File.Create(_completePath).Close();

			using (var file = new StreamWriter(_completePath, true))
			{
				DateTime time = DateTime.Now;
				file.WriteLine($"{time} {stringToPrint}");
			}
		}

		public void Clear()
		{
			File.WriteAllText(_completePath, string.Empty);
		}
	}
}
