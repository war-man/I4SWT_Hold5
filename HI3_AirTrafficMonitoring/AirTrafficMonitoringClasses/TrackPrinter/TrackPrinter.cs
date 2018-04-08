﻿using AirTrafficMonitoring.Classes.TrackDataModels;
using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.TrackPrinter
{
	public class TrackPrinter : ITrackPrinter
	{
		public void Print(List<Track> trackList)
		{
			Console.Clear();

			Console.WriteLine($"Current track count: {trackList.Count}.\n");
			foreach (var track in trackList)
			{
				Console.WriteLine(track + "\n");
			}
		}
	}
}