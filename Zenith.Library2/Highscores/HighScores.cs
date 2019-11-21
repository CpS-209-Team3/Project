using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Zenith.Library.Highscores
{
    class HighScores
    {
        public List<HiScore> LeaderList { get; set; }

        public HighScores()
        {
            LeaderList = new List<HiScore>();
        }

        //Given a HiScore object, check if it is high enough to place in the total high scores.
        //Returns true if the score is high enough (Will use Compare())
        public bool IsNewHighScore(HiScore hiScore)
        {
            if (LeaderList.Count <= 9)
            {
                return true;
            }
            else if(Compare(hiScore, LeaderList[9]) >= 0)
            {
                return true;
            }
            return false;
        }

        //Removes the lowest score in the list (if list is already full), adds the new score, and resorts the list based on HiScore.Score
        public void AddHighScore(HiScore hiScore)
        {
            if (IsNewHighScore(hiScore))
            {
                if(LeaderList.Count >= 10)
                {
                    LeaderList.RemoveAt(9);
                }
                LeaderList.Add(hiScore);
                LeaderList.Sort((x,y) => Compare(y,x));
            }
        }

        //Compares two scores to determine if one is greater
        //Returns false if scoreOne is less than scoreTwo, otherwise returns true (Ties will return true)
        public int Compare(HiScore newScore, HiScore lowestScore)
        {
            if(newScore.Score > lowestScore.Score)
            {
                return 1;
            }
            else if(newScore.Score < lowestScore.Score)
            {
                return -1;
            }
            return 0;
        }

        //Returns a complete HighScores object based on the provided save file
        public static HighScores Load(string saveFile)
        {
            using (StreamReader reader = new StreamReader(saveFile))
            {
                HighScores h = new HighScores();
                string line = reader.ReadLine();
                string[] data = line.Split(';');
                for(int i = 0; i < data.Length; ++i)
                {
                    string[] couple = data[i].Split(',');
                    h.AddHighScore(new HiScore(couple[0], Convert.ToInt32(couple[1])));
                }
                return h;
            }
        }

        //Returns a complete HighScores object based on the provided save file
        //Each entry will be in the form name,score; with no spaces
        public void Save(string fileName)
        {
            string data = "";
            for(int i = 0; i < LeaderList.Count; ++i)
            {
                data += LeaderList[i].Name + "," + LeaderList[i].Score + ";";
            }
            data = data.Remove(data.Length - 1);
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(data);
            }
        }
    }
}
