namespace RouletteGame.Legacy
{
	public abstract class Bet
    {
        protected Bet(string name, uint amount)
        {
            PlayerName = name;
            Amount = amount;
        }


        public string PlayerName { get; }
        public uint Amount { get; }

        public virtual uint WonAmount(Field field)
        {
            return 0;
        }
    }
}