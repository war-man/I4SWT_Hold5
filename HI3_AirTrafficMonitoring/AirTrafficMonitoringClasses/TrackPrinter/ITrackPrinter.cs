﻿using AirTrafficMonitoring.Classes.TrackDataModels;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.TrackPrinter
{
	public interface ITrackPrinter
	{
		void Print(List<Track> trackList);
	}
}