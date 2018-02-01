using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
	public class Calculator
	{
		public double Add(double a, double b)
		{
			return a + b;
		}

		public double Subtract(double a, double b)
		{
			return a - b;
		}

		public double Multiply(double a, double b)
		{
			return a * b;
		}

	    public double Divide(double dividend, double divisor)
	    {
            if(divisor == 0.0d) throw new ArithmeticException("denominator == 0");

	        return dividend / divisor;
	    }

        public double Power(double x, double exp)
		{
			return Math.Pow(x, exp);
		}
	}	
}
