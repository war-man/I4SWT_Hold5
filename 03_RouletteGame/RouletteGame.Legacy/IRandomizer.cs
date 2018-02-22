namespace RouletteGame.Legacy
{
	interface IRandomizer
	{
		uint Next(int low, int high);
	}
}