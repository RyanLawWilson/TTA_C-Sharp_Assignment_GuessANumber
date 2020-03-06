using System;
using System.Collections.Generic;
using System.Text;

namespace GuessANumber
{
    public class Player
    {
        // For new players
        public Player (string name)
        {
            Name = name;
            time = DateTime.Now;
            ID = string.Format("{0}{1}{2}-{3}{4}{5}", time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);
            Score = 0;
            Win = 0;
            Loss = 0;
            Plays = 0;
            IsPlaying = true;
        }

        // For returning players
        public Player (string name, string id, long totalScore, int win, int loss, int plays)
        {
            Name = name;
            ID = id;
            Score = totalScore;
            Win = win;
            Loss = loss;
            Plays = plays;
            IsPlaying = true;
        }

        private DateTime time;   // Used to create a unique ID based on the time.
        public string ID { get; set; }              // The ID is the time that the player was created.

        public string Name { get; set; }
        public long Score { get; set; }
        public int Guesses = 7;                     // later, make this based on difficulty
        public int Win { get; set; }
        public int Loss { get; set; }
        public int Plays { get; set; }
        public double WinRate { get; set; }
        public double LossRate { get; set; }

        public bool IsPlaying { get; set; }
        public bool Won = false;

        // Show all of the information associated with the player.
        public void ShowAll()
        {
            Fancy.Write(String.Format("\n" +
                 "==================================\n" +
                $"\tName:         {Name}\n" +
                $"\tScore:        {Score}\n" +
                $"\tWins:         {Win}\n" +
                $"\tLosses:       {Loss}\n" +
                $"\tGames Played: {Plays}\n" +
                $"\tWin Rate:     {((double) Win / Plays) * 100:F2}%\n" +
                $"\tLoss Rate:    {((double) Loss / Plays) * 100:F2}%\n" +
                 "==================================\n", 5));
        }
    }
}
