using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteGame.Legacy
{
    public  class ConsoleDisplay : IGameDisplay
    {
        public void BetsClosed()
        {
            Console.WriteLine("Round is closed for bets");
        }

        public void BetsClosedWarning()
        {
            throw new RouletteGameException("Bet placed while round closed");
        }

        public void BetsOpen()
        {
            Console.WriteLine("Round is open for bets");
        }

        public void PayUp(Bet bet, Field field, double won)
        {
            
            if (won > 0)
                Console.WriteLine("{0} just won {1}$ on a {2}", bet.PlayerName, bet.WonAmount(field), bet);
        }

        public void Spin()
        {
            Console.Write("Spinning...");
        }

        public void SpinResult(Field field)
        {
            Console.WriteLine("Result: {0}", field);
        }
    }
}
