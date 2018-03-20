using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Application
{
	class MicrowaveApplication
	{
		static void Main(string[] args)
		{
			// Create classes
			var door = new Door();
			var powerButton = new Button();
			var startCancelButton = new Button();
			var timeButton = new Button();

			var output = new Output();

			var cookController = new CookController(
				new Timer(),
				new Display(output),
				new PowerTube(output));
			var userInterface = new UserInterface(
				powerButton,
				timeButton,
				startCancelButton,
				door, 
				new Display(output),
				new Light(output), 
				cookController);

			cookController.UI = userInterface;

			// Wait while the classes, including the timer, do their job
			System.Console.WriteLine("Press any button to close the application.");
			System.Console.ReadLine();
		}
	}
}
