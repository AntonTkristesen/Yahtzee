using System;
using System.Linq;
using static System.Console;
using YahtzeeGame;
namespace YahtzeeGame
{
    class Program
    {
        static void Main(string[] args)
        {

            WriteLine("Welcome to Yahtzee!");

            // Create a new instance of the Yahtzee game
            Game game = new Game();

            // Play the game
            game.Play();

            WriteLine("Thanks for playing Yahtzee!");
        }
    }


    public class YahtzeeGame
    {
        public const int NUM_DICE = 5;
        public const int NUM_ROUNDS = 13;
        public const int MAX_REROLLS = 3;
    }
}
