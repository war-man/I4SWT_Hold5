﻿using System;

namespace AirTrafficMonitoring.Classes.TrackDataModels
{
	public class TrackData
	{
		public string Tag { get; set; }
		public int XCoordinate { get; }
		public int YCoordinate { get; }
		public int Altitude { get; }
		public DateTime Timestamp { get; }

		public TrackData(string tag, int xCoordinate, int yCoordinate, int altitude, DateTime timestamp)
		{
			Tag = tag;
			XCoordinate = xCoordinate;
			YCoordinate = yCoordinate;
			Altitude = altitude;
			Timestamp = timestamp;
		}

		public override string ToString()
		{
			return $"Tag: {Tag}\n" +
				   $"X-Coordinate: {XCoordinate}\n" +
				   $"Y-Coordinate: {YCoordinate}\n" +
				   $"Altitude: {Altitude}\n" +
				   $"Timestamp: {Timestamp}";
		}
	}
}