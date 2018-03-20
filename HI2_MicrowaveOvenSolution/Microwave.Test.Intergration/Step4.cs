using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Intergration
{
    [TestFixture]
    class Step4
    {
	    private IOutput _output;
	    

	    private IButton _powerButton;
	    private IButton _timerButton;
	    private IButton _startButton;

	    private IDoor _door;

	    private IUserInterface _userInterface;

		[SetUp]
        public void Setup()
        {
			_powerButton = new Button();
	        _timerButton = new Button();
			_startButton = new Button();

	        _output = new Output();

			IDisplay display = new Display(_output);
			_userInterface = new UserInterface(_powerButton,_timerButton,_startButton, _door = new Door(), display, new Light(_output), new CookController(new Timer(), display, new PowerTube(_output)));
        }

	    [Test]
	    public void output_openDoor_OutputCorrect()
	    {
			//Arrange
		    var consoleOutput = new ConsoleOutput();
			
			//Act
			_door.Open();
			
			//Assert
			Assert.IsTrue(consoleOutput.GetOuput().Contains("Light") && consoleOutput.GetOuput().Contains("turned on"));
		}
	    [Test]
	    public void output_closeDoor_OutputCorrect()
	    {
		    //Arrange
		    var consoleOutput = new ConsoleOutput();
			_door.Open();

		    //Act
		    _door.Close();

			//Assert
		    Assert.IsTrue(consoleOutput.GetOuput().Contains("Light") && consoleOutput.GetOuput().Contains("turned off"));
	    }

	    [TestCase(1)]
	    [TestCase(5)]
		public void output_PowerbuttonPress_OutputCorrect(int timesToPress)
	    {
		    //Arrange
		    var consoleOutput = new ConsoleOutput();

			//Act
		    for (int i = 0; i < timesToPress; i++)
		    {
			    _powerButton.Press();
		    }

		    //Assert
			Assert.IsTrue(consoleOutput.GetOuput().Contains("Display") && consoleOutput.GetOuput().Contains( $"{timesToPress * 50} W"));
	    }

		[TestCase(1)]
		[TestCase(5)]
		public void output_timebuttonPress_inPowerState_OutputCorrect(int timesToPress)
	    {
		    //Arrange
		    var consoleOutput = new ConsoleOutput();
			_powerButton.Press();
			//Act
			for (int i = 0; i < timesToPress; i++)
			{
				_timerButton.Press();
			}
			//Assert
			Assert.IsTrue(consoleOutput.GetOuput().Contains("Display") && consoleOutput.GetOuput().Contains($"{timesToPress:D2}:00"));
	    }

	    [TestCase(1)]
	    [TestCase(5)]
	    public void output_timebuttonPress_notInPowerstate_OutputCorrect(int timesToPress)
	    {
		    //Arrange
		    var consoleOutput = new ConsoleOutput();
		    //Act
		    for (int i = 0; i < timesToPress; i++)
		    {
			    _timerButton.Press();
		    }
		    //Assert
		    Assert.IsFalse(consoleOutput.GetOuput().Contains("Display") && consoleOutput.GetOuput().Contains($"{timesToPress:D2}:00"));
	    }

	    [TestCase(1, 1)]
	    [TestCase(1, 2)]
	    [TestCase(1, 3)]
		public void output_CookingState_PowerOutput(int timesPressed, int powerPressed)
		{
			//Arrange
			var consoleOutput = new ConsoleOutput();
			string output = null;
			for (int i = 0; i < powerPressed; i++)
			{
				_powerButton.Press();
			}
			for (int i = 0; i < timesPressed; i++)
			{
				_timerButton.Press();
			}
			
			//Act
			_startButton.Press();
			output = consoleOutput.GetOuput();

			double procent = (powerPressed*50.0 / 700.0) * 100.0;
			
			//Assert
			Assert.IsTrue(output.Contains("PowerTube") && output.Contains($"{(int)procent} %"));

		}

	    [TestCase(2, 1, 10)]
	    public void output_CookingState_TimeOutput(int timesPressed, int powerPressed, int testAfterTime)
	    {
		    //Arrange
		    var consoleOutput = new ConsoleOutput();
		    string output = String.Empty;

		    for (int i = 0; i < powerPressed; i++)
		    {
			    _powerButton.Press();
			   
		    }
		    for (int i = 0; i < timesPressed; i++)
		    {
			    _timerButton.Press();
			   
		    }
			//Act
			_startButton.Press();
		    output = consoleOutput.GetOuput();

			//Assert
			Assert.That(
						output.Contains("Display") && 
						output.Contains($"{timesPressed - 1:D2}:{60 - testAfterTime:D2}"),
						Is.True.After(testAfterTime * 1000  + 500));

		}

		//helper class
		public class ConsoleOutput : IDisposable
	    {
			//From https://stackoverflow.com/questions/2139274/grabbing-the-output-sent-to-console-out-from-within-a-unit-test

		    private StringWriter stringWriter;
		    private TextWriter originalOutput;

		    public ConsoleOutput()
		    {
			    stringWriter = new StringWriter();
			    originalOutput = Console.Out;
			    Console.SetOut(stringWriter);
		    }

		    public string GetOuput()
		    {
			    return stringWriter.ToString();
		    }

		    public void Dispose()
		    {
			    Console.SetOut(originalOutput);
			    stringWriter.Dispose();
		    }
	    }
	}
}
