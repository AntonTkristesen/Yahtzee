using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Yahtzee3
{
    public class Dice
    {
        private const int NUM_DICE = 5;
        private int[] dice = new int[NUM_DICE];
        private Random random = new Random();
        public void RollDice()
        {
            for (int i = 0; i < NUM_DICE; i++)
            {
                if (dice[i] == 0)
                    dice[i] = random.Next(1, 7);
            }
        }

        public void RerollDice()
        {
            WriteLine("Enter the indices of dice you want to keep (1-5), separated by commas (e.g., 1,3,5):");
            string input = ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                var indicesToKeep = input.Split(',').Select(s => int.Parse(s.Trim()));
                for (int i = 0; i < NUM_DICE; i++)
                {
                    if (!indicesToKeep.Contains(i + 1))
                        dice[i] = 0;
                }
            }
        }
    }
}
