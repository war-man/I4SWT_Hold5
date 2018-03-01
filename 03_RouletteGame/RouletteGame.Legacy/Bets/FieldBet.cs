using System;

namespace RouletteGame.Legacy.Bets
{
	public class FieldBet : Bet
    {
        private readonly uint _fieldNumber;

        public FieldBet(string name, uint amount, uint fieldNumber) : base(name, amount)
        {
            _fieldNumber = fieldNumber;
        }

        public override uint WonAmount(Field field)
        {
	        if (field != null)
	        {
		        if (field.Number == _fieldNumber) return 36 * Amount;
		        return 0;
			}

	        throw new BetException("Field was null!");
        }

        public override string ToString()
        {
            return $"{Amount}$ field bet on {_fieldNumber}";
        }
    }
}