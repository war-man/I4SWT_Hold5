using System;
using RouletteGame.Legacy.Bets;

namespace RouletteGame.Legacy
{
    internal class RouletteGameTestApplication
    {
        private static void Main()
        {
            IGameDisplay display = new ConsoleDisplay();
            var game = new RouletteGame(new Roulette(new StandardFieldFactory(), new Randomizer()), display, new BetHandler(display));

            game.OpenBets();
            game.PlaceBet(new ColorBet("Player 1", 100, Field.Black));
            game.PlaceBet(new ColorBet("Player 1", 100, Field.Red));

            game.PlaceBet(new EvenOddBet("Player 2", 100, true));
            game.PlaceBet(new EvenOddBet("Player 2", 100, false));

            for (uint i = 0; i < 36; i++)
                game.PlaceBet(new FieldBet("Player 3", 100, i));

            game.CloseBets();
            game.SpinRoulette();
            game.PayUp();

            Console.ReadKey();
        }
    }
}