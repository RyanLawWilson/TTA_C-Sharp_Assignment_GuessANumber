using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GuessANumber
{
    public class GuessANumber
    {
        // You need 1 player to play this game
        public GuessANumber(Player player, Difficulty diff)
        {
            Player = player;
            Diff = diff;
            MaxGuess = (int)Diff;       // Maximum guess based on the Difficulty enum's underlying value
        }

        private Random rand = new Random();
        private int CorrectNumber;
        public int MinGuess = 1;        // The smallest number to guess will never be smaller than 1
        public int MaxGuess { get; set; }

        public Player Player { get; set; }
        public Difficulty Diff { get; set; }

        public void Play()
        {
            // Reset Variables
            Player.Guesses = 7;
            Player.Won = false;

            // Add difficulty multiplier later
            CorrectNumber = rand.Next(MinGuess, MaxGuess);
            Console.WriteLine(CorrectNumber);

            // As long as the player has guesses, guess.
            while (Player.Guesses > 0 && !Player.Won)
            {
                // Determine if the guess is actually valid.
                bool invalidGuess = true;
                while (invalidGuess)
                {
                    Fancy.Write($"\t({Player.Guesses} left) Guess a number: ");
                    string guess = Fancy.ReadLine(ConsoleColor.Green).ToLower();
                    EvaluateGuess(guess, out invalidGuess);
                }
            }

            if (Player.Won)
            {
                Fancy.Write("\nWell done!  ", afterPause: 500);

                Player.Won = true;
                Player.Win++;
                Player.Plays++;

                int scoreGained = (Player.Guesses + 1) * (int)Diff;
                Fancy.Write("You got " + scoreGained + " points!  Here are your stats:\n", color: ConsoleColor.Green);
                Player.Score += scoreGained;
            }
            else
            {
                Fancy.Write("\nYou lost.  It happens to the best of us....\n", afterPause: 1000);

                Player.Won = false;
                Player.Loss++;
                Player.Plays++;
            }

            Player.ShowAll();

            GameIO.SaveGame(Player, Diff);            // Save the game.

            Fancy.Write("\nDo you want to play again? (y or n) ");
            string answer = Fancy.ReadLine(ConsoleColor.Cyan);
            if (answer != "y")
            {
                Player.IsPlaying = false;
            }
        }

        // Checks the user input against a bunch of if statements and redirects to the method that is needed
        public void EvaluateGuess(string guess, out bool invalidGuess)
        {
            if (guess == "rules")
            {
                invalidGuess = false;
                throw new NotImplementedException();
            }
            else if (guess == "settings")
            {
                invalidGuess = false;
                throw new NotImplementedException();
            }
            else                // The guess is a number
            {
                try
                {
                    HighOrLow(Convert.ToInt32(guess));
                    invalidGuess = false;
                }
                catch (FormatException)
                {
                    Fancy.Write("\tYou need to guess a number...\n", 30, afterPause: 250, color: ConsoleColor.Yellow);
                    invalidGuess = true;
                }
            }
        }

        // If the user's input is valid, determine if the number is too high or too low.
        public void HighOrLow(int guess)
        {
            // If the guess is in bounds, take a guess away.
            if (guess >= MinGuess && guess <= MaxGuess)
            {
                Player.Guesses--;

                if (guess == CorrectNumber)
                {
                    Fancy.Write(
                      "\n  *******************\n" +
                        "        You Win!\n" +
                        "  *******************\n", 5, color: ConsoleColor.Magenta);
                    Player.Won = true;
                }
                else if (guess > CorrectNumber) Fancy.Write("\tToo High\n", 80);
                else if (guess < CorrectNumber) Fancy.Write("\tToo Low\n", 80);
            }
            else Fancy.Write($"\tOut of bounds!  Your guess should be between {MinGuess} and {MaxGuess}\n");
        }
    }
}
