using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.TrackDataModels;

namespace AirTrafficMonitoring.Classes.Printer
{
	public class TrackListFormatter : ITrackListFormatter
	{
		public string Format(List<Track> trackList)
		{
			if (trackList.Count == 0) return "";

			var returnString = new StringBuilder();

			returnString.AppendLine($"Current track count: {trackList.Count}.\n");
			foreach (var track in trackList)
			{
				returnString.AppendLine(track + "\n");
			}

			return returnString.ToString();
		}
	}
}
