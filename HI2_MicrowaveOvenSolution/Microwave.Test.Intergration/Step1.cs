using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microwave
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal.Commands;

namespace Microwave.Test.Intergration
{
    [TestFixture]
    public class step1
    {
        private IOutput _output;
        private IDisplay _display;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private ICookController _cookController;
        [SetUp]
        public void Setup()
        {
            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _cookController = new CookController(_timer,_display,_powerTube);

        }

        [Test]
        public void CookController_StartCooking_OutputRecieved()
        {
            //Arrange

            //Act
            _cookController.StartCooking(5, 5);
            //Assert
            _output.Received().OutputLine(Arg.Any<string>());
        }

    }
}
