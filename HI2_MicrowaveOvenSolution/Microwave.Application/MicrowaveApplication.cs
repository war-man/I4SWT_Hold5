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


			//Sumulationg user activities
			Console.WriteLine("User opens door, puts in food and closes the door");
			door.Open();
			door.Close();

			Console.WriteLine("User set up power and time");
			for (int i = 0; i < 5; i++)
			{
			powerButton.Press();	
			}

			for (int i = 0; i < 1; i++)
			{
				timeButton.Press();
			}

			Console.WriteLine("User start the microwave oven and stands there looking at his food turning around");
			startCancelButton.Press();

			System.Threading.Thread.Sleep(10000);

			Console.WriteLine("User want to check if food is ready so opens the door");
			door.Open();
			Console.WriteLine("Suprisingly, food was not ready, User closes dooor");
			door.Close();

			Console.WriteLine("User presses power and time button");
			for (int i = 0; i < 10; i++)
			{
				powerButton.Press();
			}
			timeButton.Press();
			Console.WriteLine("user starts the microwave oven agein");
			startCancelButton.Press();
			
			// Wait while the classes, including the timer, do their job
			System.Console.WriteLine("Press any button to close the application.");
			System.Console.ReadLine();
		}
	}
}
