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
		#region Setup
		private IOutput _output;
		private IButton _powerButton;
		private IButton _timeButton;
		private IButton _startButton;
		private IDoor _door;
		private IDisplay _display;
		private ICookController _cookController;
		private IUserInterface _userInterface;

		private ConsoleOutput _consoleOutput;

		[SetUp]
		public void Setup()
		{
			_consoleOutput = new ConsoleOutput();

			_powerButton = new Button();
			_timeButton = new Button();
			_startButton = new Button();
			_output = new Output();

			_display = new Display(_output);
			_cookController = new CookController(new Timer(), _display, new PowerTube(_output));

			_userInterface = new UserInterface(_powerButton, _timeButton, _startButton, _door = new Door(), _display, new Light(_output), _cookController);
			((CookController)(_cookController)).UI = _userInterface;

		}

		#endregion

		#region Display -> Output
		[Test]
		public void DisplayOutput_LogLine_OutputWritesDisplayToConsole()
		{
			//Arrange

			//Act
			_powerButton.Press();

			//Assert
			Assert.IsTrue(_consoleOutput.GetOuput().Contains($"Display"));
		}

		#endregion

		#region PowerTube -> Output
		[Test]
		public void PowerTubeOutput_LogLine_OutputWritesPowerTubeToConsole()
		{
			//Arrange
			_powerButton.Press();
			_timeButton.Press();

			//Act
			_startButton.Press();

			//Assert
			Assert.IsTrue(_consoleOutput.GetOuput().Contains($"PowerTube"));
		}

		#endregion

		#region Light -> Output
		[Test]
		public void LightOutput_LogLine_OutputWritesLightToConsole()
		{
			//Arrange

			//Act
			_door.Open();

			//Assert
			Assert.IsTrue(_consoleOutput.GetOuput().Contains($"Light"));
		}

		#endregion

		#region Utilities
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
		#endregion
	}
}
