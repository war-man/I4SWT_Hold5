using AirTrafficMonitoring.Classes.DataModels;
using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.Objectifier
{
	public class TrackDataObjectifier : ITrackDataObjectifier
	{
		public List<TrackData> Objectify(List<string> transponderData)
		{
			var trackDataList = new List<TrackData>();

			foreach (var data in transponderData)
			{
				var dataElements = data.Split(';');

				var tag = dataElements[0];
				var xCoordinate = int.Parse(dataElements[1]);
				var yCoordinate = int.Parse(dataElements[2]);
				var altitude = int.Parse(dataElements[3]);
				var timestamp = DateTime.ParseExact(dataElements[4], "yyyyMMddHHmmssfff",
					System.Globalization.CultureInfo.InvariantCulture);

				trackDataList.Add(new TrackData(tag, xCoordinate, yCoordinate, altitude, timestamp));
			}

			return trackDataList;
		}
	}
}