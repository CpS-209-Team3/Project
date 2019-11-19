using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library.Highscores
{
    class HighScores
    {
        public List<HiScore> LeaderList { get; set; }

        public HighScores()
        {

        }

        //Given a HiScore object, check if it is high enough to place in the total high scores.
        //Returns true if the score is high enough (Will use Compare())
        public bool IsNewHighScore(HiScore hiScore)
        {
            return true;
        }

        //Removes the lowest score in the list (if list is already full), adds the new score, and resorts the list based on HiScore.Score
        public void AddHighScore(HiScore hiScore)
        {
             
        }

        //Compares two scores to determine if one is greater
        //Returns false if scoreOne is less than scoreTwo, otherwise returns true (Ties will return true)
        public bool Compare(HiScore newScore, HiScore lowestScore)
        {
            return true;
        }

        //Returns a complete HighScores object based on the provided save file
        public static HighScores Load(string saveFile)
        {
            return new HighScores();
        }

        //Returns a complete HighScores object based on the provided save file
        public string Save()
        {
            return "This string would probably return random garbage if it were an actual save file.";
        }
    }
}
