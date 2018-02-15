using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace ECS.Legacy.Test.Unit
{
    [TestFixture]
    class TempUnitTest
    {
        private TempSensor _uut;

        [SetUp]
        public void Init()
        {
            _uut = new TempSensor();
        }

        [Test]
        public void GetTemp_ResultIs25()
        {
            Assert.That(_uut.GetTemp(), Is.EqualTo(25));
        }

        [Test]
        public void tempSensor_selfTest()
        {
            Assert.That(_uut.RunSelfTest, Is.EqualTo(true));
        }

    }
}
