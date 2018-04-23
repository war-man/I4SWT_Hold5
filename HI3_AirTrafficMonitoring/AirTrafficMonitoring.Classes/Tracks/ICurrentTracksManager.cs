using AirTrafficMonitoring.Classes.DataModels;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.Tracks
{
	public interface ICurrentTracksManager
	{
		List<Track> CurrentTracks { get; }
		int TrackCount { get; }

		void AddTrack(Track track);
		Track FindTrack(string tag);
		void RemoveTrack(Track track);
		void UpdateTrack(TrackData trackData);
	}
}