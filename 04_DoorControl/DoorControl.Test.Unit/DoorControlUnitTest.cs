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

		//------------------------------------------------------------------
		// Test of Ctor
		[Test]
		public void Ctor_ForceOpenDoor_AlarmSignals()
		{
			_uut.DoorOpened();

			_fakeAlarm.Received().SignalAlarm();
		}

		//------------------------------------------------------------------
		// Test of RequestEntry()
		[Test]
		public void RequestEntry_DoorClosedStateAndValidationOK_OpenDoorIsCalled()
		{
			_fakeUserValidation.ValidateEntryRequest(Arg.Any<string>()).Returns(true);

			_uut.RequestEntry(Arg.Any<string>());

			_fakeDoor.Received().Open();
		}

		[Test]
		public void RequestEntry_DoorClosedStateAndValidationOK_NotifyEntryGrantedIsCalled()
		{
			_fakeUserValidation.ValidateEntryRequest(Arg.Any<string>()).Returns(true);

			_uut.RequestEntry("123");

			_fakEntryNotification.Received().NotifyEntryGranted();
		}

		[Test]
		public void RequestEntry_DoorClosedStateAndValidationNotOK_NotifyEntryDeniedIsCalled()
		{
			_fakeUserValidation.ValidateEntryRequest(Arg.Any<string>()).Returns(false);

			_uut.RequestEntry("123");

			_fakEntryNotification.Received().NotifyEntryDenied();
		}

		[Test]
		public void RequestEntry_DoorClosedStateAndValidationOK_NotifyEntryDeniedIsNotCalled()
		{
			_fakeUserValidation.ValidateEntryRequest(Arg.Any<string>()).Returns(true);

			_uut.RequestEntry("123");

			_fakEntryNotification.DidNotReceive().NotifyEntryDenied();
		}

		[Test]
		public void RequestEntry_DoorBreachedState_OpenDoorIsNotCalled()
		{
			// Bring system to breached state
			_uut.DoorOpened();

			_uut.RequestEntry("123");

			_fakeDoor.DidNotReceive().Open();
		}

		[Test]
		public void RequestEntry_DoorBreachedState_NotifyEntryDeniedIsNotCalled()
		{
			// Bring system to breached state
			_uut.DoorOpened();

			_uut.RequestEntry("123");

			_fakEntryNotification.DidNotReceive().NotifyEntryDenied();
		}

		//------------------------------------------------------------------
		// Test of DoorOpened()
		[Test]
		public void DoorOpened_DoorClosedState_SignalAlarmIsCalled()
		{
			_uut.DoorOpened();

			_fakeAlarm.Received().SignalAlarm();
		}

		[Test]
		public void DoorOpened_DoorClosedState_DoorCloseIsCalled()
		{
			_uut.DoorOpened();

			_fakeDoor.Received().Close();
		}

		[Test]
		public void DoorOpened_DoorOpeningState_DoorCloseIsCalled()
		{
			_fakeUserValidation.ValidateEntryRequest(Arg.Any<string>()).Returns(true);
			_uut.RequestEntry("123");

			_uut.DoorOpened();

			_fakeDoor.Received().Close();
		}

		[Test]
		public void DoorOpened_DoorOpeningState_SignalAlarmIsNotCalled()
		{
			_fakeUserValidation.ValidateEntryRequest(Arg.Any<string>()).Returns(true);
			_uut.RequestEntry("123");

			_uut.DoorOpened();

			_fakeAlarm.DidNotReceive().SignalAlarm();
		}

		// We should now test that given the system is in another state,
		// we should test that none of the above functions are called.
		// We chose not to do so.

		//------------------------------------------------------------------
		// Test of DoorClosed()
		[Test]
		public void DoorClosed_DoorClosingState_SystemIsNowInDoorCloosedState()
		{
			_fakeUserValidation.ValidateEntryRequest(Arg.Any<string>()).Returns(true);
			_uut.RequestEntry("123");
			_uut.DoorOpened();

			_uut.DoorClosed();

			_uut.RequestEntry("123");
			_fakeDoor.Received().Open();
		}
	}
}
