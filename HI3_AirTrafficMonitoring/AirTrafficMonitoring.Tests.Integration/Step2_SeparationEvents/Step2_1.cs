using AirTrafficMonitoring.Classes.Printer;
using AirTrafficMonitoring.Classes.SeparationEvents;
using NUnit.Framework;

namespace AirTrafficMonitoring.Tests.Integration.Step2_SeparationEvents
{
	[TestFixture]
	class Step2_1
	{
		private ISeparationEventController _uut;

		[SetUp]
		public void Init()
		{
			_uut = new SeparationEventController(new CurrentSeparationEventsManager(), 
													new SeparationEventGenerator(), 
													new SeparationEventListFormatter(), 
													new SeparationEventLogger());
		}
	}
}
