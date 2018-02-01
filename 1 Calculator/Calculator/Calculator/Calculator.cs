using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
	public class Calculator
	{
		public double Accumulator { get; set; }

		public double Add(double a, double b)
		{
			Accumulator = a + b;

			return Accumulator;
		}

		public double Subtract(double a, double b)
		{
			Accumulator = a - b;

			return Accumulator;
		}

		public double Multiply(double a, double b)
		{
			Accumulator = a * b;

			return Accumulator;
		}

	    public double Divide(double dividend, double divisor)
	    {
            if(divisor == 0.0d) throw new ArithmeticException("denominator == 0");

	        return dividend / divisor;
	    }

        public double Power(double x, double exp)
		{
			Accumulator = Math.Pow(x, exp);

			return Accumulator;
		}

		public void Clear()
		{
			Accumulator = 0;
		}
	}
}