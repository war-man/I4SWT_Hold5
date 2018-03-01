using RouletteGame.Legacy.Bets;

namespace RouletteGame.Legacy
{
    public interface IGameDisplay
    {
        void BetsClosed();
        void BetsClosedWarning();
        void BetsOpen();
        void PayUp(Bet bet, Field field, double won);
        void Spin();
        void SpinResult(Field field);
    }
}
