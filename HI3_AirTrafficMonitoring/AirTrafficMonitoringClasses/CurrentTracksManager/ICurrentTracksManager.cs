using AirTrafficMonitoring.Classes.TrackDataModels;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.CurrentTracksManager
{
	public interface ICurrentTracksManager
	{
		List<Track> CurrentTracks { get; }

		void AddTrack(Track track);
		Track FindTrack(string tag);
		int GetTrackCount();
		void RemoveTrack(Track track);
		void UpdateTrack(TrackData trackData);
	}
}