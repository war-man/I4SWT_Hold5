using System;

namespace AirTrafficMonitoring.Classes.DataModels
{
	public class Track
	{
		public Track(string tag, TrackData currentTrack)
		{
			Tag = tag;
			CurrentTrack = currentTrack;
			PreviousTrack = null;
			Direction = null;
			Velocity = null;
		}

		public string Tag { get; }
		public double? Direction { get; private set; }
		public double? Velocity { get; private set; }
		public TrackData CurrentTrack { get; private set; }
		public TrackData PreviousTrack { get; private set; }

		public void AddNewTrackData(TrackData data)
		{
			PreviousTrack = CurrentTrack;
			CurrentTrack = data;

			CalculateNewValues();
		}

		public override string ToString()
		{
			return $"Tag: {Tag} | " +
				   $"Coordinates (X,Y): {CurrentTrack.XCoordinate},{CurrentTrack.YCoordinate} | " +
				   $"Altitude: {CurrentTrack.Altitude} m\n" +
				   $"Velocity: {Velocity:F2} m/s | " +
				   $"Course: {Direction:F2} degrees | " +
				   $"Timestamp: {CurrentTrack.Timestamp}";
		}

		private void CalculateNewValues()
		{
			// Calculate Delta values
			int deltaX = CurrentTrack.XCoordinate - PreviousTrack.XCoordinate;
			int deltaY = CurrentTrack.YCoordinate - PreviousTrack.YCoordinate;

			double deltaTime = (CurrentTrack.Timestamp - PreviousTrack.Timestamp).TotalSeconds;

			// Calculate Direction
			double radians = Math.Atan2(deltaY, deltaX);
			Direction = radians * (180 / Math.PI);
			if (Direction < 0) Direction += 360;

			// Calculate Velocity
			Velocity = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2)) / deltaTime;
		}
	}
}
