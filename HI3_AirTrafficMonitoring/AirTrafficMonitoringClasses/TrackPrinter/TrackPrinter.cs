using AirTrafficMonitoring.Classes.TrackDataModels;
using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.TrackPrinter
{
	public class TrackPrinter : ITrackPrinter
	{
		public void Print(IEnumerable<Track> trackList)
		{
			Console.Clear();

			foreach (var track in trackList)
			{
				Console.WriteLine(track + "\n");
			}
		}
	}
}