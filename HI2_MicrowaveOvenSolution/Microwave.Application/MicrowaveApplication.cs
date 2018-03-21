using System;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;

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


			// Simulation of user activities
			Console.WriteLine("User opens the door.");
			door.Open();

			Console.WriteLine("User puts in his food and closes the door.");
			door.Close();

			Console.WriteLine("User sets up power level by pressing the power button a couple of times.");
			for (int i = 0; i < 5; i++)
			{
				powerButton.Press();
			}

			Console.WriteLine("User sets up timer.");
			timeButton.Press();

			Console.WriteLine("User starts the microwave oven and stands there looking at his food turning around.");
			startCancelButton.Press();
			System.Threading.Thread.Sleep(10000);

			Console.WriteLine("User wants to check if food is ready, so he opens the door.");
			door.Open();

			Console.WriteLine("Surprisingly, the food was not ready. User closes door again.");
			door.Close();

			Console.WriteLine("User sets up power level by pressing the power button a couple of times.");
			for (int i = 0; i < 10; i++)
			{
				powerButton.Press();
			}

			Console.WriteLine("User sets up timer.");
			timeButton.Press();

			Console.WriteLine("User starts the microwave oven again.");
			startCancelButton.Press();

			// Wait while the classes, including the timer, do their job
			Console.WriteLine("Press any button to close the application.");
			Console.ReadLine();
		}
	}
}
