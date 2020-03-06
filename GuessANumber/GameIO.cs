using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace GuessANumber
{
    // This class is used to 
    public static class GameIO
    {
        public static string GameFile = @"C:\Users\Ryan Wilson\guess_a_game.txt";           // The file path that the user information will be on.
        public static string AssignmentFile = @"C:\Users\Ryan Wilson\assignment.txt";       // The file that is used to complete the assignment just in case
        public static string Path = @"C:\Users\Ryan Wilson\";                               // The path with no file referenced.

        // Seperates the file base on , to isolate each player.  Look through all players and find the one with the player's name.
        public static string[] GetPlayerData(string playerName)
        {
            string file = File.ReadAllText(GameIO.GameFile);            // Read the file
            string[] allPlayers = file.Split(",");                      // Get all of the players that have played the game.

            // Possible Feature: Check if there players with the same name.  Give the player the option to chose which profile is theirs.
            string playerDataString = allPlayers.Where(x => x.Contains(playerName)).First();       // Find the specific player data based on name.

            return playerDataString.Split("|");
        }

        // Removes the player record from the file then adds the updated record.
        public static void UpdateGameFile(GuessANumberPlayer player)
        {
            // Format of each playerDataString: NAME|ID|SCORE|WIN|LOSS|PLAYS|DIFF

            string file = File.ReadAllText(GameIO.GameFile);
            List<string> allPlayers = file.Split(",").ToList();

            // Remove the specific player from the
            allPlayers.Remove(allPlayers.Where(x => x.Contains(player.ID)).First());

            allPlayers.Add("{}");

            // Put the delimeter back
            for (int i = 0; i < allPlayers.Count; i++)
            {
                allPlayers[i] += ",";
            }
        }
    }
}
