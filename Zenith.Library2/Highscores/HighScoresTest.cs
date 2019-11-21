using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NUnit.Framework;

namespace Zenith.Library.Highscores
{
    [TestFixture]
    class HighScoresTest
    {
        [Test]
        public void AddHighScore_AddInitialValue_SuccessfulAddition()
        {
            HighScores LeaderBoard = new HighScores();
            HiScore score = new HiScore("Splatt", 10000);
            LeaderBoard.AddHighScore(score);
            Assert.IsTrue(LeaderBoard.LeaderList[0].Name == "Splatt");
            Assert.IsTrue(LeaderBoard.LeaderList[0].Score == 10000);
        }

        [Test]
        public void AddHighScore_ReplaceLowestValue_ReplaceLowest()
        {
            HighScores LeaderBoard = new HighScores();
            LeaderBoard.AddHighScore(new HiScore("Splatt", 1));
            LeaderBoard.AddHighScore(new HiScore("Cade", 10000));
            LeaderBoard.AddHighScore(new HiScore("Mon", 30));
            LeaderBoard.AddHighScore(new HiScore("Evans", 50));
            LeaderBoard.AddHighScore(new HiScore("James", 100));
            LeaderBoard.AddHighScore(new HiScore("Gon", 2));
            LeaderBoard.AddHighScore(new HiScore("Zales", 400000));
            LeaderBoard.AddHighScore(new HiScore("Bao", 22020));
            LeaderBoard.AddHighScore(new HiScore("Schaub", 2000));
            LeaderBoard.AddHighScore(new HiScore("Data", 10000));

            LeaderBoard.AddHighScore(new HiScore("Q", 2));

            Assert.IsTrue(LeaderBoard.LeaderList[9].Name == "Q");
            Assert.IsTrue(LeaderBoard.LeaderList[9].Score == 2);

            Assert.IsTrue(LeaderBoard.LeaderList[8].Name == "Gon");
            Assert.IsTrue(LeaderBoard.LeaderList[8].Score == 2);

            Assert.IsTrue(LeaderBoard.LeaderList[0].Name == "Zales");
            Assert.IsTrue(LeaderBoard.LeaderList[0].Score == 400000);

            LeaderBoard.AddHighScore(new HiScore("Dave", 232));

            Assert.IsTrue(LeaderBoard.LeaderList[9].Name == "Gon");
            Assert.IsTrue(LeaderBoard.LeaderList[9].Score == 2);
        }

        [Test]
        public void Save_SaveFileToString_ReturnString()
        {
            HighScores LeaderBoard = new HighScores();
            LeaderBoard.AddHighScore(new HiScore("Splatt", 1));
            LeaderBoard.AddHighScore(new HiScore("Cade", 10000));
            LeaderBoard.AddHighScore(new HiScore("Mon", 30));
            LeaderBoard.AddHighScore(new HiScore("Evans", 50));
            LeaderBoard.AddHighScore(new HiScore("James", 100));
            LeaderBoard.AddHighScore(new HiScore("Gon", 2));
            LeaderBoard.AddHighScore(new HiScore("Zales", 400000));
            LeaderBoard.AddHighScore(new HiScore("Bao", 22020));
            LeaderBoard.AddHighScore(new HiScore("Schaub", 2000));
            LeaderBoard.AddHighScore(new HiScore("Data", 10000));

            LeaderBoard.Save("saveFile.txt");
            using (StreamReader reader = new StreamReader("saveFile.txt"))
            {
                string log = reader.ReadLine();
                Assert.IsTrue(log == "Zales,400000;Bao,22020;Cade,10000;Data,10000;Schaub,2000;James,100;Evans,50;Mon,30;Gon,2;Splatt,1");
            }
        }

        [Test]
        public void Load_LoadObjectFromSave_ReturnObject()
        {
            HighScores savedGame = HighScores.Load("saveFile.txt");

            HighScores LeaderBoard = new HighScores();
            LeaderBoard.AddHighScore(new HiScore("Splatt", 1));
            LeaderBoard.AddHighScore(new HiScore("Cade", 10000));
            LeaderBoard.AddHighScore(new HiScore("Mon", 30));
            LeaderBoard.AddHighScore(new HiScore("Evans", 50));
            LeaderBoard.AddHighScore(new HiScore("James", 100));
            LeaderBoard.AddHighScore(new HiScore("Gon", 2));
            LeaderBoard.AddHighScore(new HiScore("Zales", 400000));
            LeaderBoard.AddHighScore(new HiScore("Bao", 22020));
            LeaderBoard.AddHighScore(new HiScore("Schaub", 2000));
            LeaderBoard.AddHighScore(new HiScore("Data", 10000));

            for (int i = 0; i < 10; ++i)
            {
                Assert.IsTrue(savedGame.LeaderList[i].Name == LeaderBoard.LeaderList[i].Name);
                Assert.IsTrue(savedGame.LeaderList[i].Score == LeaderBoard.LeaderList[i].Score);
            }
        }
    }
}