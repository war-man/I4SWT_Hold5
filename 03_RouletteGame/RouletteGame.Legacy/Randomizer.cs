using System;

namespace RouletteGame.Legacy
{
	class Randomizer : IRandomizer
	{
		public uint Next(int low, int high)
		{
			return (uint) new Random().Next(low, high);
		}
	}
}