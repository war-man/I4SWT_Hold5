﻿using System;

namespace RouletteGame.Legacy
{
	public class Randomizer : IRandomizer
	{
		public uint NextRandom(int low, int high)
		{
			return (uint) new Random().Next(low, high);
		}
	}
}