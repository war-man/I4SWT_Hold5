using AirTrafficMonitoring.Classes.DataModels;
using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.SeparationEvents
{
	public class SeparationEventController : ISeparationEventController
	{
		private readonly ICurrentSeparationEventsManager _currentSeparationEventsManager;
		private readonly ISeparationEventGenerator _separationEventGenerator;
		private readonly ISeparationEventListFormatter _separationEventListFormatter;

		private const int VerticalLimit = 300;
		private const int HorizontalLimit = 5000;

		public SeparationEventController(
			ICurrentSeparationEventsManager currentSeparationEventsManager,
			ISeparationEventGenerator separationEventGenerator,
			ISeparationEventListFormatter formatter)
		{
			_currentSeparationEventsManager = currentSeparationEventsManager;
			_separationEventGenerator = separationEventGenerator;
			_separationEventListFormatter = formatter;
		}

		public List<SeparationEvent> CheckForSeparationEvents(List<Track> trackList)
		{
			if (trackList == null) return null;

			var tempTrackList = trackList;

			foreach (var track in trackList)
			{
				foreach (var tempTrack in tempTrackList)
				{
					// Check for new separation events to be added
					if (_currentSeparationEventsManager.FindEvent(track.Tag, tempTrack.Tag) == null &&
						CheckForHorizontalConflict(track, tempTrack) &&
						CheckForVerticalConflict(track, tempTrack))
					{
						_currentSeparationEventsManager.AddEvent(
							_separationEventGenerator.GenerateSeparationEvent(track, tempTrack, DateTime.Now));
					}
					// Check for separation events that are no longer relevant
					else if (_currentSeparationEventsManager.FindEvent(track.Tag, tempTrack.Tag) != null &&
							 (!CheckForHorizontalConflict(track, tempTrack) ||
							  !CheckForVerticalConflict(track, tempTrack)))
					{
						_currentSeparationEventsManager.RemoveEvent(track.Tag, tempTrack.Tag);
					}
				}
			}

			return null;
		}

		public string GetFormattedSeparationEvents()
		{
			return _separationEventListFormatter.Format(_currentSeparationEventsManager.CurrentEvents);
		}

		private bool CheckForHorizontalConflict(Track track1, Track track2)
		{
			var deltaX = track1.CurrentTrack.XCoordinate - track2.CurrentTrack.XCoordinate;
			var deltaY = track1.CurrentTrack.YCoordinate - track2.CurrentTrack.YCoordinate;

			return Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2)) < HorizontalLimit;
		}

		private bool CheckForVerticalConflict(Track track1, Track track2)
		{
			var track1Altitude = track1.CurrentTrack.Altitude;
			var track2Altitude = track2.CurrentTrack.Altitude;

			bool conflict;
			if (track1Altitude > track2Altitude)
				conflict = track1Altitude - track2Altitude < VerticalLimit;
			else
				conflict = track2Altitude - track1Altitude < VerticalLimit;

			return conflict;
		}
	}
}