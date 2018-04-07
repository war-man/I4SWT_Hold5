using AirTrafficMonitoring.Classes.TrackDataModels;
using AirTrafficMonitoring.Classes.TrackManager;
using System;
using System.Collections.Generic;
using TransponderReceiver;

namespace AirTrafficMonitoring.Classes
{
	public class TrackDataObjectifier
	{
		private readonly ITrackManager _trackManager;

		public TrackDataObjectifier(ITransponderReceiver transponderReceiver, ITrackManager trackManager)
		{
			transponderReceiver.TransponderDataReady += OnTransponderDataReady;
			_trackManager = trackManager;
		}

		private void OnTransponderDataReady(
			object sender, RawTransponderDataEventArgs rawTransponderDataEventArgs)
		{
			var trackDataList = new List<TrackData>();

			var transponderData = rawTransponderDataEventArgs.TransponderData;

			foreach (var data in transponderData)
			{
				var dataElements = data.Split(';');

				var tag = dataElements[0];
				var xCoordinate = int.Parse(dataElements[1]);
				var yCoordinate = int.Parse(dataElements[2]);
				var altitude = int.Parse(dataElements[3]);
				var year = int.Parse(dataElements[4].Substring(0, 4));
				var month = int.Parse(dataElements[4].Substring(4, 2));
				var day = int.Parse(dataElements[4].Substring(6, 2));
				var hour = int.Parse(dataElements[4].Substring(8, 2));
				var minute = int.Parse(dataElements[4].Substring(10, 2));
				var seconds = int.Parse(dataElements[4].Substring(12, 2));
				var milliseconds = int.Parse(dataElements[4].Substring(14, 3));
				var date = new DateTime(year, month, day, hour, minute, seconds, milliseconds);

				trackDataList.Add(new TrackData(tag, xCoordinate, yCoordinate, altitude, date));
			}

			_trackManager.AddTrackDataObjects(trackDataList);
		}
	}
}