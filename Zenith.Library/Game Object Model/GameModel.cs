using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace Zenith.Library
{
    interface ISerialization
    {
        void Serialize();

        void Deserialize();
    }

    class GameModel
    {
        private GameModel[] savedGames = new GameModel[5];
        private string gameName;
        private List<object> gameObjects;
        private List<string> gameObjectStrings;

        public GameModel(string name)
        {
            this.gameName = name;
        }

        public string GameName
        {
            get { return gameName; }
            set { this.gameName = value; }
        }

        public List<object> GameObjects
        {
            get { return this.gameObjects; }
        }
        public void Load(string filename)
        {
            if (File.Exists(filename)) 
            {
                 using (StreamReader reader = new StreamReader(filename, true))
                 {
                     
                 }
            }
            
        }

        // This function saves the game as a text file named [filename].txt
        // It does this by first deleting any text files under the same name,
        // creating a new file under the name [filename], and then writing 
        // the serialized version of all the game objects to the file and 
        // closing it.
        public void Save(string filename)
        {
            if (File.Exists(filename)) 
            {
                File.Delete(filename);
            }

            using (StreamWriter writer = new StreamWriter(filename, true))
            {
                foreach (object x in this.gameObjects) 
                {
                    writer.WriteLine(x.Serialize());
                }
                
            }
        }

    }
}
