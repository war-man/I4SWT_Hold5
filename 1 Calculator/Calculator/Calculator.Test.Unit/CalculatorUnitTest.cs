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

        [TestCase(1, 1, 1, 3)]
        [TestCase(1, 1, -1, 1)]
        [TestCase(1, -2, 1, 0)]
        [TestCase(-1, -1, -1, -3)]
        [TestCase(1.1, 1.1, 1.1, 3.3)]
        [TestCase(-1.1, -1.1, -1.1, -3.3)]
        [TestCase(double.MaxValue, 1, 1, double.MaxValue)]
        public void Add_By_accummulate_Neg_and_pos(double a, double b, double c, double result)
	    {
	        _uut.Add(a, b);

	        Assert.That(_uut.Add(c), Is.EqualTo(result).Within(0.1));
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

	    [TestCase(2.5, 5, 0.5)]
	    [TestCase(9, -3, -3)]
	    [TestCase(-9, 3, -3)]
	    [TestCase(-9, -3, 3)]
        public void Divide_DivideNumbers_ResultIsCorrect(double a, double b, double result)
	    {
            Assert.That(_uut.Divide(a,b),Is.EqualTo(result));
	    }

	    [Test]
	    public void Divide_DivideByZero_ThrowsArithmeticException()
	    {
	        Assert.Throws<ArithmeticException>(() => _uut.Divide(5, 0));
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

		// ---------------------------------------------------------------------
		// Test Accumulator
		[TestCase(2.3, 4, 6.3)]
		[TestCase(-2, 4, 2)]
		[TestCase(2, -4, -2)]
		[TestCase(-2, -4, -6)]
		public void Accumulator_AddPosAndNegNumbers_ResultIsCorrect(double a, double b, double result)
		{
			Assert.That(_uut.Add(a, b), Is.EqualTo(_uut.Accumulator));
		}

		[TestCase(2, 4, -2)]
		[TestCase(-2, 4, -6)]
		[TestCase(2, -4, 6)]
		[TestCase(-2, -4, 2)]
		public void Accumulator_SubtractPosAndNegNumbers_ResultIsCorrect(double a, double b, double result)
		{
			Assert.That(_uut.Subtract(a, b), Is.EqualTo(_uut.Accumulator));
		}

		[TestCase(2, 4, 8)]
		[TestCase(-2, 4, -8)]
		[TestCase(2, -4, -8)]
		[TestCase(-2, -4, 8)]
		[TestCase(0, 2, 0)]
		[TestCase(2, 0, 0)]
		[TestCase(0, 0, 0)]
		public void Accumulator_MultiplyNumbers_ResultIsCorrect(double a, double b, double result)
		{
			Assert.That(_uut.Multiply(a, b), Is.EqualTo(_uut.Accumulator));
		}

		[TestCase(3, 2, 9)]
		[TestCase(-3, 2, 9)]
		public void Accumulator_RaiseNumbers_ResultIsCorrect(double x, double exp, double result)
		{
			Assert.That(_uut.Power(x, exp), Is.EqualTo(_uut.Accumulator));
		}

		// ---------------------------------------------------------------------
		// Test Clear
		[TestCase()]
		public void Clear_ClearWhenAccumulatorIsCleared_ResultIsZero()
		{
			_uut.Accumulator = 0;
			_uut.Clear();
			Assert.That(_uut.Accumulator, Is.EqualTo(0));
		}

		[TestCase()]
		public void Clear_ClearWhenAccumulatorIsNotCleared_ResultIsZero()
		{
			_uut.Accumulator = 10;
			_uut.Clear();
			Assert.That(_uut.Accumulator, Is.EqualTo(0));
		}
	}
}

namespace Calculator.Test.Unit
{
    class TestFixtureAttribute : Attribute
    {
    }
}