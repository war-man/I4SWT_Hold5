using AirTrafficMonitoring.Classes.DataModels;
using System.Collections.Generic;
using System.Text;

namespace AirTrafficMonitoring.Classes.Tracks
{
	public class TrackListFormatter : ITrackListFormatter
	{
		public string Format(List<Track> trackList)
		{
			if (trackList == null || trackList.Count == 0) return "";

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
