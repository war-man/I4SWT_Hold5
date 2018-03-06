namespace RouletteGame.Legacy
{
	public interface IRandomizer
	{
		uint NextRandom(int low, int high);
	}
}