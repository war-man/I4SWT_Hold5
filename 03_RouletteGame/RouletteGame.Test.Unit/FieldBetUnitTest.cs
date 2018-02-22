using NUnit.Framework;
using RouletteGame.Legacy;

namespace RouletteGame.Test.Unit
{
	[TestFixture]
	public class FieldBetUnitTest
	{
		private FieldBet _uut;

		private string _name = "TestPerson";
		private uint _amount = 100;
		private uint _betPayoutMultiplier = 36;

		[TestCase((uint)0, true)]
		[TestCase((uint)1, false)]
		[TestCase((uint)36, false)]
		public void WonAmount_Bet0CheckAmountWithDifferentNumbers_AmountIsCorrect(uint number, bool betWon)
		{
			_uut = new FieldBet(_name, _amount, 0);

			uint expectedAmount;
			if (betWon) expectedAmount = _betPayoutMultiplier * _amount;
			else expectedAmount = 0;

			var field = new Field(number, Field.Black);
			Assert.That(_uut.WonAmount(field), Is.EqualTo(expectedAmount));
		}

		[TestCase((uint)0, false)]
		[TestCase((uint)1, true)]
		[TestCase((uint)36, false)]
		public void WonAmount_Bet1CheckAmountWithDifferentNumbers_AmountIsCorrect(uint number, bool betWon)
		{
			_uut = new FieldBet(_name, _amount, 1);

			uint expectedAmount;
			if (betWon) expectedAmount = _betPayoutMultiplier * _amount;
			else expectedAmount = 0;

			var field = new Field(number, Field.Black);
			Assert.That(_uut.WonAmount(field), Is.EqualTo(expectedAmount));
		}

		[TestCase((uint)0, false)]
		[TestCase((uint)1, false)]
		[TestCase((uint)36, true)]
		public void WonAmount_Bet36CheckAmountWithDifferentNumbers_AmountIsCorrect(uint number, bool betWon)
		{
			_uut = new FieldBet(_name, _amount, 36);

			uint expectedAmount;
			if (betWon) expectedAmount = _betPayoutMultiplier * _amount;
			else expectedAmount = 0;

			var field = new Field(number, Field.Black);
			Assert.That(_uut.WonAmount(field), Is.EqualTo(expectedAmount));
		}
	}
}
