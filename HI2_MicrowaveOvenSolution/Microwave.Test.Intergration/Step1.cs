using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal.Commands;
using Assert = NUnit.Framework.Assert;

namespace Microwave.Test.Intergration
{
	[TestFixture]
	public class Step1
	{
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
			_userInterface = Substitute.For<UserInterface>();

			_display = new Display(_outputDisplay);
			_timer = new Timer();
			_powerTube = new PowerTube(_outputPower);
			_cookController = new CookController(_timer, _display, _powerTube);


		}

		[Test]
		public void CookController_StartCooking_OutputPowerRecieved_TurnOn()
		{
			//Arrange
			int power = 50;
			//double percent = (power / 700.0) * 100.0;
			int timesCalled = 0;
			//Act

			_outputPower.When(x=> x.OutputLine(Arg.Is<string>(s => s.Contains("PowerTube works"))))
				.Do(x => ++timesCalled);

			_cookController.StartCooking(power, 2);
			
			//Assert
			Assert.That(() => (timesCalled == 1), Is.True.After(1500));
		}

		[Test]
		public void CookController_StartCooking_OutputPowerRecieved_TurnOff_AndOnTimerExpired()
		{
			//Arrange
			int power = 50;
			int timeSek = 2;
			//double percent = (power / 700.0) * 100.0;
			int timesCalled = 0;
			//Act

			_outputPower.When(x => x.OutputLine(Arg.Is<string>(s => s.Contains("PowerTube turned off"))))
				.Do(x => ++timesCalled);

			_cookController.StartCooking(power, timeSek);

			//Assert
			Assert.That(() => (timesCalled == 1), Is.True.After(timeSek *1000 +500));
		}


		[Test]
		public void CookController_StartCooking_OutputDisplayRecievedFiveTimes()
		{
			//Arrange
			int timesCalled = 0;
			//Act
			_cookController.StartCooking(50, 5000);
			
			_outputDisplay.When(x => x.OutputLine(Arg.Any<string>()))
				.Do(x => ++timesCalled);

			//Assert
			Assert.That(() => (timesCalled == 5), Is.True.After(5500));


		}

	/*	[Test]
		public void CookController_CookingDone() //not done
		{

			//Arrange
			int power = 50;
			int timeSek = 2;
			//double percent = (power / 700.0) * 100.0;
			
			//Act
			
			_cookController.StartCooking(power, timeSek);

			//Assert
			
			Assert.That(_userInterface.Received().CookingIsDone(), Is.True.After(timeSek*1000+500));


		}*/


	}
}
