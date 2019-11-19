using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public interface ViewManager
    {
        void AddSprite(GameObject obj);

        void RemoveSprite(GameObject obj);
    }

    public class World
    {
        // Singleton Code
        private static World instance = new World();
        public static World Instance { get { return instance; } }

        private World()
        {
            gameTick = 0;
            objects = new List<GameObject>();
            collisionManager = new CollisionManager(objects);
        }

        // End of Singleton Code

        private List<GameObject> objects;
        public Random random;
        private int gameTick;
        private CollisionManager collisionManager;

        // Properties

        public double Width { get; set; }

        public double Height { get; set; }

        public Random Random { get { return random; } }

        public Ship Player { get; set; }

        public ViewManager ViewManager { get; set; }

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
            ViewManager.AddSprite(gameObject);
        }

        public void RemoveObject(GameObject gameObject)
        {
            objects.Add(gameObject);
            ViewManager.RemoveSprite(gameObject);
        }
    }
}
