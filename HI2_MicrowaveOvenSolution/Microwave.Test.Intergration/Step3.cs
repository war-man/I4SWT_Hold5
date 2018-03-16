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
	    // Classes that are under test
	    private Button _powerButton;
	    private Button _startCancelButton;
	    private Button _timeButton;
		private Door _door;

	    // Stubs
	    private IOutput _fakeDisplayOutput;
	    private IOutput _fakePowerTubeOutput;
	    private IOutput _fakeLightOutput;

		// Classes that have been tested
	    private Timer _timer;
	    private Display _display;
	    private PowerTube _powerTube;
	    private Light _light;
		private UserInterface _userInterface;
	    private CookController _cookController;

		[SetUp]
        public void Setup()
		{
			_powerButton = new Button();
			_startCancelButton = new Button();
			_timeButton = new Button();
			_door = new Door();

			_fakeDisplayOutput = Substitute.For<IOutput>();
			_fakePowerTubeOutput = Substitute.For<IOutput>();
			_fakeLightOutput = Substitute.For<IOutput>();

			_timer = new Timer();
			_display = new Display(_fakeDisplayOutput);
			_powerTube = new PowerTube(_fakePowerTubeOutput);
			_light = new Light(_fakeLightOutput);
			_userInterface = new UserInterface(_powerButton,
				_timeButton, _startCancelButton, _door, _display,
				_light, _cookController);
			_cookController = new CookController(_timer, _display,
				_powerTube, _userInterface);
		}
    }
}
