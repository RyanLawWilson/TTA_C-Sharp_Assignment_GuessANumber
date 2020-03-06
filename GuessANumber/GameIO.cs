using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuessANumber
{
    // This class is used to 
    public static class GameIO
    {
        public static string GameFile = @"C:\Users\Ryan Wilson\guess_a_game.txt";           // The file path that the user information will be on.
        public static string AssignmentFile = @"C:\Users\Ryan Wilson\assignment.txt";       // The file that is used to complete the assignment just in case
        public static string Path = @"C:\Users\Ryan Wilson\";                               // The path with no file referenced.

        // Seperates the file base on , to isolate each player.  Look through all players and find the one with the player's name.
        public static string[] GetPlayerData(string playerName, out bool newPlayer)
        {
            // Format of each player entry: NAME|ID|SCORE|WIN|LOSS|PLAYS|DIFF
            // Each player is seperated by comma: NAME|ID|SCORE|WIN|LOSS|PLAYS|DIFF,NAME|ID|SCORE|WIN|LOSS|PLAYS|DIFF,NAME|ID|SCORE|WIN|LOSS|PLAYS|DIFF

            string file = File.ReadAllText(GameIO.GameFile);            // Read the file
            string[] allPlayers = file.Split(",");                      // Get all of the players that have played the game.

            // If we get an invalid operation, we are dealing with a new player.  Return null
            try
            {
                string playerDataString = allPlayers.Where(x => x.Contains(playerName)).First();       // Find the specific player data based on name.
                newPlayer = false;
                return playerDataString.Split("|");
            }
            catch (InvalidOperationException)
            {
                newPlayer = true;
            }

            return null;
        }

        // Saves the game by removing the player's data from the old file and adding the player back to the file
        public static void SaveGame(Player player, Difficulty diff)
        {
            // Format of each playerDataString: NAME|ID|SCORE|WIN|LOSS|PLAYS|DIFF

            string oldFile = File.ReadAllText(GameFile);                            // Read the game file
            List<string> allPlayers = oldFile.Split(",").ToList();                  // List all of the players
            allPlayers.RemoveAll(x => x.Equals(""));                                // Get rid of any blank cells from the split method

            allPlayers.Remove(allPlayers.Where(x => x.Contains(player.ID)).First());    // Remove the specific player from the file based on ID.  Removes old player info.

            allPlayers.Add($"{player.Name}|{player.ID}|{player.Score}|{player.Win}|{player.Loss}|{player.Plays}|{diff}");       // Add the player back to the file

            StringBuilder newFile = new StringBuilder();

            // Put the , delimeter back
            for (int i = 0; i < allPlayers.Count; i++)
            {
                allPlayers[i] += ",";
                newFile.Append(allPlayers[i]);
            }

            File.WriteAllText(GameFile, newFile.ToString());

            Fancy.Write("\n=== Data saved ===\n", 100, color: ConsoleColor.Green);
        }

        // Adds the player to the game file
        public static void AddPlayerToFile(Player player, Difficulty diff)
        {
            string oldFile = File.ReadAllText(GameFile);
            string newFile = string.Format($"{oldFile}{player.Name}|{player.ID}|{player.Score}|{player.Win}|{player.Loss}|{player.Plays}|{diff},");
            File.WriteAllText(GameFile, newFile);
        }
    }
}
