using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using YahtzeeGame;

namespace YahtzeeGame
{
    public class Dice
    {
        //YahtzeeGame y = new YahtzeeGame();
        public Random random = new Random();
        public int[] dice = new int[YahtzeeGame.NUM_DICE];

        public void RollDice()
        {
            for (int i = 0; i < YahtzeeGame.NUM_DICE; i++)
            {
                if (dice[i] == 0)
                    dice[i] = random.Next(1, 7);
            }
        }

        public void RerollDice()
        {
            WriteLine("Enter the indices of dice you want to keep (1-5), separated by commas (e.g., 1,3,5), or type n to keep none:");
            string userInput = ReadLine();
            string input = userInput.ToLower();

            if (input == "n")
            {
                Array.Clear(dice, 0, dice.Length);

                return;
            }

            if (!string.IsNullOrWhiteSpace(input))
            {
                var indicesToKeep = input.Split(',').Select(s => int.Parse(s.Trim()));
                for (int i = 0; i < YahtzeeGame.NUM_DICE; i++)
                {
                    if (!indicesToKeep.Contains(i + 1))
                        dice[i] = 0;
                }
            }
        }
    }
}
