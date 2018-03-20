using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Intergration
{
	[TestFixture]
	class Step3
	{
		// Classes that have an interface which is under test
		private Door _door;
		private Button _powerButton;
		private Button _startCancelButton;
		private Button _timeButton;

		// Stubs
		private IOutput _fakeDisplayOutput;
		private IOutput _fakePowerTubeOutput;
		private IOutput _fakeLightOutput;

		// Classes that have been tested
		private Timer _timer;
		private Display _display;
		private PowerTube _powerTube;
		private Light _light;
		private CookController _cookController;
		private UserInterface _userInterface;

		[SetUp]
		public void Setup()
		{
			_door = new Door();
			_powerButton = new Button();
			_startCancelButton = new Button();
			_timeButton = new Button();

			_fakeDisplayOutput = Substitute.For<IOutput>();
			_fakePowerTubeOutput = Substitute.For<IOutput>();
			_fakeLightOutput = Substitute.For<IOutput>();

			_timer = new Timer();
			_display = new Display(_fakeDisplayOutput);
			_powerTube = new PowerTube(_fakePowerTubeOutput);
			_light = new Light(_fakeLightOutput);
			_cookController = new CookController(_timer, _display,
				_powerTube, _userInterface);
			_userInterface = new UserInterface(_powerButton,
				_timeButton, _startCancelButton, _door, _display,
				_light, _cookController);
		}

		[Test]
		public void Door_OpenDoor_CorrectOutputIsShown()
		{
			// Act
			_door.Open();

			// Assert
			_fakeLightOutput.Received().OutputLine(Arg.Is<string>(str =>
				str.ToLower().Contains("light") &&
				str.ToLower().Contains("turned on")));
		}

		[Test]
		public void Door_CloseDoorWhenDoorIsOpen_CorrectOutputIsShown()
		{
			// Arrange
			_door.Open();

			// Act
			_door.Close();

			// Assert
			_fakeLightOutput.Received().OutputLine(Arg.Is<string>(str =>
				str.ToLower().Contains("light") &&
				str.ToLower().Contains("turned off")));
		}

		[TestCase(1)]
		[TestCase(5)]
		[TestCase(10)]
		public void PowerButton_PressButton_CorrectOutputIsShown(int timesToPress)
		{
			// Act
			for (int i = 0; i < timesToPress; i++)
			{
				_powerButton.Press();
			}

			// Assert
			_fakeDisplayOutput.Received().OutputLine(Arg.Is<string>(str =>
				str.ToLower().Contains("display shows") &&
				str.ToLower().Contains($"{timesToPress * 50} w")));
		}

		[TestCase(1)]
		[TestCase(5)]
		[TestCase(10)]
		public void TimerButton_PressButton_CorrectOutputIsShown(int timesToPress)
		{
			// Arrange
			_powerButton.Press();

			// Act
			for (int i = 0; i < timesToPress; i++)
			{
				_timeButton.Press();
			}

			// Assert
			_fakeDisplayOutput.Received().OutputLine(Arg.Is<string>(str =>
				str.ToLower().Contains("display shows") &&
				str.ToLower().Contains($"{timesToPress:D2}:00")));
		}

		[Test]
		public void StartCancelButton_PressButon_CorrectOutputIsShownOnLight()
		{
			// Arrange
			_powerButton.Press();
			_timeButton.Press();

			// Act
			_startCancelButton.Press();

			// Assert
			_fakeLightOutput.Received().OutputLine(Arg.Is<string>(str =>
				str.ToLower().Contains("light") &&
				str.ToLower().Contains("turned on")));
		}

		[Test]
		public void StartCancelButton_PressButon_CorrectOutputIsShownOnPowertube()
		{
			// Arrange
			_powerButton.Press();
			_timeButton.Press();

			// Act
			_startCancelButton.Press();

			// Assert
			_fakePowerTubeOutput.Received().OutputLine(Arg.Is<string>(str =>
				str.ToLower().Contains("powertube works") &&
				str.ToLower().Contains("50 %")));
		}

		[Test]
		public void Door_OpenDoorWhenRunning_CorrectOutputIsShown()
		{
			// Arrange
			_door.Open();
			_door.Close();
			_powerButton.Press();
			_timeButton.Press();
			_startCancelButton.Press();

			// Act
			_door.Open();

			// Assert
			_fakePowerTubeOutput.Received().OutputLine(Arg.Is<string>(str =>
				str.ToLower().Contains("powertube") &&
				str.ToLower().Contains("turned off")));
		}

		[Test]
		public void StartCancelButton_PressButtonWhenRunning_CorrectOutputIsShown()
		{
			// Arrange
			_door.Open();
			_door.Close();
			_powerButton.Press();
			_timeButton.Press();
			_startCancelButton.Press();

			// Act
			_startCancelButton.Press();

			// Assert
			_fakePowerTubeOutput.Received().OutputLine(Arg.Is<string>(str =>
				str.ToLower().Contains("powertube") &&
				str.ToLower().Contains("turned off")));
		}
	}
}
