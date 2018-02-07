using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
	class TestCalculator
	{
		static void Main(string[] args)
		{
			Calculator calc = new Calculator();  // Create Calculator object

			// Test Calculator
			Console.WriteLine("============================");
			Console.WriteLine("Test of calculator:");
			Console.WriteLine("============================");
			Console.WriteLine("Test add:\t 5 + 4 =\t " + calc.Add(5, 4) + ".\t Should return 9.");
			Console.WriteLine("Test add:\t -5 + 4 =\t " + calc.Add(-5, 4) + ".\t Should return -1.");
			Console.WriteLine("Test add:\t 5 + -4 =\t " + calc.Add(5, -4) + ".\t Should return 1.");
			Console.WriteLine("Test add:\t -5 + -4 =\t " + calc.Add(-5, -4) + ".\t Should return -9.");
			Console.WriteLine("----------------------------");
			Console.WriteLine("Test subtract:\t 9 - 8 = " + calc.Subtract(9, 8));
			Console.WriteLine("----------------------------");
			Console.WriteLine("Test multiply:\t 5 * 4 = " + calc.Multiply(5, 4));
			Console.WriteLine("----------------------------");
			Console.WriteLine("Test power:\t 3^2   = " + calc.Power(3, 2));
            Console.WriteLine();
		}
	}
}
