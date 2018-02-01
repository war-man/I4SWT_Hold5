using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Calculator.Test.Unit
{
	[TestFixture]
	public class CalculatorUnitTest
	{
		private Calculator _uut;

		[SetUp]
		public void Init()
		{
			_uut = new Calculator();
		}

		[TestCase(2.3, 4, 6.3)]
	    [TestCase(-2, 4, 2)]
	    [TestCase(2, -4, -2)]
	    [TestCase(-2, -4, -6)]
        public void Add_AddPosAndNegNumbers_ResultIsCorrect(double a, double b, double result)
	    {
	        Assert.That(_uut.Add(a, b), Is.EqualTo(result));
	    }

	    [TestCase(2, 4, -2)]
	    [TestCase(-2, 4, -6)]
	    [TestCase(2, -4, 6)]
	    [TestCase(-2, -4, 2)]
	    public void Subtract_SubtractPosAndNegNumbers_ResultIsCorrect(double a, double b, double result)
	    {
	        Assert.That(_uut.Subtract(a, b), Is.EqualTo(result));
	    }

	    [TestCase(2, 4, 8)]
	    [TestCase(-2, 4, -8)]
	    [TestCase(2, -4, -8)]
	    [TestCase(-2, -4, 8)]
	    [TestCase(0, 2, 0)]
	    [TestCase(2, 0, 0)]
	    [TestCase(0, 0, 0)]
		public void Multiply_MultiplyNumbers_ResultIsCorrect(double a, double b, double result)
	    {
	        Assert.That(_uut.Multiply(a, b), Is.EqualTo(result));
	    }

	    [TestCase(3, 2, 9)]
	    [TestCase(-3, 2, 9)]
	    [TestCase(3, 2.3, 12.513502532843182)]
	    [TestCase(-3, 2.3, Double.NaN)]
	    [TestCase(0, 1, 0)]
	    [TestCase(45, 0, 1)]
        public void Power_RaiseNumbers_ResultIsCorrect(double x, double exp, double result)
	    {
	        Assert.That(_uut.Power(x, exp), Is.EqualTo(result));
	    }
    }
}

namespace Calculator.Test.Unit
{
    class TestFixtureAttribute : Attribute
    {
    }
}