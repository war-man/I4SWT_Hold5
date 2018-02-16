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

	    [TestCase(true, true, true)]
	    [TestCase(true, false, false)]
	    [TestCase(false, true, false)]
	    [TestCase(false, false, false)]
		public void RunSelfTest_SetTrueFalse_SelfTestIsCorrect(bool tempTest, bool heaterTest, bool result)
	    {
		    _temp.SelfTestStatus = tempTest;
		    _heater.SelfTestStatus = heaterTest;

			Assert.That(_uut.RunSelfTest(), Is.EqualTo(result));
	    }
	}
}
