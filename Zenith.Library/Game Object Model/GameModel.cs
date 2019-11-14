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
        
        // Reads a list of strings from the file specifed by filename and puts them into the list
        // of game object strings, then depending on the type of the string given by the first comma
        // seperated value, it will create a different object, deserialize the rest of the information
        // and add it to game objects.
        public void Load(string filename)
        {
            if (File.Exists(filename)) 
            {
                 using (StreamReader reader = new StreamReader(filename, true))
                 {
                     while (reader.peek() != null) 
                     {
                        this.gameObjectStrings.Add(x);
                     }
                     foreach (string x in gameObjectStrings)
                     {
                         string type = x.Substring(0, x.IndexOf(","))
                         switch (type)
                         {
                             case "enemy1":
                                // create new object
                                 Enemy1 enemy1 = new Enemy1():
                                 enemy1.Deserialize(x);
                                 GameObjects.Add(x);
                             case "starship:
                                 // create
                                 Starship s = new Starship();
                                 s.Deserialize(x);
                                 GameObjects.Add(x);
                             ...
                         }
                     }
                     
                     
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
