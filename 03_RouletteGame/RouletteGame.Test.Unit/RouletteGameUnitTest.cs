﻿using NUnit.Framework;
using NSubstitute;
using RouletteGame.Legacy;
using RouletteGame.Legacy.Bets;

namespace RouletteGame.Test.Unit
{
    [TestFixture]
    public class RouletteGameUnitTest
    {
        private Legacy.RouletteGame _uut;
        private IRoulette _roulette;
        private IGameDisplay _display;
        private IBetHandler _betHandler;

        [SetUp]
        public void Init()
        {
            _roulette = Substitute.For<IRoulette>();
            _display = Substitute.For<IGameDisplay>();
            _betHandler = Substitute.For<IBetHandler>();

            _uut = new Legacy.RouletteGame(_roulette, _display, _betHandler);
        }

        [Test]
        public void RouletteGame_display_OpenBets_Called()
        {
            //act
            _uut.OpenBets();

            //assert
            _display.Received().BetsOpen();
        }

        [Test]
        public void RouletteGame_display_ClosedBets_Called()
        {
            //act
            _uut.CloseBets();

            //assert
            _display.Received().BetsClosed();

        }

        [Test]
        public void RouletteGame_display_PlacedBet_Roundclosed_ClosedBetsWarning_Called()
        {
            ColorBet bet = new ColorBet("test", 100, 2);

            //act
            _uut.CloseBets();
            _uut.PlaceBet(bet);

            //assert
            _display.Received().BetsClosedWarning();

        }

        [Test]
        public void RouletteGame_display_PlacedBet_Roundopen_ClosedBetsWarning_NotCalled()
        {
            ColorBet bet = new ColorBet("test", 100, 2);

            //act
            _uut.OpenBets();
            _uut.PlaceBet(bet);

            //assert
            _display.DidNotReceive().BetsClosedWarning();

        }

        [Test]
        public void RouletteGame_display_SpinRoulette_Spin_called()
        {
            _uut.SpinRoulette();
            _display.Received().Spin();

        }

        [Test]
        public void RouletteGame_display_SpinRoulette_SpinResult_called()
        {
            Field field = new Field(1, 1);

            _uut.SpinRoulette();
            var result = _roulette.GetResult();

            _display.ReceivedWithAnyArgs().SpinResult(result);

        }

        [Test]
        public void RouletteGame_BetHandler_PayUp_called()
        {
            Field field = new Field(1, 1);

            _uut.PayUp();
            var result = _roulette.GetResult();
            
            _betHandler.Received().PayUp(result);
        }
    }
}
