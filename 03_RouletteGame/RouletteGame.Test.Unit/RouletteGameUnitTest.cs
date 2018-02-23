using NUnit.Framework;
using NSubstitute;
using RouletteGame.Legacy;

namespace RouletteGame.Test.Unit
{
	[TestFixture]
	public class RouletteGameUnitTest
	{
	    private Legacy.RouletteGame _uut;
	    private IRoulette _roulette;
	    private IGameDisplay _display;

		[SetUp]
		public void Init()
		{
		    _roulette = Substitute.For<IRoulette>();
            _display = Substitute.For<IGameDisplay>();

            _uut = new Legacy.RouletteGame(_roulette, _display );
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
	        _display.Received().SpinResult(field);

	    }
    }
}
