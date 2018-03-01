using NUnit.Framework;
using RouletteGame.Legacy;
using RouletteGame.Legacy.Bets;

namespace RouletteGame.Test.Unit
{
	[TestFixture]
	public class EvenOddBetUnitTest
	{
		private EvenOddBet _uut;

		private string _name = "TestPerson";
		private uint _amount = 100;
		private uint _betPayoutMultiplier = 2;

		[TestCase((uint)1, false)]
		[TestCase((uint)2, true)]
		[TestCase((uint)3, false)]
		[TestCase((uint)4, true)]
		public void WonAmount_EvenCheckAmountWithAllBetTypes_AmountIsCorrect(uint number, bool betWon)
		{
			_uut = new EvenOddBet(_name, _amount, true);

			uint expectedAmount;
			if (betWon) expectedAmount = _betPayoutMultiplier * _amount;
			else expectedAmount = 0;

			var field = new Field(number, Field.Black);
			Assert.That(_uut.WonAmount(field), Is.EqualTo(expectedAmount));
		}

		[TestCase((uint)1, true)]
		[TestCase((uint)2, false)]
		[TestCase((uint)3, true)]
		[TestCase((uint)4, false)]
		public void WonAmount_OddCheckAmountWithAllBetTypes_AmountIsCorrect(uint number, bool betWon)
		{
			_uut = new EvenOddBet(_name, _amount, false);

			uint expectedAmount;
			if (betWon) expectedAmount = _betPayoutMultiplier * _amount;
			else expectedAmount = 0;

			var field = new Field(number, Field.Black);
			Assert.That(_uut.WonAmount(field), Is.EqualTo(expectedAmount));
		}
	}
}
