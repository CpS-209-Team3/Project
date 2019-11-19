using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Zenith.Library
{
    interface ViewManager
    {
        void AddSprite();
        void RemoveSprite();
    }

    interface ISerialize
    {
        string Serialize();

        void Deserialize(string saveInfo);
    }

    class World
    {
        // Singleton Code
        private static World instance = new World();
        public static World Instance { get { return instance; } }

        private World()
        {
            gameTick = 0;
            collisionManager = new CollisionManager(objects);
        }

        // End of Singleton Code

        private List<GameObject> objects;
        public Random random;

        private double width;
        private double height;

        private Ship player;

        private int gameTick;
        private CollisionManager collisionManager;

        // Properties

        public double Width { get { return width; } }

        public double Height { get { return height; } }

        public Random Random { get { return random; } }

        public Ship Player { get { return player; } }

        // Methods

        public void Update()
        {
            for (int i = 0; i < objects.Count; ++i)
            {
                objects[i].Update();
                if (objects[i].Destroy)
                {
                    objects.RemoveAt(i);
                    // fixe index after removal
                    --i;
                }
            }

            collisionManager.CheckForCollisions();

            ++gameTick;
        }

        public void AddObject(GameObject gameObject)
        {
            objects.Add(gameObject);
        }

        public void RemoveObject(GameObject gameObject)
        {
            objects.Add(gameObject);
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
                    while (reader.Peek() != 0)
                    {

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
                foreach (object obj in this.objects)
                {
                    //writer.WriteLine(obj.Serialize());
                }
            }
        }

        public object CreateInstanceOf(string objectType)
        {
            switch (objectType)
            {
                case "Generic":
                    break;
                case "Item":
                    break;
                case "Background":
                    break;
                case "Laser":
                    break;
                case "Asteroid":
                    break;
                case "Ship":
                    break;
                case "Enemy1":
                    break;
                case "Enemy2":
                    break;
                case "Enemy3":
                    break;
                case "Boss1":
                    break;

            }

            return "f" as object;
        }

    }
}
