using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace YahtzeeGame
{
    public class Categories
    {
        private Dice d = new Dice();
        

        public Dictionary<string, int> scorecard = new Dictionary<string, int>();


        public void ScoreCategory()
        {
            WriteLine("\nChoose a category to score:");
            foreach (var category in scorecard.Keys)
            {
                if (scorecard[category] == -1)
                    WriteLine($"{scorecard.Keys.ToList().IndexOf(category) + 1}. {category}");
            }

            int choice = int.Parse(ReadLine());
            int score = 0;

            switch (choice)
            {
                case 1:
                    score = CountDiceValue(1);
                    break;
                case 2:
                    score = CountDiceValue(2);
                    break;
                case 3:
                    score = CountDiceValue(3);
                    break;
                case 4:
                    score = CountDiceValue(4);
                    break;
                case 5:
                    score = CountDiceValue(5);
                    break;
                case 6:
                    score = CountDiceValue(6);
                    break;
                case 7:
                    score = ThreeOfAKind();
                    break;
                case 8:
                    score = FourOfAKind();
                    break;
                case 9:
                    score = FullHouse();
                    break;
                case 10:
                    score = Straights(3, 30);
                    break;
                case 11:
                    score = Straights(4, 40);
                    break;
                case 12:
                    score = Yahtzee();
                    break;
                case 13:
                    score = Chance();
                    break;
                default:
                    WriteLine("Invalid choice. Try again.");
                    ScoreCategory();
                    return;
            }
            string selectedCategory = scorecard.Keys.ElementAt(choice - 1);
            scorecard[selectedCategory] = score;
            WriteLine($"Score for category {selectedCategory}: {score}");
        }

        public void DisplayScorecard()
        {
            WriteLine("\nScorecard:");
            foreach (var category in scorecard)
            {
                WriteLine($"{category.Key}: {category.Value}");
            }
        }

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

        private int ThreeOfAKind()
        {
            if (Game.d.dice.GroupBy(x => x).Any(g => g.Count() >= 3))
                return Game.d.dice.Sum();
            else
                return 0;
        }

        private int FourOfAKind()
        {
            if (Game.d.dice.GroupBy(x => x).Any(g => g.Count() >= 4))
                return Game.d.dice.Sum();
            else
                return 0;
        }

        private int FullHouse()
        {
            if (Game.d.dice.GroupBy(x => x).Any(g => g.Count() == 3) &&
                Game.d.dice.GroupBy(x => x).Any(g => g.Count() == 2))
                return 25;
            else
                return 0;
        }

        private int Straights(int n, int points)
        {
            var distinctDice = Game.d.dice.Distinct().OrderBy(x => x).ToList();
            int count = 0;

            for (int i = 0; i < distinctDice.Count - 1; i++)
            {
                if (distinctDice[i] + 1 == distinctDice[i + 1])
                {
                    count++;
                    if (count >= n)
                        return points;
                }
                else
                {
                    count = 0;
                }
            }

            return 0;
        }

        private int Yahtzee()
        {
            if (Game.d.dice.GroupBy(x => x).Any(g => g.Count() == 5))
                return 50;
            else
                return 0;
        }

        private int Chance()
        {
            return Game.d.dice.Sum();
        }


        private int CountDiceValue(int value)
        {
            return value * Game.d.dice.Count(d => d == value);
        }
    }
}
