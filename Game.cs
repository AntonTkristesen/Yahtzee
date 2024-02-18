using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // Allow the player to roll the dice up to 3 times
            for (int roll = 0; roll < 3; roll++)
            {
                diceCup.RollAll(random);
                DisplayDice();

                if (roll < 2)
                {
                    Console.WriteLine("Choose dice to keep (1-5), or 'r' to re-roll all:");
                    var input = Console.ReadLine();
                    if (input.ToLower() == "r") continue;

                    SelectDiceToKeep(input);
                }
            }

            ChooseCombination(player);
        }

        private void DisplayDice()
        {
            Console.WriteLine("Dice: " + string.Join(", ", diceCup.Dice.Select(d => d.Value)));
        }

        private void SelectDiceToKeep(string input)
        {
            var indicesToKeep = input.Select(c => c - '1').Where(i => i >= 0 && i < diceCup.Dice.Count);

            var newDiceCup = new DiceCup(0);
            foreach (int index in indicesToKeep)
            {
                newDiceCup.Dice.Add(diceCup.Dice[index]);
            }

            for (int i = newDiceCup.Dice.Count; i < 5; i++)
            {
                newDiceCup.Dice.Add(new Die());
            }

            diceCup = newDiceCup;
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
