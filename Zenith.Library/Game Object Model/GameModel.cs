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
            string serializedGameObjects = System.IO.File.ReadAllText(string.Format(@"../../SaveFiles/{0}.txt", filename));
            List<string> gameObjectStrings = serializedGameObjects.Split(',').ToList();
            foreach (string obj in gameObjects)
            {
                gameObjects.Add(obj.Deserialize());
            }
        }

        // This function saves the game as a text file named [filename].txt
        // It does this by first deleting any text files under the same name,
        // then creating a new file under the name [filename], and writing to
        // it per the Serialization Design wiki page.
        public void Save(string filename)
        {
            
            if (File.Exists(filename)) 
            {
                File.Delete(filename);
            }

            using (StreamWriter writer = new StreamWriter(filename, true))
            {
                
            }
            // serialize all necessary objects
            foreach (object x in this.gameObjects) 
            {
                string serializedObject = x.Serialize();
                this.gameObjectStrings.Add(serializedObject);
            }
            System.IO.File.WriteAllLines(@"../../SaveFiles/", gameObjectStrings);
        }

    }
}
