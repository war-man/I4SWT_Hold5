using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
