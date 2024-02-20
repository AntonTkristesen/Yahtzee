using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YahtzeeGame;
using static System.Console;

namespace YahtzeeGame
{
    public class Game
    {
        public static Dice d = new Dice();
        Categories c = new Categories();
        public void Play()
        {
            c.InitializeScorecard();
            for (int round = 1; round <= YahtzeeGame.NUM_ROUNDS; round++)
            {
                WriteLine($"\nRound {round}:");

                int rerollsLeft = YahtzeeGame.MAX_REROLLS;
                Array.Clear(d.dice, 0, d.dice.Length);

                do
                {
                    d.RollDice();

                    WriteLine($"Your roll: {string.Join(", ", d.dice)}");

                    if (rerollsLeft > 0)
                    {
                        WriteLine($"Rerolls left: {rerollsLeft}");
                        d.RerollDice();
                    }

                    // Decrement rerolls left
                    rerollsLeft--;
                } while (rerollsLeft > 0);

                c.ScoreCategory();

                c.DisplayScorecard();
            }
        }
    }
}
