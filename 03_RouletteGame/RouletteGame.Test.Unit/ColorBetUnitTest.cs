using NUnit.Framework;
using RouletteGame.Legacy;

namespace RouletteGame.Test.Unit
{
	[TestFixture]
	public class ColorBetUnitTest
	{
		private ColorBet _uut;

		private string _name = "TestPerson";
		private uint _amount = 100;
		private uint _betPayoutMultiplier = 2;

		[TestCase(Field.Red, true)]
		[TestCase(Field.Black, false)]
		[TestCase(Field.Green, false)]
		public void WonAmount_RedColorCheckAmountWithAllColors_AmountIsCorrect(uint color, bool betWon)
		{
			_uut = new ColorBet(_name, _amount, Field.Red);

			uint expectedAmount;
			if (betWon) expectedAmount = _betPayoutMultiplier * _amount;
			else expectedAmount = 0;

			var field = new Field(0, color);
			Assert.That(_uut.WonAmount(field), Is.EqualTo(expectedAmount));
		}

		[TestCase(Field.Red, false)]
		[TestCase(Field.Black, true)]
		[TestCase(Field.Green, false)]
		public void WonAmount_BlackColorCheckAmountWithAllColors_AmountIsCorrect(uint color, bool betWon)
		{
			_uut = new ColorBet(_name, _amount, Field.Black);

			uint expectedAmount;
			if (betWon) expectedAmount = _betPayoutMultiplier * _amount;
			else expectedAmount = 0;

			var field = new Field(0, color);
			Assert.That(_uut.WonAmount(field), Is.EqualTo(expectedAmount));
		}

		[TestCase(Field.Red, false)]
		[TestCase(Field.Black, false)]
		[TestCase(Field.Green, true)]
		public void WonAmount_GreenColorCheckAmountWithAllColors_AmountIsCorrect(uint color, bool betWon)
		{
			_uut = new ColorBet(_name, _amount, Field.Green);

			uint expectedAmount;
			if (betWon) expectedAmount = _betPayoutMultiplier * _amount;
			else expectedAmount = 0;

			var field = new Field(0, color);
			Assert.That(_uut.WonAmount(field), Is.EqualTo(expectedAmount));
		}
	}
}
