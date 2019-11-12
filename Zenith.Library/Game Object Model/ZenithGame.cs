using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class ZenithGame
    {
        private List<GameObject> objects;
        private int gameTick;
        private CollisionManager collisionManager;

        public List<GameObject> Objects { get { return objects; } }

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

        public ZenithGame()
        {
            objects = new List<GameObject>();
            gameTick = 0;
            collisionManager = new CollisionManager(objects);
        }
    }
}
