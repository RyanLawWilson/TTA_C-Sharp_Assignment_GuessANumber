using System;
using System.Collections.Generic;
using System.Text;

namespace GuessANumber
{
    public class Player
    {
        // New player
        public Player(string name)
        {
            Name = name;
            Score = 0;
            IsPlaying = true;
            Guesses = 7;        // later, make this based on difficulty
            ID = string.Format("{0}{1}{2}-{3}{4}{5}", time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);
        }

        // Keep the player's information from previous games
        public Player (string name, string id, long totalScore, int win, int loss, int plays)
        {
            Name = name;
            ID = id;
            Score = totalScore;
            Win = win;
            Loss = loss;
            Plays = plays;
            IsPlaying = true;
            Guesses = 7;        // later, make this based on difficulty
        }

        private DateTime time = new DateTime();     // Used to create a unique ID based on the time.
        public string ID { get; set; }             // The ID is the time that the player was created.

        public string Name { get; set; }
        public long Score { get; set; }
        public int Guesses { get; set; }
        public int Win { get; set; }
        public int Loss { get; set; }
        public int Plays { get; set; }
        public double WinRate { get; set; }
        public double LossRate { get; set; }

        public bool IsPlaying { get; set; }
        public bool Won = false;

        // Show the player's win/loss rate
        public void ShowWLR()
        {
            Fancy.Write("");
        }

        // Show all of the information associated with the player.
        public void ShowAll()
        {

        }
    }
}
