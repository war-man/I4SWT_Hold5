using System;
using RouletteGame.Legacy.Bets;

namespace RouletteGame.Legacy
{
    public class RouletteGame : IRouletteGame
    {
        
        private readonly IRoulette _roulette;
        private readonly IGameDisplay _display;
        private readonly IBetHandler _betHandler;



        public RouletteGame(IRoulette roulette, IGameDisplay display, IBetHandler betHandler)
        {
            _display = display;
            _roulette = roulette;
            _betHandler = betHandler;
        }

        public void OpenBets()
        {
            _display.BetsOpen();
            _betHandler.BettingOpen = true;

        }

        public void CloseBets()
        {
            _display.BetsClosed();
            _betHandler.BettingOpen = false;
        }

        public void PlaceBet(Bet bet)
        {
            if (!_betHandler.Add(bet)) {
            _display.BetsClosedWarning(); }
        }

        public void SpinRoulette()
        {
            _display.Spin();
            _roulette.Spin();
            _display.SpinResult(_roulette.GetResult());
        }

        public void PayUp()
        {
            _betHandler.PayUp(_roulette.GetResult());
            
        }
    }

    public class RouletteGameException : Exception
    {
        public RouletteGameException(string s) : base(s)
        {
        }
    }
}