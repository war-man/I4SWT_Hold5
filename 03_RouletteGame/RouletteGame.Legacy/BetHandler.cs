using System.Collections.Generic;
using RouletteGame.Legacy.Bets;

namespace RouletteGame.Legacy
{
    public class BetHandler : IBetHandler
    {
        private readonly List<Bet> _bets;
        private readonly IGameDisplay _display;
        
        public BetHandler(IGameDisplay display)
        {
            _display = display;
            _bets = new List<Bet>();
        }
        public bool Add(Bet bet)
        {
            if (BettingOpen)
            {
                _bets.Add(bet);
                return true;
            }
            else return false;
        }
        public bool BettingOpen { get; set; }
        
        public void PayUp(Field result)
        {
            foreach (var bet in _bets)
            {
                _display.PayUp(bet, result, bet.WonAmount(result));
            }
        }

        public List<Bet> GetList => _bets;
    }
}
