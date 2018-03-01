namespace RouletteGame.Legacy.Bets
{
	public class EvenOddBet : Bet
    {
        private readonly bool _even;

        public EvenOddBet(string name, uint amount, bool even) : base(name, amount)
        {
            _even = even;
        }

        public override uint WonAmount(Field field)
        {
	        if (field != null)
	        {
				if (field.Even == _even) return 2 * Amount;
		        return 0;
			}

	        throw new BetException("Field was null!");
        }

        public override string ToString()
        {
            var evenOddString = _even ? "even" : "odd";

            return $"{Amount}$ even/odd bet on {evenOddString}";
        }
    }
}