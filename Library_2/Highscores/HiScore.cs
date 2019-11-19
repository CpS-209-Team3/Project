using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library.Highscores
{
    class HiScore
    {
        string name;
        int score;
        
        public HiScore(string nme, int scr)
        {
            name = nme;
            score = scr;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
    }
}
