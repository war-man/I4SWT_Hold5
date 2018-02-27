using NSubstitute;
using NUnit.Framework;

namespace DoorControl.Test.Unit
{
	[TestFixture]
	public class DoorControlUnitTest
	{
		private DoorControl _uut;
		private IAlarm _fakeAlarm;
		private IDoor _fakeDoor;
		private IEntryNotification _fakEntryNotification;
		private IUserValidation _fakeUserValidation;

		[SetUp]
		public void Init()
		{
			_fakeAlarm = Substitute.For<IAlarm>();
			_fakeDoor = Substitute.For<IDoor>();
			_fakEntryNotification = Substitute.For<IEntryNotification>();
			_fakeUserValidation = Substitute.For<IUserValidation>();

			_uut = new DoorControl(_fakeAlarm, _fakeDoor, _fakEntryNotification, _fakeUserValidation);
		}
	}
}
