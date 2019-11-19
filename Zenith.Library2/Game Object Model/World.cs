using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    interface ViewManager
    {
        void AddSprite();

        void RemoveSprite();
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
    }
}
