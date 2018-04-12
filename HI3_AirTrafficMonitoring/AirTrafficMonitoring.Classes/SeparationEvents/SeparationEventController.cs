namespace AirTrafficMonitoring.Classes.SeparationEvents
{
	public class SeparationEventController : ISeparationEventController
	{
		private readonly ICurrentSeparationEventsManager _currentSeparationEventsManager;

		public SeparationEventController(ICurrentSeparationEventsManager currentSeparationEventsManager)
		{
			_currentSeparationEventsManager = currentSeparationEventsManager;
		}
	}
}