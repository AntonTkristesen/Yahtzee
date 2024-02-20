using System;
using System.Linq;
using static System.Console;

namespace YahtzeeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Welcome to Yahtzee!");

            // Create a new instance of the Yahtzee game
            YahtzeeGame yahtzee = new YahtzeeGame();

            // Play the game
            yahtzee.Play();

            WriteLine("Thanks for playing Yahtzee!");
        }
    }


    public class YahtzeeGame
    {
        private const int NUM_DICE = 5;
        private const int NUM_ROUNDS = 2;
        private const int MAX_REROLLS = 3;
        private int[] dice = new int[NUM_DICE];
        private Random random = new Random();
        private Dictionary<string, int> scorecard = new Dictionary<string, int>();

        public YahtzeeGame()
        {
            InitializeScorecard();
        }

        private void InitializeScorecard()
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

        public void Play()
        {
            for (int round = 1; round <= NUM_ROUNDS; round++)
            {
                WriteLine($"\nRound {round}:");

                int rerollsLeft = MAX_REROLLS;
                Array.Clear(dice, 0, dice.Length);

                do
                {
                    RollDice();

                    WriteLine($"Your roll: {string.Join(", ", dice)}");

                    if (rerollsLeft > 0)
                    {
                        WriteLine($"Rerolls left: {rerollsLeft}");
                        RerollDice();
                    }

                    // Decrement rerolls left
                    rerollsLeft--;
                } while (rerollsLeft > 0);

                ScoreCategory();

                DisplayScorecard();
            }
        }

        private void RollDice()
        {
            for (int i = 0; i < NUM_DICE; i++)
            {
                if (dice[i] == 0)
                    dice[i] = random.Next(1, 7);
            }
        }

        private void RerollDice()
        {
            WriteLine("Enter the indices of dice you want to keep (1-5), separated by commas (e.g., 1,3,5), or type n to keep none:");
            string userInput = ReadLine();
            string input = userInput.ToLower();

            if (input == "n") {
                Array.Clear(dice, 0, dice.Length);

                return;
            }

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

        private void ScoreCategory()
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
                    score = SmallStraight();
                    break;
                case 11:
                    score = LargeStraight();
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

        private int ThreeOfAKind()
        {
            if (dice.GroupBy(x => x).Any(g => g.Count() >= 3))
                return dice.Sum();
            else
                return 0;
        }

        private int FourOfAKind()
        {
            if (dice.GroupBy(x => x).Any(g => g.Count() >= 4))
                return dice.Sum();
            else
                return 0;
        }

        private int FullHouse()
        {
            if (dice.GroupBy(x => x).Any(g => g.Count() == 3) &&
                dice.GroupBy(x => x).Any(g => g.Count() == 2))
                return 25;
            else
                return 0;
        }

        private int SmallStraight()
        {
            if (dice.Distinct().OrderBy(x => x).Select((value, index) => value - index).Distinct().Count() >= 4)
                return 30;
            else
                return 0;
        }

        private int LargeStraight()
        {
            if (dice.Distinct().OrderBy(x => x).Select((value, index) => value - index).Distinct().Count() == 5)
                return 40;
            else
                return 0;
        }

        private int Yahtzee()
        {
            if (dice.GroupBy(x => x).Any(g => g.Count() == 5))
                return 50;
            else
                return 0;
        }

        private int Chance()
        {
            return dice.Sum();
        }


        private int CountDiceValue(int value)
        {
            return value * dice.Count(d => d == value);
        }

        private void DisplayScorecard()
        {
            WriteLine("\nScorecard:");
            foreach (var category in scorecard)
            {
                WriteLine($"{category.Key}: {category.Value}");
            }
        }
    }
}
