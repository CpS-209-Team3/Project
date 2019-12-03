using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Zenith.Library.Game_Object_Model;

namespace Zenith.Library
{
    public enum WorldState
    {
        Stage,      // When the player is moving forwards, fighting small enemies and asteroids
        Boss,       // When the player is fighting a boss
        Shop,       // When the player is in a shop
        Pause,      // When the game is paused
    }

    public interface ViewManager
    {
        void AddSprite(GameObject gameObject);
        void RemoveSprite(GameObject gameObject);
    }

    interface ISerialize
    {
        string Serialize();

        void Deserialize(string saveInfo);
    }

    public class World

    {
        // Singleton Code
        private static World instance = new World();
        public static World Instance { get { return instance; } }

        private World()
        {
            gameTick = 0;
            level = 1;
            random = new Random();
            PlayerController = new GameController();
            Width = 500;
            Height = 500;

            objects = new List<GameObject>();
            collisionManager = new CollisionQuad(new Vector(0, 0), new Vector(Width, Height), 0);
            collisionManager.Objects = objects;

            levelManager = new LevelManager(difficulty, level);
        }

        // End of Singleton Code

        // Instance variables

        private List<GameObject> objects;
        public Random random;
        private int gameTick;
        private CollisionQuad collisionManager;
        private string playerName;
        private int level;
        private int score;
        private double deltaTime = 1.0 / 60.0;
        private string directory = null;
        private int collisions = 0;

        private int difficulty = 1;
        private LevelManager levelManager;

        // Properties

        public double Width { get; set; }

        public double Height { get; set; }

        public Random Random { get { return random; } }

        public Ship Player { get; set; }

        public GameController PlayerController { get; set; }

        public ViewManager ViewManager { get; set; }

        public string PlayerName { get { return playerName; } set { playerName = value; } }

        public int Level { get { return level; } set { level = value; } }

        public int Score { get { return score; } set { score = value; } }

        public int GameTick { get { return gameTick; } set { gameTick = value; } }

        public List<GameObject> Objects { get { return objects; } set { objects = value; } }

        public double DeltaTime { get { return deltaTime; } }

        public string Directory { get { return directory; } set { directory = value; } }

        public int Difficulty { get { return difficulty; } set { difficulty = value; } }

        public int Collisions { get { return collisions; } set { collisions = value; } }

        // Methods

        public void Update()
        {
            if (!PlayerController.Pause)
            {
                for (int i = 0; i < objects.Count; ++i)
                {
                    objects[i].Update();

                    if (objects[i].Destroy)
                    {
                        RemoveObject(objects[i]);
                        // fix index after removal
                        --i;
                    }
                }

                collisions = 0;
                collisionManager.CheckForCollisions();

                levelManager.Update();

                ++gameTick;
            }
            
        }

        public void AddObject(GameObject gameObject)
        {
            objects.Add(gameObject);
            ViewManager.AddSprite(gameObject);
        }

        public void RemoveObject(GameObject gameObject)
        {
            objects.Remove(gameObject);
            ViewManager.RemoveSprite(gameObject);
        }

        public void EnableCheatMode()
        {
            Player.Health = 0x7FFFFFFF;
            Player.MaxHealth = 0x7FFFFFFF;
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
                    playerName = reader.ReadLine();
                    gameTick = Convert.ToInt32(reader.ReadLine());
                    level = Convert.ToInt32(reader.ReadLine());
                    score = Convert.ToInt32(reader.ReadLine());
                    while (reader.Peek() != -1)
                    {
                        string saveInfo = reader.ReadLine();
                        string objectType = saveInfo.Substring(0, saveInfo.IndexOf(","));
                        string objectInfo = saveInfo.Substring(saveInfo.IndexOf(",") + 1);
                        GameObject obj = CreateInstanceOf(objectType);
                        obj.Deserialize(objectInfo);
                        //AddObject(obj);
                        objects.Add(obj);
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
                writer.WriteLine(playerName);
                writer.WriteLine(gameTick);
                writer.WriteLine(level);
                writer.WriteLine(score);
                foreach (GameObject obj in this.objects)
                {
                    writer.WriteLine(obj.Serialize());
                }
            }
        }

        // This method resets the instance of world.
        public void Reset()
        {
            playerName = "";
            level = 1;
            score = 0;
            gameTick = 0;

            objects.Clear();

            var p = new Player(new Library.Vector(90, Height / 2));
            AddObject(p);
            Player = p;
            p.Velocity.Cap(0);
        }

        public GameObject CreateInstanceOf(string objectType)
        {
            switch (objectType)
            {
                case "Item":
                    return new Item(null);
                case "Asteroid":
                    return new Asteroid(null, 0);
                case "Laser":
                    return new Laser(null, null, 0, true);
                case "BackgroundElement":
                    return new BackgroundElement(null, 0);
                case "Enemy1":
                    return new Enemy1(null);
                case "Enemy2":
                    return new Enemy2(null);
                case "Enemy3":
                    return new Enemy3(null);
                case "Boss1":
                    return new Boss1(null);
                case "Boss2":
                    return new Boss2(null);
                case "Boss3":
                    return new Boss3(null);
                case "Boss4":
                    return new Boss4(null);
                case "Boss5":
                    return new Boss5(null);
                case "Player":
                    return new Player(null);
                case "HealthBar":
                    return new HealthBar(null);
            }
            return null;
        }

    }
}
