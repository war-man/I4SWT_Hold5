using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteGame.Legacy
{
    public interface IBetHandler
    {
       
        bool Add(Bet bet);

        bool BettingOpen{get; set; }

        void PayUp(Field result);
    }

   
}
