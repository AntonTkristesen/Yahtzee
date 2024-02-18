namespace Yahtzee
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Yahtzee!");

            // Ask for the number of players
            Console.Write("Enter number of players: ");
            int playerCount = int.Parse(Console.ReadLine());

            var playerNames = new List<string>();
            for (int i = 0; i < playerCount; i++)
            {
                Console.Write($"Enter name for player {i + 1}: ");
                playerNames.Add(Console.ReadLine());
            }

            Game game = new Game(playerNames);
            game.Start();
        }
    }
}
