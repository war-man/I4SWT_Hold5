﻿using System;

namespace AirTrafficMonitoring.Classes.TrackDataModels
{
	public class Track
	{
		public Track(string tag, TrackData currentTrack)
		{
			Tag = tag;
			CurrentTrack = currentTrack;
		}

		public string Tag { get; }
		public double Direction { get; private set; }
		public double Velocity { get; private set; }
		public string CurrentPosition => CurrentTrack.XCoordinate + ";" + CurrentTrack.YCoordinate;

		public TrackData CurrentTrack { get; set; }
		public TrackData PreviousTrack { get; set; }

		public void AddNewTrackData(TrackData data)
		{
			PreviousTrack = CurrentTrack;
			CurrentTrack = data;

			// Calculate Delta values
			int deltaX = PreviousTrack.XCoordinate - CurrentTrack.XCoordinate;
			int deltaY = PreviousTrack.YCoordinate - CurrentTrack.YCoordinate;
			double deltaTime = (CurrentTrack.Timestamp - PreviousTrack.Timestamp).TotalSeconds;

			// Calculate Direction
			double radians = Math.Atan2(deltaY, deltaX);
			Direction = radians * (180 / Math.PI);

			// Calculate Velocity
			Velocity = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2)) / deltaTime;
		}

		public override string ToString()
		{
			return CurrentTrack.ToString();
		}
	}
}