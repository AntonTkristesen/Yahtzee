using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee3;
using static System.Console;
namespace Yahtzee3
{
    public class Scoreboard
    {
        private Dictionary<string, int> scorecard = new Dictionary<string, int>();
        public void InitializeScorecard()
        {
            scorecard.Add("Ones", -1);
            scorecard.Add("Twos", -1);
            scorecard.Add("Threes", -1);
            scorecard.Add("Fours", -1);
            scorecard.Add("Fives", -1);
            scorecard.Add("Sixes", -1);
            scorecard.Add("Three of a Kind", -1);
            scorecard.Add("Four of a Kind", -1);
            scorecard.Add("Full House", -1);
            scorecard.Add("Small Straight", -1);
            scorecard.Add("Large Straight", -1);
            scorecard.Add("Yahtzee", -1);
            scorecard.Add("Chance", -1);
        }

        public void DisplayScorecard()
        {
            WriteLine("\nScorecard:");
            foreach (var category in scorecard)
            {
                WriteLine($"{category.Key}: {category.Value}");
            }
        }
    }
}
