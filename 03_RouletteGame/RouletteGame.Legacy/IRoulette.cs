using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteGame.Legacy
{
    public interface IRoulette
    {
        void Spin();

        Field GetResult();
    }
}
