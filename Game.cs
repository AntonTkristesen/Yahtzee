using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Yahtzee3;

namespace Yahtzee3
{
    public class Game
    {
        private const int NUM_DICE = 5;
        private const int NUM_ROUNDS = 13;
        private const int MAX_REROLLS = 3;
        private int[] dice = new int[NUM_DICE];
        private Random random = new Random();
        private Dictionary<string, int> scorecard = new Dictionary<string, int>();
        Scoreboard sb = new Scoreboard();
        Categories cat = new Categories();

        public Game()
        {
            sb.InitializeScorecard();
        }



        public void Play()
        {
            Dice diceClass = new Dice();
            for (int round = 1; round <= NUM_ROUNDS; round++)
            {
                WriteLine($"\nRound {round}:");

                int rerollsLeft = MAX_REROLLS;
                Array.Clear(dice, 0, dice.Length);

                do
                {
                    diceClass.RollDice();

                    WriteLine($"Your roll: {string.Join(", ", dice)}");

                    if (rerollsLeft > 0)
                    {
                        WriteLine($"Rerolls left: {rerollsLeft}");
                        diceClass.RerollDice();
                    }

                    rerollsLeft--;
                } while (rerollsLeft > 0);

                cat.ScoreCategory();

                sb.DisplayScorecard();
            }
        }
    }
}
