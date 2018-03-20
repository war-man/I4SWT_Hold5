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

	    private ConsoleOutput _consoleOutput;

		[SetUp]
        public void Setup()
        {
	        _consoleOutput = new ConsoleOutput();

			_powerButton = new Button();
	        _timerButton = new Button();
			_startButton = new Button();

	        _output = new Output();

			IDisplay display = new Display(_output);
	        var cookController = new CookController(new Timer(), display, new PowerTube(_output));

	        _userInterface = new UserInterface(_powerButton,_timerButton,_startButton, _door = new Door(), display, new Light(_output), cookController);

	        cookController.UI = _userInterface;

        }

	    [Test]
	    public void output_openDoor_OutputCorrect()
	    {
			//Arrange
			
			//Act
			_door.Open();
			
			//Assert
			Assert.IsTrue(_consoleOutput.GetOuput().Contains("Light") && _consoleOutput.GetOuput().Contains("turned on"));
		}
	    [Test]
	    public void output_closeDoor_OutputCorrect()
	    {
		    //Arrange
			_door.Open();

		    //Act
		    _door.Close();

			//Assert
		    Assert.IsTrue(_consoleOutput.GetOuput().Contains("Light") && _consoleOutput.GetOuput().Contains("turned off"));
	    }

	    [TestCase(1)]
	    [TestCase(5)]
		public void output_PowerbuttonPress_OutputCorrect(int timesToPress)
	    {
		    //Arrange
		   

			//Act
		    for (int i = 0; i < timesToPress; i++)
		    {
			    _powerButton.Press();
		    }

		    //Assert
			Assert.IsTrue(_consoleOutput.GetOuput().Contains("Display") && _consoleOutput.GetOuput().Contains( $"{timesToPress * 50} W"));
	    }

		[TestCase(1)]
		[TestCase(5)]
		public void output_timebuttonPress_inPowerState_OutputCorrect(int timesToPress)
	    {
		    //Arrange
			_powerButton.Press();
			//Act
			for (int i = 0; i < timesToPress; i++)
			{
				_timerButton.Press();
			}
			//Assert
			Assert.IsTrue(_consoleOutput.GetOuput().Contains("Display") && _consoleOutput.GetOuput().Contains($"{timesToPress:D2}:00"));
	    }

	    [TestCase(1)]
	    [TestCase(5)]
	    public void output_timebuttonPress_notInPowerstate_OutputCorrect(int timesToPress)
	    {
		    //Arrange
		    //Act
		    for (int i = 0; i < timesToPress; i++)
		    {
			    _timerButton.Press();
		    }
		    //Assert
		    Assert.IsFalse(_consoleOutput.GetOuput().Contains("Display") && _consoleOutput.GetOuput().Contains($"{timesToPress:D2}:00"));
	    }

	    [TestCase(1, 1)]
	    [TestCase(1, 2)]
	    [TestCase(1, 3)]
		public void output_CookingState_PowerOutput(int timesPressed, int powerPressed)
		{
			//Arrange
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
			output = _consoleOutput.GetOuput();

			double procent = (powerPressed*50.0 / 700.0) * 100.0;
			
			//Assert
			Assert.IsTrue(output.Contains("PowerTube") && output.Contains($"{(int)procent} %"));

		}

	    [TestCase(2, 1, 1)]
		[TestCase(2, 1, 10)]
		public void output_CookingState_TimeOutput(int timesPressed, int powerPressed, int testAfterTime)
	    {
			//Arrange
		    int timescalc = (timesPressed * 60) - testAfterTime;
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



			//Assert
			Assert.That( () =>
					//taknes all console output and splits into an array of strings. Reverse it so that the lastline is in top, keeps the "last two" and then check the last non empty line
					_consoleOutput.GetOuput().Split('\n').Reverse().Take(2).ToArray()[1].Contains("Display") &&
					_consoleOutput.GetOuput().Split('\n').Reverse().Take(2).ToArray()[1].Contains($"{(timescalc/60):D2}:{(timescalc % 60):D2}"),
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
