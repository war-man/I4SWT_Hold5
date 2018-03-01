using RouletteGame.Legacy.Bets;

namespace RouletteGame.Legacy
{
    public interface IBetHandler
    {
       
        bool Add(Bet bet);

        bool BettingOpen{get; set; }

        void PayUp(Field result);
    }
}
