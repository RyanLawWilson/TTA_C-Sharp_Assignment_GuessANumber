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

            string[] playerDataArray;       // Will store the information from saved game file.

            Player player;

            // If there is already a text file, find the player's information 
            int difficulty;                 // The number representation of the difficulty
            Difficulty difficultyName;      // The actual difficulty

            // Determine if this player is new or has played before.  If the player is a returning player, get the player's data from the game file.
            bool newPlayer;
            if (File.Exists(GameIO.GameFile)) playerDataArray = GameIO.GetPlayerData(playerName, out newPlayer);  // Get the data relating to that player from the game file.
            else
            {
                playerDataArray = null;
                newPlayer = true;
            }

            // If the player is not new load their information from the game file.
            if (!newPlayer)
            {                
                Fancy.Write("Welcome back, " + playerDataArray[0], 50, afterPause: 500, color: ConsoleColor.Magenta);
                try
                {
                    Fancy.Write($"\nYour previous Difficulty: {playerDataArray[6]} | Choose Game Difficulty (1-4): ", 20);

                    // If the player has a save game, instantiate the information.
                    player = new Player(
                        name:       playerDataArray[0],                         
                        id:         playerDataArray[1],                         
                        totalScore: Convert.ToInt64(playerDataArray[2]),        
                        win:        Convert.ToInt32(playerDataArray[3]),        
                        loss:       Convert.ToInt32(playerDataArray[4]),        
                        plays:      Convert.ToInt32(playerDataArray[5]));       
                }
                catch (IndexOutOfRangeException)
                {
                    Fancy.Write(
                        ".  You closed the program prematurely: no saved data.\n" +
                        "To save your game history, be sure to exit the program as intended.\n", afterPause: 500, color: ConsoleColor.Yellow);
                    Fancy.Write("Choose Game Difficulty: ", 20);
                    player = new Player(playerName);
                }
                catch (Exception ex)
                {
                    Fancy.Write($"\n\n\tI don't know what went wrong!!\n\t{ex.GetType()}\n\t{ex.Message}\n\n");
                    player = new Player(playerName);
                }

                difficulty = AskForDifficulty();
                difficultyName = SetDifficulty(difficulty);
            }
            else                // No text file? make one with the player's information.
            {
                player = new Player(playerName);

                Fancy.Write("What difficulty do you want to play? (1, 2, 3, 4): ", 20);
                difficulty = AskForDifficulty();
                difficultyName = SetDifficulty(difficulty);

                GameIO.AddPlayerToFile(player, difficultyName);

                Tutorial();
            }

            BackUpAssignment(difficulty);

            GuessANumber game = new GuessANumber(player, difficultyName);

            // Game Loop, stops when the player doesn't want to play anymore.
            while (player.IsPlaying) game.Play();

            Fancy.Write("\n\nThanks for playing!", 75, color: ConsoleColor.Green);
            Console.Read();
        }

        // This method fulfills step 2 and step 3 of the assignment on Page 218.
        // Writes the user input to file and then prints the text back from the file.
        public static void BackUpAssignment(int difficulty)
        {
            // This little bit satifies the assignment (I think)
            Fancy.Write("\n\nFor the sake of the assignment, Writing your number to a file...  ", 40, afterPause: 750);
            File.WriteAllText(GameIO.AssignmentFile, difficulty.ToString());
            Fancy.Write("Done\n", $"Printing the contents of the file below:\n{File.ReadAllText(GameIO.AssignmentFile)}", 20, 25, pause: 250, afterPause: 250);

            Fancy.Write("\n\nNow...", " time to put way too much work into this assignment!\n\n", pause: 500, afterPause: 500);
        }

        // Peice of code that returns the difficulty that the player chooses.
        public static int AskForDifficulty()
        {
            try
            {
                int difficulty = Convert.ToInt32(Fancy.ReadLine(ConsoleColor.Red));
                if (difficulty <= 0 || difficulty >= 5) throw new FormatException();

                Fancy.Write("Going with " + SetDifficulty(difficulty) + " difficulty");
                return difficulty;
            }
            catch (FormatException)
            {
                int difficulty = 2;
                Fancy.Write("That is not a valid difficulty.  I'll just set you at " + SetDifficulty(difficulty) + "\n", afterPause: 500);
                return difficulty;
            }
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
            Fancy.Write("\n\n\tThe objective of this game is to guess the number I am thinking of.\n\t", 35, afterPause: 1000);
            // Fancy.Write("You can change the difficulty to increase your score for winning.\n\t", 35, afterPause: 750);
            // Fancy.Write("To do so, type 'settings' whenever you are prompted to type a number.\n", 35, afterPause: 750);
        }
    }
}
