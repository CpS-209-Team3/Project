//-----------------------------------------------------------
//File:   World.cs
//Desc:   Contrains the class that controls all of Zenith.
//----------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Zenith.Library.Game_Object_Model;

namespace Zenith.Library
{
    public interface ViewManager
    {
        void AddSprite(GameObject gameObject);
        void RemoveSprite(GameObject gameObject);
        void PlaySound(string key);
        void TriggerEndGame();
    }

    interface ISerialize
    {
        string Serialize();

        void Deserialize(string saveInfo);
    }

    // This class is responsible for controlling and managing all of the Game Model
    // logic.
    public class World
    {
        // Singleton Code

        // A singletone instance of World
        private static World instance = new World();

        public static World Instance { get { return instance; } }

        // Constructor
        private World()
        {
            gameTick = 0;
            level = 1;
            difficulty = 1;
            random = new Random();
            PlayerController = new GameController();
            EndX = 500;
            EndY = 500;
            StartX = 0;
            StartY = 0;

            objects = new List<GameObject>();
            collisionManager = new CollisionQuad(new Vector(StartX, StartY), new Vector(Width, Height), 0);
            collisionManager.Objects = objects;

            levelManager = new LevelManager();

            EndGame = () => ViewManager.TriggerEndGame();
        }

        // End of Singleton Code

        // Instance variables

        // A list of GameObjects that are active in Zenith
        private List<GameObject> objects;

        // Random object for all code in Zenith to use
        public Random random;

        // The amount of times Update() was called
        private int gameTick;

        // The base quadtree branch that handles collision
        private CollisionQuad collisionManager;

        // The name of the player
        private string playerName;

        // The current level the player is on
        private int level;

        // The score the player has currently
        private int score;

        // The precalculated time that will pass between each call to Update()
        private double deltaTime = 1.0 / 60.0;

        // The director of the executable (this is used to located images for sprites)
        private string directory = null;

        // This is a debugging variables that is used to count the number of collisions
        // that took place
        private int collisions = 0;

        // The difficulty of the game
        private int difficulty = 1;

        // The instance that is handling the different levels of the game
        private LevelManager levelManager;

        // Specifies whether cheat mode is on
        private bool cheatsOn = false;
        private bool gameOver = false;

        private int currentWave = 1;
        private int enemiesLeftInWave = 0;
        private Action endGame;

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

        public bool CheatsOn { get { return cheatsOn; } }

        public bool GameOver { get { return gameOver; } set { gameOver = value; } }

        public int CurrentWave { get { return currentWave; } set { currentWave = value; } }

        public int EnemiesLeftInWave { get { return enemiesLeftInWave; } set { enemiesLeftInWave = value; } }
        public Action EndGame { get { return endGame; } set { endGame = value; } }

        // Methods

        // Sets the screen dimensions to the values given
        public void SetScreenDimensions(double startX, double startY, double endX, double endY)
        {
            StartX = startX;
            StartY = startY;
            EndX = endX;
            EndY = endY;
            // collisionManager = new CollisionQuad(new Vector(StartX, StartY), new Vector(Width, Height), 0);
        }

        // Maps the absolute position on a physical screen to the virtual position
        // according to World's dimensions.
        public Vector GetScreenPosition(double x, double y)
        {
            return new Vector((EndX - StartX) * x, (EndY - StartY) * y);
        }

        // Called every 1.60 of a second. It reacts to a player's call
        // to save and load. When the game is not paused, all GameObjects
        // will be updates and collision checks will take place. The
        // LevelManager is also called in this method.
        public void Update()
        {
            if (PlayerController.Save) Save(playerName + ".txt");

            if (PlayerController.Load)
            {
                Load(playerName + ".txt");
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

        // This method is called when the player dies.
        public void OnPlayerDeath()
        {
            ViewManager.TriggerEndGame();
        }

        // This method is called when Boss5 is defeated
        public void OnGameFinish()
        {
            World.Instance.Score += (54000 - gameTick);
            ViewManager.TriggerEndGame();
        }

        // This method adds an object to the GameObject list
        // and adds an accompanying Sprite.
        public void AddObject(GameObject gameObject)
        {
            objects.Add(gameObject);
            ViewManager.AddSprite(gameObject);
        }

        // Removes an object from the GameObject list
        // and removes an accompanying Sprite.
        public void RemoveObject(GameObject gameObject)
        {
            objects.Remove(gameObject);
            ViewManager.RemoveSprite(gameObject);
        }

        // Turns cheat mode on.
        public void EnableCheatMode()
        {
            cheatsOn = true;
        }

        // Turns cheat mode off.
        public void DisableCheatMode()
        {
            cheatsOn = false;
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
        }

        public void CreatePlayer()
        {
            var p = new Player(new Library.Vector(90, EndY / 2));
            AddObject(p);
            Player = p;
            p.Velocity.Cap(0);
            p.OnDeath = EndGame;
        }

        // This method resets the instance of World.
        public void Reset()
        {
            playerName = "";
            level = 1;
            score = 0;
            gameTick = 0;
            currentWave = 1;
            enemiesLeftInWave = 0;

            for (int i = objects.Count - 1; i > 0; i--)
            {
                objects[i].Destroy = true;
            }
        }

        // Reads a list of strings from the file specifed by filename and puts them into the list
        // of game object strings, then depending on the type of the string given by the first comma
        // seperated value, it will create a different object, deserialize the rest of the information
        // and add it to game objects.
        public void Load(string filename)
        {
            Reset();

            if (File.Exists(filename))
            {
                using (StreamReader reader = new StreamReader(filename, true))
                {
                    playerName = reader.ReadLine();
                    gameTick = Convert.ToInt32(reader.ReadLine());
                    level = Convert.ToInt32(reader.ReadLine());
                    score = Convert.ToInt32(reader.ReadLine());
                    currentWave = Convert.ToInt32(reader.ReadLine());
                    enemiesLeftInWave = Convert.ToInt32(reader.ReadLine());

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
                foreach (GameObject obj in objects)
                {
                    if (obj is Boss5)
                    {
                        Boss5 b = obj as Boss5;
                        b.OnDeath = EndGame;
                    }
                    else if (obj is Enemy)
                    {
                        Enemy e = obj as Enemy;
                        e.OnDeath = LevelManager.CurrentWave.DeathAction;
                    }
                    else if (obj is Player)
                    {
                        Player p = obj as Player;
                        p.OnDeath = EndGame;
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
                writer.WriteLine(currentWave);
                writer.WriteLine(LevelManager.CurrentWave.WaveCount);
                foreach (GameObject obj in this.objects)
                {
                    if (!(obj is HealthBar))
                    {
                        writer.WriteLine(obj.Serialize());
                    }

                }
            }
        }

        // ???
        public GameObject CreateInstanceOf(string objectType)
        {
            Vector tempVector = new Vector(1, 1, false);
            switch (objectType)
            {
                case "Item":
                    //return new Item(tempVector);
                case "Asteroid":
                    return new Asteroid(tempVector, 0);
                case "Laser":
                    return new Laser(tempVector, tempVector, 0, true);
                case "BackgroundElement":
                    // return new BackgroundElement(tempVector, 0);
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
