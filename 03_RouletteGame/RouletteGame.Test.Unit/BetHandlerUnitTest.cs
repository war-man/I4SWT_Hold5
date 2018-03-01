using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using RouletteGame.Legacy;
using RouletteGame.Legacy.Bets;

namespace RouletteGame.Test.Unit
{
    [TestFixture]
    class BetHandlerUnitTest
    {
        private BetHandler _uut;
        private IGameDisplay _display;

        [SetUp]
        public void Init()
        {
            _display = Substitute.For<IGameDisplay>();
            _uut = new BetHandler(_display);
        }

        [Test]
        public void BetHandler_add_bettingOpen_test()
        {
            _uut.BettingOpen = true;
            ColorBet bet = new ColorBet("Tbet",1,1);

            _uut.Add(bet);


            //Assert.That(bet Is.EquivalentTo(_uut.GetList[0] );
            Assert.That(_uut.GetList, Contains.Item(bet));

        }
        [Test]
        public void BetHandler_add_bettingCloesed_test()
        {
            _uut.BettingOpen = false;
            ColorBet bet = new ColorBet("Tbet", 1, 1);

            _uut.Add(bet);
            
            Assert.That(_uut.GetList, Has.No.Member(bet));

        }
        [Test]
        public void BetHandler_payUp_test()
        {
            _uut.BettingOpen = true;
            ColorBet bet = new ColorBet("Tbet", 1, 1);
            ColorBet bet2 = new ColorBet("Tbet2", 2, 2);
            Field field =new Field(1,1);

            _uut.Add(bet);
            _uut.Add(bet2);

            _uut.PayUp(field);

           _display.ReceivedWithAnyArgs(2).PayUp(bet,field, 100);

        }
    }
}
