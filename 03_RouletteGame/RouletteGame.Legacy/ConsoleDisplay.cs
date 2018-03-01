using System;
using RouletteGame.Legacy.Bets;

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
	        try
	        {
		        if (won > 0)
			        Console.WriteLine("{0} just won {1}$ on a {2}", bet.PlayerName, bet.WonAmount(field), bet);
			}
	        catch (NullReferenceException e)
	        {
		        Console.WriteLine(e.Message);
	        }
        }

        public void Spin()
        {
            Console.Write("Spinning...");
        }

        public void SpinResult(Field field)
        {
	        if (field != null)
				Console.WriteLine("Result: {0}", field);
        }
    }
}
