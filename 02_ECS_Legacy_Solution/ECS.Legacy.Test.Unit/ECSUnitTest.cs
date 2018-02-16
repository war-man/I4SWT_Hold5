using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ECS.Legacy.Test.Unit
{
    public class ECSUnitTest
    {
	    private ECS _uut;
	    private FakeTempSensor _temp;
	    private FakeHeater _heater;

	    [SetUp]
	    public void Init()
	    {
			_temp = new FakeTempSensor();
			_heater = new FakeHeater();

		    _uut = new ECS(1, _temp, _heater);
	    }

		[TestCase(0)]
		[TestCase(10)]
		[TestCase(25)]
		[TestCase(30)]
		public void GetTemp_GetTemp_ResultIsSame(int temperature)
		{
			_temp.Temp = (temperature);
			Assert.That(_uut.GetCurTemp(), Is.EqualTo(temperature));
		}

	    [Test]
	    public void GetThreshold_GetConstructorAssignment_ThresholdIs1()
	    {
		    Assert.That(_uut.GetThreshold(), Is.EqualTo(1));
	    }

	    [TestCase(-100)]
	    [TestCase(0)]
	    [TestCase(100)]
		public void GetThreshold_SetPosAndNegNumbers_ThresholdIsCorrect(int threshold)
	    {
			_uut.SetThreshold(threshold);
		    Assert.That(_uut.GetThreshold(), Is.EqualTo(threshold));
	    }

	    [TestCase(25, 20, false)]
	    [TestCase(20, 25, true)]
	    [TestCase(25, 25, false)]
	    public void Regulate_SetEqualAndNonEqualTempAndThreshold_StatusIsCorrect(int temp, int treshold, bool status)
	    {
		    _temp.Temp = temp;
		    _uut.SetThreshold(treshold);
		    _uut.Regulate();
		    Assert.That(_heater.Status, Is.EqualTo(status));
	    }
	}
}
