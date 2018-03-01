using RouletteGame.Legacy.Bets;

namespace RouletteGame.Legacy
{
    public interface IRouletteGame
    {
       void OpenBets();

        void CloseBets();

        void PlaceBet(Bet bet);

        void SpinRoulette();
        void PayUp();
    }
}
