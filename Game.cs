using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Yahtzee
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Game
    {
        private List<Player> players;
        private DiceCup diceCup;
        private ScoreCalculator scoreCalculator;
        private Random random;
        private int turnCount;
        private const int MaxTurns = 13; // In standard Yahtzee there are 13 turns

        public Game(List<string> playerNames)
        {
            players = playerNames.Select(name => new Player(name)).ToList();
            diceCup = new DiceCup(5); // Yahtzee is played with 5 dice
            scoreCalculator = new ScoreCalculator();
            random = new Random();
            turnCount = 0;
        }

        public void Start()
        {
            while (turnCount < MaxTurns)
            {
                foreach (var player in players)
                {
                    PlayTurn(player);
                }
                turnCount++;
            }

            Player winner = DetermineWinner();
            Console.WriteLine($"Winner is {winner.Name} with a total score of {winner.Scorecard.CalculateTotalScore()}!");
        }

        private void PlayTurn(Player player)
        {
            Console.WriteLine($"{player.Name}'s turn:");
            bool[] diceKept = new bool[5]; // Tracks which dice are kept. False means the die will be rolled.

            // Allow the player to roll the dice up to 3 times
            for (int roll = 0; roll < 3; roll++)
            {
                // Roll only the dice that have not been kept
                for (int i = 0; i < diceCup.Dice.Count; i++)
                {
                    if (!diceKept[i])
                    {
                        diceCup.Dice[i].Roll(random);
                    }
                }
                DisplayDice();

                if (roll < 2) // No need to choose dice to keep after the last roll
                {
                    Console.WriteLine("Choose dice to keep (1-5), or 'r' to re-roll all:");
                    var input = Console.ReadLine();

                    if (input.ToLower() == "r")
                    {
                        Array.Fill(diceKept, false); // Reset the kept dice if re-rolling all
                        continue;
                    }

                    var savedDices = input.Split(',').Select(x => int.Parse(x.Trim()) - 1).ToList(); // Adjust for zero-based index
                    foreach (var index in savedDices)
                    {
                        if (index >= 0 && index < 5)
                        {
                            diceKept[index] = true; // Mark the selected dice as kept
                        }
                    }
                }
            }

            ChooseCombination(player);
        }


        private void DisplayDice()
        {
            Console.WriteLine("Dice: " + string.Join(", ", diceCup.Dice.Select(d => d.Value)));
        }

        private void SelectDiceToKeep(int[] savedDices)
        {
            // Adjusting user input to match zero-based indexing
            var indicesToKeep = savedDices.Select(i => i - 1).ToList();

            // Create a new list to hold the dice to keep
            List<Die> diceToKeep = new List<Die>();

            // Add the selected dice to the new list
            foreach (int index in indicesToKeep)
            {
                if (index >= 0 && index < diceCup.Dice.Count)
                {
                    diceToKeep.Add(diceCup.Dice[index]);
                    Console.Write(index + 1 + " "); // Adjusted to match user expectation of 1-based indexing
                }
            }
            Console.WriteLine(); // To ensure output is nicely formatted

            // Clear the diceCup and add the kept dice back
            diceCup.Dice.Clear();
            diceCup.Dice.AddRange(diceToKeep);

            // Refill the dice cup to ensure there are 5 dice
            while (diceCup.Dice.Count < 5)
            {
                diceCup.Dice.Add(new Die());
            }
        }


        private void ChooseCombination(Player player)
        {
            Console.WriteLine("Choose a combination to score:");
            var combination = Console.ReadLine();

            if (player.Scorecard.Scores.ContainsKey(combination) && player.Scorecard.Scores[combination] == -1)
            {
                var score = scoreCalculator.CalculateScore(combination, diceCup.Dice.Select(d => d.Value).ToList());
                player.Scorecard.SetScore(combination, score);
                Console.WriteLine($"Scored {score} points for {combination}.");
            }
            else
            {
                Console.WriteLine("Invalid combination or already scored. Please choose another.");
                ChooseCombination(player); // Recursive call for simplicity
            }
        }

        private Player DetermineWinner()
        {
            return players.OrderByDescending(p => p.Scorecard.CalculateTotalScore()).First();
        }
    }

}
