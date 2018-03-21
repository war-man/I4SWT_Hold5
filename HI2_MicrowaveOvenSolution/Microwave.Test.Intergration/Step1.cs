using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal.Commands;
using Assert = NUnit.Framework.Assert;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Intergration
{
	[TestFixture]
	public class Step1
	{
		#region Setup
		private IOutput _outputDisplay;
		private IOutput _outputPower;

		private IDisplay _display;
		private ITimer _timer;
		private IPowerTube _powerTube;
		private ICookController _cookController;
		private IUserInterface _userInterface;

		[SetUp]
		public void Setup()
		{
			_outputDisplay = Substitute.For<IOutput>();
			_outputPower = Substitute.For<IOutput>();
			_userInterface = Substitute.For<IUserInterface>();

			_display = new Display(_outputDisplay);
			_timer = new Timer();
			_powerTube = new PowerTube(_outputPower);
			_cookController = new CookController(_timer, _display, _powerTube);
		}

		#endregion

		#region CookController -> Display
		[Test]
		public void CookControllerDisplay_ShowTime_DisplayWritesTimeToOutput()
		{
			//Arrange
			_cookController.StartCooking(50, 65);

			//Act
			((CookController)_cookController).OnTimerTick(_timer, null);

			//Assert
			_outputDisplay.Received().OutputLine($"Display shows: {65 / 60:D2}:{65 % 60:D2}");
		}

		#endregion

		#region CookController -> PowerTube
		[Test]
		public void CookControllerPowerTube_TurnOn_PowerTubeWritesOnToConsole()
		{
			//Arrange

			//Act
			_cookController.StartCooking(50, 65);

			//Assert
			_outputPower.Received().OutputLine($"PowerTube works with {(int)(50 / 700.0 * 100)} %");
		}

		[Test]
		public void CookControllerPowerTube_TurnOff_PowerTubeWritesOffToConsole()
		{
			//Arrange
			_cookController.StartCooking(50, 65);

			//Act
			_cookController.Stop();

			//Assert
			_outputPower.Received(1).OutputLine($"PowerTube turned off");
		}

		#endregion

		#region CookController <-> Timer
		[Test]
		public void CookControllerTimer_StartAndOnTimerTick_DisplayWritesTimeToOutput()
		{
			//Arrange

			//Act
			_cookController.StartCooking(50, 65);
			Thread.Sleep(1100);

			//Assert
			_outputDisplay.Received(1).OutputLine($"Display shows: {64 / 60:D2}:{64 % 60:D2}");
		}

		[Test]
		public void CookControllerTimer_OnTimerExpired_DisplayDoesNotWriteTimeToOutput()
		{
			//Arrange

			//Act
			_cookController.StartCooking(50, 65);
			_cookController.Stop();

			//Assert
			_outputDisplay.DidNotReceive().OutputLine($"Display shows: {64 / 60:D2}:{64 % 60:D2}");
		}

		#endregion
	}
}
