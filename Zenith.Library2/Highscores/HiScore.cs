//---------------------------------------------------------------
//File:   HiScore.cs
//Desc:   Manages a single high score
//---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class HiScore
    {
        //Holds the high score player name
        string name;

        //Holds the high score
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
