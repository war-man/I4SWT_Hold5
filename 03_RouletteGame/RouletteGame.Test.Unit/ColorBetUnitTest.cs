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
		private uint _color = Field.Black;

		[SetUp]
		public void Init()
		{
			_uut = new ColorBet(_name, _amount, _color);
		}
	}
}
