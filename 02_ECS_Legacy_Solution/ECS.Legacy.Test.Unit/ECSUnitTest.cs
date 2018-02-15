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

	    [SetUp]
	    public void Init()
	    {
		    _uut = new ECS(1, new FakeTempSensor(25), new FakeHeater());
	    }

		[Test]
		public void dummy()
		{
			Assert.That(_uut.GetCurTemp(), Is.EqualTo(25));
		}
	}
}
