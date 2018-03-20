using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Microwave;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Intergration
{
	[TestFixture]
	class Step2
	{
		#region Setup
		// Drivers
		private IButton _powerButton;
		private IButton _timeButton;
		private IButton _startCancelButton;
		private IDoor _door;

		// Stubs
		private IOutput _displayOutput;
		private IOutput _powerTubeOutput;
		private IOutput _lightOutput;

		// Real classes
		private IDisplay _display;
		private IPowerTube _powerTube;
		private ILight _light;
		private ITimer _timer;

		// Units Under Test
		private CookController _uutCookController;
		private UserInterface _uutUserInterface;


		[SetUp]
		public void Setup()
		{
			// Drivers
			_powerButton = Substitute.For<IButton>();
			_timeButton = Substitute.For<IButton>();
			_startCancelButton = Substitute.For<IButton>();
			_door = Substitute.For<IDoor>();

			// Stubs
			_displayOutput = Substitute.For<IOutput>();
			_powerTubeOutput = Substitute.For<IOutput>();
			_lightOutput = Substitute.For<IOutput>();

			// Real classes
			_display = new Display(_displayOutput);
			_powerTube = new PowerTube(_powerTubeOutput);
			_light = new Light(_lightOutput);
			_timer = new Timer();

			// Units Under Test
			_uutCookController = new CookController(_timer, _display, _powerTube);
			_uutUserInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _uutCookController);
			_uutCookController.UI = _uutUserInterface;
		}
		#endregion

		#region ReadyState
		[Test]
		public void UserInterface_ReadyState_OpenDoor_LightWritesOnToLog()
		{
			//Arrange
			//State is already correct

			//Act
			_uutUserInterface.OnDoorOpened(_door, new EventArgs());

			//Assert
			_lightOutput.Received().OutputLine("Light is turned on");
		}

		[Test]
		public void UserInterface_ReadyState_PressPowerButton_DisplayWritesPowerToLog()
		{
			//Arrange
			//State is already correct

			//Act
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			//Assert
			_displayOutput.Received().OutputLine("Display shows: 50 W");
		}
		#endregion

		#region DoorIsOpenState
		[Test]
		public void UserInterface_DoorIsOpenState_CloseDoor_LightWritesOffToLog()
		{
			//Arrange
			_uutUserInterface.OnDoorOpened(_door, new EventArgs());

			//Act
			_uutUserInterface.OnDoorClosed(_door, new EventArgs());

			//Assert
			_lightOutput.Received().OutputLine("Light is turned off");
		}

		#endregion

		#region SetPowerState
		[Test]
		public void UserInterface_SetPowerState_PressPowerButton_DisplayWritesPowerToLog()
		{
			//Arrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			//Act
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			//Assert
			_displayOutput.Received().OutputLine("Display shows: 100 W");
		}

		[Test]
		public void UserInterface_SetPowerState_PressPowerButtonTwice_DisplayWritesPowerToLog()
		{
			//Arrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			//Act
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			//Assert
			_displayOutput.Received().OutputLine("Display shows: 150 W");
		}

		[Test]
		public void UserInterface_SetPowerState_PressStartCancelButton_DisplayWritesClearedToLog()
		{
			//Arrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			//Act
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Assert
			_displayOutput.Received().OutputLine("Display cleared");
		}

		[Test]
		public void UserInterface_SetPowerState_PressStartCancelButton_ValuesAreReset()
		{
			//Arrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			//Act
			//Increase the power level a bit
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Rearrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			//Assert
			_displayOutput.Received(2).OutputLine("Display shows: 50 W");
		}

		[Test]
		public void UserInterface_SetPowerState_DoorOpened_DisplayWritesClearedToLog()
		{
			//Arrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			//Act
			_uutUserInterface.OnDoorOpened(_door, new EventArgs());

			//Assert
			_displayOutput.Received().OutputLine("Display cleared");
		}

		[Test]
		public void UserInterface_SetPowerState_DoorOpened_LightWritesOnToLog()
		{
			//Arrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			//Act
			_uutUserInterface.OnDoorOpened(_door, new EventArgs());

			//Assert
			_lightOutput.Received().OutputLine("Light is turned on");
		}

		[Test]
		public void UserInterface_SetPowerState_DoorOpened_ValuesAreReset()
		{
			//Arrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			//Act
			//Increase the power level a bit
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			_uutUserInterface.OnDoorOpened(_door, new EventArgs());

			//Rearrange
			//Get to state
			_uutUserInterface.OnDoorClosed(_door, new EventArgs());
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			//Assert
			_displayOutput.Received(2).OutputLine("Display shows: 50 W");
		}

		[Test]
		public void UserInterface_SetPowerState_PressTimeButton_DisplayWritesTimeToLog()
		{
			//Arrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			//Act
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());

			//Assert
			_displayOutput.Received().OutputLine("Display shows: 01:00");
		}

		#endregion

		#region SetTimeState
		[Test]
		public void UserInterface_SetTimeState_PressTimeButton_DisplayWritesTimeToLog()
		{
			//Arrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());

			//Act
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());

			//Assert
			_displayOutput.Received().OutputLine("Display shows: 01:00");
		}

		[Test]
		public void UserInterface_SetTimeState_PressTimeButtonTwice_DisplayWritesTimeToLog()
		{
			//Arrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());

			//Act
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());

			//Assert
			_displayOutput.Received().OutputLine("Display shows: 03:00");
		}

		[Test]
		public void UserInterface_SetTimeState_DoorOpened_DisplayWritesClearedToLog()
		{
			//Arrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());

			//Act
			_uutUserInterface.OnDoorOpened(_door, new EventArgs());

			//Assert
			_displayOutput.Received().OutputLine("Display cleared");
		}

		[Test]
		public void UserInterface_SetTimeState_DoorOpened_LightWritesOnToLog()
		{
			//Arrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());

			//Act
			_uutUserInterface.OnDoorOpened(_door, new EventArgs());

			//Assert
			_lightOutput.Received().OutputLine("Light is turned on");
		}

		[Test]
		public void UserInterface_SetTimeState_DoorOpened_ValuesAreReset()
		{
			//Arrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());
			//Increase the power level a bit
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());

			//Act
			_uutUserInterface.OnDoorOpened(_door, new EventArgs());

			//Rearrange
			//Get to state
			_uutUserInterface.OnDoorClosed(_door, new EventArgs());
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());

			//Assert
			_displayOutput.Received(2).OutputLine("Display shows: 01:00");
		}

		[Test]
		public void UserInterface_SetTimeState_StartCancelButtonPressed_PowerTubeWritesOnToLog()
		{
			//Arrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());

			//Act
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Assert
			_powerTubeOutput.Received().OutputLine($"PowerTube works with {(int)(50 / 700.0 * 100.0)} %");
		}

		[Test]
		public void UserInterface_SetTimeState_StartCancelButtonPressedHigherPower_PowerTubeWritesOnToLog()
		{
			//Arrange
			//Get to state & increase power
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());

			//Act
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Assert
			_powerTubeOutput.Received().OutputLine($"PowerTube works with {(int)(150/700.0*100.0)} %");
		}

		#endregion

		#region CookingState
		[Test]
		public void UserInterface_CookingState_StartCancelButtonPressed_PowerTubeWritesOffToLog()
		{
			//Arrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Act
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Assert
			_powerTubeOutput.Received().OutputLine($"PowerTube turned off");
		}

		[Test]
		public void UserInterface_CookingState_StartCancelButtonPressed_DisplayWritesClearedToLog()
		{
			//Arrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Act
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Assert
			_displayOutput.Received().OutputLine("Display cleared");
		}

		[Test]
		public void UserInterface_CookingState_StartCancelButtonPressed_ValuesAreReset()
		{
			//Arrange
			//Get to state and increase power a bit
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Act
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			//Assert
			_displayOutput.Received(2).OutputLine("Display shows: 50 W");
		}

		[Test]
		public void UserInterface_CookingState_DoorOpened_PowerTubeWritesOffToLog()
		{
			//Arrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Act
			_uutUserInterface.OnDoorOpened(_door, new EventArgs());

			//Assert
			_powerTubeOutput.Received().OutputLine($"PowerTube turned off");
		}

		[Test]
		public void UserInterface_CookingState_DoorOpened_DisplayWritesClearedToLog()
		{
			//Arrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Act
			_uutUserInterface.OnDoorOpened(_door, new EventArgs());

			//Assert
			_displayOutput.Received().OutputLine("Display cleared");
		}

		[Test]
		public void UserInterface_CookingState_DoorOpened_ValuesAreReset()
		{
			//Arrange
			//Get to state and increase power a bit
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Act
			_uutUserInterface.OnDoorOpened(_door, new EventArgs());

			//Rearrange
			_uutUserInterface.OnDoorClosed(_door, new EventArgs());
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			//Assert
			_displayOutput.Received(2).OutputLine("Display shows: 50 W");
		}

		[Test]
		public void UserInterface_CookingState_DoorOpened_LightWritesOnToLog()
		{
			//Arrange
			//Get to state and increase power a bit
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Act
			_uutUserInterface.OnDoorOpened(_door, new EventArgs());

			//Rearrange
			_uutUserInterface.OnDoorClosed(_door, new EventArgs());

			//Assert
			_lightOutput.Received(1).OutputLine("Light is turned on");
		}

		[Test]
		public void UserInterface_CookingState_TimerTickOneSec_DisplayWritesTimeToLog()
		{
			//Arrange
			bool wasCalled = false;
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());

			//Act
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Assert
			//Set wasCalled, if OutputLine() was called with 00:59 time
			_displayOutput
				.When(x => x.OutputLine(Arg.Is<string>(
					str => str.ToLower().Contains("display shows: 00:59")
				)))
				.Do(x => wasCalled = true);

			Assert.That(() => (wasCalled), Is.True.After(1100));
		}

		[Test]
		public void UserInterface_CookingState_TimerTickFiveSec_DisplayWritesTimeToLog()
		{
			//Arrange
			bool wasCalled = false;
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());

			//Act
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Assert
			//Set wasCalled, if OutputLine() was called with 00:55 time
			_displayOutput
				.When(x => x.OutputLine(Arg.Is<string>(
					str => str.ToLower().Contains("display shows: 00:55")
				)))
				.Do(x => wasCalled = true);

			Assert.That(() => (wasCalled), Is.True.After(5100));
		}

		[Test]
		public void UserInterface_CookingState_CookingIsDone_DisplayWritesClearedToLog()
		{
			//Arrange
			bool wasCalled = false;
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());

			//Act
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Assert
			//Set wasCalled, if OutputLine() was called with display cleared
			_displayOutput
				.When(x => x.OutputLine(Arg.Is<string>(
					str => str.ToLower().Contains("display cleared")
				)))
				.Do(x => wasCalled = true);

			Assert.That(() => (wasCalled), Is.True.After(61000));
		}

		[Test]
		public void UserInterface_CookingState_CookingIsDone_ValuesAreReset()
		{
			//Arrange
			//Get to state and increase power a bit
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Act
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());
			//Wait for timer to expire
			Thread.Sleep(61000);

			//Rearrange
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			//Assert
			_displayOutput.Received(2).OutputLine("Display shows: 50 W");
		}

		#endregion

	}
}
