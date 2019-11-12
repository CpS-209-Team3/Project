using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class CollisionManager
    {
        private List<GameObject> objects;
        private bool didSplit;
        private CollisionManager[] quads;

        private void Split()
        {
            didSplit = true;
            // todo: create new quadtrees and split the objects into those
        }

        public void CheckForCollisions()
        {
            int objectCount = objects.Count;

            if (objectCount > 50)
            {
                Split();
            }

            for (int i = 0; i < objectCount; ++i)
            {
                for (int j = i + 1; j < objectCount; ++j)
                {
                    // todo: implement the collision checks here
                }
            }
        }

        public CollisionManager(List<GameObject> objects)
        {
            this.objects = objects;
        }
    }
}
