using AirTrafficMonitoring.Classes.DataModels;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.Tracks
{
	public class CurrentTracksManager : ICurrentTracksManager
	{
		public CurrentTracksManager()
		{
			CurrentTracks = new List<Track>();
		}

		public List<Track> CurrentTracks { get; }

		public void AddTrack(Track track)
		{
			if (track != null)
				CurrentTracks.Add(track);
		}

		public Track FindTrack(string tag)
		{
			if (tag == null) return null;

			return CurrentTracks.Find(t => t.Tag == tag);
		}

		public int GetTrackCount()
		{
			return CurrentTracks.Count;
		}

		public void RemoveTrack(Track track)
		{
			if (track != null)
				CurrentTracks.Remove(track);
		}

		public void UpdateTrack(TrackData trackData)
		{
			if (trackData == null) return;

			var track = FindTrack(trackData.Tag);

			track?.AddNewTrackData(trackData);
		}
	}
}
