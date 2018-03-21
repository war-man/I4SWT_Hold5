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

		#region UI -> Light
		[Test]
		public void UserInterfaceLight_TurnLightOn_LightWritesOnToOutput()
		{
			//Arrange
			//State is already correct

			//Act
			_uutUserInterface.OnDoorOpened(_door, new EventArgs());

			//Assert
			_lightOutput.Received(1).OutputLine("Light is turned on");
		}

		[Test]
		public void UserInterfaceLight_TurnLightOff_LightWritesOffToOutput()
		{
			//Arrange
			_uutUserInterface.OnDoorOpened(_door, new EventArgs());

			//Act
			_uutUserInterface.OnDoorClosed(_door, new EventArgs());

			//Assert
			_lightOutput.Received().OutputLine("Light is turned off");
		}

		#endregion

		#region UI -> Display
		[Test]
		public void UserInterfaceDisplay_ClearDisplay_DisplayWritesClearedToOutput()
		{
			//Arrange
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			//Act
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Assert
			_displayOutput.Received().OutputLine("Display cleared");
		}

		[Test]
		public void UserInterfaceDisplay_ShowPower_DisplayWritesPowerToOutput()
		{
			//Arrange
			//State is already correct

			//Act
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());

			//Assert
			_displayOutput.Received(1).OutputLine("Display shows: 50 W");
		}

		[Test]
		public void UserInterfaceDisplay_ShowTime_DisplayWritesTimeToOutput()
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

		#region UI -> CookController
		[Test]
		public void UserInterfaceCookController_StartCooking_PowerTubeWritesOnToOutput()
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
		public void UserInterfaceCookController_StartCooking_DisplayWritesTimeToOutput()
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
		public void UserInterfaceCookController_Stop_PowerTubeWritesOffToOutput()
		{
			//Arrange
			//Get to state
			_uutUserInterface.OnPowerPressed(_powerButton, new EventArgs());
			_uutUserInterface.OnTimePressed(_timeButton, new EventArgs());
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Act
			_uutUserInterface.OnStartCancelPressed(_startCancelButton, new EventArgs());

			//Assert
			_powerTubeOutput.Received().OutputLine("PowerTube turned off");
		}

		#endregion

		#region CookController -> UI
		[Test]
		public void CookControllerUserInterface_CookingIsDone_DisplayWritesClearedToOutput()
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

		#endregion

	}
}
