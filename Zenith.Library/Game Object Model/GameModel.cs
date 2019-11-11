using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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

        public void Save(string filename)
        {
            
            // serialize all necessary objects
            foreach(object x in this.gameObjects) 
            {
                string serializedObject = x.Serialize();
                this.gameObjectStrings.Add(serializedObject);
            }
            System.IO.File.WriteAllLines(@"../../SaveFiles/", gameObjectStrings);
        }

    }
}
