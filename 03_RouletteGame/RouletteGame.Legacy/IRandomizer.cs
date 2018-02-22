namespace RouletteGame.Legacy
{
	public interface IRandomizer
	{
		uint Next(int low, int high);
	}
}