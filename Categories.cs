using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Yahtzee3;

namespace Yahtzee3
{
    public class Categories
    {
        private Dictionary<string, int> scorecard = new Dictionary<string, int>();
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

            PointSystem pointSystem = new PointSystem();
            switch (choice)
            {
                case 1:
                    score = pointSystem.CountDiceValue(1);
                    break;
                case 2:
                    score = pointSystem.CountDiceValue(2);
                    break;
                case 3:
                    score = pointSystem.CountDiceValue(3);
                    break;
                case 4:
                    score = pointSystem.CountDiceValue(4);
                    break;
                case 5:
                    score = pointSystem.CountDiceValue(5);
                    break;
                case 6:
                    score = pointSystem.CountDiceValue(6);
                    break;
                case 7:
                    score = pointSystem.ThreeOfAKind();
                    break;
                case 8:
                    score = pointSystem.FourOfAKind();
                    break;
                case 9:
                    score = pointSystem.FullHouse();
                    break;
                case 10:
                    score = pointSystem.SmallStraight();
                    break;
                case 11:
                    score = pointSystem.LargeStraight();
                    break;
                case 12:
                    score = pointSystem.Yahtzee();
                    break;
                case 13:
                    score = pointSystem.Chance();
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
    }
}
