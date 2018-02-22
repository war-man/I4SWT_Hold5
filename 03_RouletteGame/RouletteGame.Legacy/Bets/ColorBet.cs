﻿namespace RouletteGame.Legacy
{
	public class ColorBet : Bet
    {
        private readonly uint _color;

        public ColorBet(string name, uint amount, uint color) : base(name, amount)
        {
            _color = color;
        }

        public override uint WonAmount(Field field)
        {
            if (field.Color == _color) return 2*Amount;
            return 0;
        }

        public override string ToString()
        {
            string colorString;

            switch (_color)
            {
                case Field.Red:
                    colorString = "red";
                    break;
                case Field.Black:
                    colorString = "black";
                    break;
                default:
                    colorString = "green";
                    break;
            }

            return $"{Amount}$ color bet on {colorString}";
        }
    }
}