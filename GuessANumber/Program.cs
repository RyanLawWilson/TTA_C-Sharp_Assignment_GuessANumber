using System;
using System.IO;
using System.Linq;

namespace GuessANumber
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the player's name
            Fancy.Write("Welcome to Guess a Game.","  What is your name: ", 15, 30, pause: 500);
            string playerName = Console.ReadLine();

            string[] playerDataArray;
            // If there is already a text file, find the player's information 
            if (File.Exists(GameIO.GameFile))
            {
                // Format of each player entry: NAME|ID|SCORE|WIN|LOSS|PLAYS|DIFF
                playerDataArray = GameIO.GetPlayerData(playerName);         // Get the data relating to that player.

                Fancy.Write("Welcome back, " + playerDataArray[0] + "\n", 50, afterPause: 500, color: ConsoleColor.Magenta);

                Fancy.Write($"Your previous Difficulty: {playerDataArray[6]} | Difficulty: ", 20);
            }
            else                // No text file? make one with the player's information.
            {
                File.WriteAllText(GameIO.GameFile, playerName + "|");

                Fancy.Write("What difficulty do you want to play? (1, 2, 3, 4): ", 20);

                Tutorial();
            }

            int difficulty = 2;     // Default
            try
            {
                difficulty = Convert.ToInt32(Fancy.ReadLine(ConsoleColor.Red));
                if (difficulty <= 0 || difficulty >= 5) throw new FormatException();

                Fancy.Write("Going with " + SetDifficulty(difficulty) + " difficulty");
            }
            catch (FormatException)
            {
                Fancy.Write("That is not a valid difficulty.  I'll just set you at " + SetDifficulty(difficulty) + "\n", afterPause: 500);
            }

            // This little bit satifies the assignment (I think)
            Fancy.Write("\n\nFor the sake of the assignment, Writing your number to a file...  ", 50, afterPause: 750);
            File.WriteAllText(GameIO.AssignmentFile, difficulty.ToString());
            Fancy.Write("Done\n",$"Printing the contents of the file below:\n{File.ReadAllText(GameIO.AssignmentFile)}", 20, 25, pause: 250, afterPause: 250);

            Fancy.Write("\n\nNow..."," time to put way too much work into this assignment!\n\n", pause: 500, afterPause: 500);

            GuessANumberPlayer player = new GuessANumberPlayer(playerName);

            GuessANumberGame game = new GuessANumberGame(player, SetDifficulty(difficulty));

            if (player.IsPlaying)
            {
                game.Play();
            }

            Fancy.Write("Thanks for playing!", 75, color: ConsoleColor.Green);
            Console.Read();
        }

        // Get's the difficutly enum based on the integer that the user typed in.
        public static Difficulty SetDifficulty(int difficulty)
        {
            switch (difficulty)
            {
                case 1:
                    return Difficulty.Easy;
                case 2:
                    return Difficulty.Normal;
                case 3:
                    return Difficulty.Heroic;
                case 4:
                    return Difficulty.Legendary;
            }

            return Difficulty.Normal;
        }

        // Just some text that shows the tutorial.
        public static void Tutorial()
        {
            Fancy.Write("\n\tThe objective of this game is to guess the number I am thinking of.\n\t", 35, afterPause: 750);
            Fancy.Write("You can change the difficulty to increase your score for winning.\n\t", 35, afterPause: 750);
            Fancy.Write("To do so, type 'settings' whenever you are prompted to type a number.\n\n", 35, afterPause: 750);
        }
    }
}
