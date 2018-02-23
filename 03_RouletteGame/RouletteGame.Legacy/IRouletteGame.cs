using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
