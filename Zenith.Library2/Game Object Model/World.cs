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
            EndX = 500;
            EndY = 500;
            StartX = 0;
            StartY = 0;

            objects = new List<GameObject>();
            collisionManager = new CollisionQuad(new Vector(StartX, StartY), new Vector(Width, Height), 0);
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

        public double Width { get { return EndX - StartX; } }

        public double Height { get { return EndY - StartY; } }

        public double EndX { get; set; }

        public double EndY { get; set; }

        public double StartX { get; set; }

        public double StartY { get; set; }

        public double MidX { get { return (EndX - StartX) / 2; } }

        public double MidY { get { return (EndY - StartY) / 2; } }

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

        public LevelManager LevelManager { get { return levelManager; } }

        // Methods

        public void SetScreenDimensions(double startX, double startY, double endX, double endY)
        {
            StartX = startX;
            StartY = startY;
            EndX = endX;
            EndY = endY;
            // collisionManager = new CollisionQuad(new Vector(StartX, StartY), new Vector(Width, Height), 0);
        }

        public Vector GetScreenPosition(double x, double y)
        {
            return new Vector((EndX - StartX) * x, (EndY - StartY) * y);
        }

        public void Update()
        {
            if (PlayerController.Save) Save("test.txt");
            
            if (PlayerController.Load)
            {
                Reset();
                Load("test.txt");
            }

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

        // Spawns a boss with a valid ID. Mainly used for debugging purposes.
        public Ship SpawnBoss(int bossID)
        {
            Ship boss = null;
            var startingPosition = new Vector(EndX, EndY / 2);

            switch (bossID)
            {
                case 1:
                    boss = new Boss1(startingPosition);
                    break;
                case 2:
                    boss = new Boss2(startingPosition);
                    break;
                case 3:
                    boss = new Boss3(startingPosition);
                    break;
                case 4:
                    boss = new Boss4(startingPosition);
                    break;
                case 5:
                    boss = new Boss5(startingPosition);
                    break;
            }
            return boss;
            //if (boss != null) AddObject(boss);
        }

        public void CreatePlayer()
        {
            var p = new Player(new Library.Vector(90, EndY / 2));
            AddObject(p);
            Player = p;
            p.Velocity.Cap(0);
            EnableCheatMode();
        }

        // This method resets the instance of world.
        public void Reset()
        {
            playerName = "";
            level = 1;
            score = 0;
            gameTick = 0;

            for (int i = objects.Count - 1; i > 0; i--)
            {
                objects[i].Destroy = true;
                //RemoveObject(objects[i]);
            }

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
                        AddObject(obj);
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
                    if (!(obj is HealthBar))
                    {
                        writer.WriteLine(obj.Serialize());
                    }
                    
                }
            }
        }

        public GameObject CreateInstanceOf(string objectType)
        {
            Vector tempVector = new Vector(1, 1, false);
            switch (objectType)
            {
                case "Item":
                    return new Item(tempVector);
                case "Asteroid":
                    return new Asteroid(tempVector, 0);
                case "Laser":
                    return new Laser(tempVector, tempVector, 0, true);
                case "BackgroundElement":
                    return new BackgroundElement(tempVector, 0);
                case "Enemy1":
                    return new Enemy1(tempVector);
                case "Enemy2":
                    return new Enemy2(tempVector);
                case "Enemy3":
                    return new Enemy3(tempVector);
                case "Boss1":
                    return new Boss1(tempVector);
                case "Boss2":
                    return new Boss2(tempVector);
                case "Boss3":
                    return new Boss3(tempVector);
                case "Boss4":
                    return new Boss4(tempVector);
                case "Boss5":
                    return new Boss5(tempVector);
                case "Player":
                    return new Player(tempVector);
                case "HealthBar":
                    return new HealthBar(null);
            }
            return null;
        }

    }
}
