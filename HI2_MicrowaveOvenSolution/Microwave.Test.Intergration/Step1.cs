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
	public class step1
	{
		private IOutput _outputDisplay;
		private IOutput _outputPower;

		private IDisplay _display;
		private ITimer _timer;
		private IPowerTube _powerTube;
		private ICookController _cookController;
		[SetUp]
		public void Setup()
		{
			_outputDisplay = Substitute.For<IOutput>();
			_outputPower = Substitute.For<IOutput>();
			_display = new Display(_outputDisplay);
			_timer = new Timer();
			_powerTube = new PowerTube(_outputPower);
			_cookController = new CookController(_timer, _display, _powerTube);

		}

		[Test]
		public void CookController_StartCooking_OutputPowerRecieved()
		{
			//Arrange
			int timesCalled = 0;
			//Act

			_outputPower.When(x=> x.OutputLine(Arg.Any<string>()))
				.Do(x => ++timesCalled);

			_cookController.StartCooking(50, 2000);
			
			//Assert
			Assert.That(() => (timesCalled == 2), Is.True.After(2500));
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
		

	}
}
