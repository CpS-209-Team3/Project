using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class CollisionQuad
    {
        // Constants
        const int maxTier = 4;
        const int maxObjectCount = 10;

        // Instance Variables

        private Vector origin;
        private Vector size;

        private List<GameObject> objects;
        private CollisionQuad[] quads;
        private int tier;

        // Properties

        public List<GameObject> Objects { get { return objects; } set { objects = value; } }

        // Methods

        private void Split()
        {
            var newSize = size / 2;

            quads[0] = new CollisionQuad(origin, newSize, tier + 1);
            quads[1] = new CollisionQuad(origin + new Vector(0, newSize.Y), newSize, tier + 1);
            quads[2] = new CollisionQuad(origin + new Vector(newSize.X, 0), newSize, tier + 1);
            quads[3] = new CollisionQuad(origin + newSize, newSize, tier + 1);
        }

        private void DivideObjects()
        {
            for (int i = 0; i < objects.Count; ++i)
            {
                // if the object fits to the left of the quad
                if (objects[i].Position.X < origin.X)
                {
                    // quad[0]
                    if (objects[i].Position.Y < origin.Y + size.Y) quads[0].Objects.Add(objects[i]);
                    // quad[1]
                    if (objects[i].Position.Y > origin.Y + size.Y) quads[1].Objects.Add(objects[i]);
                }
                // if the object fits to the right of the quad
                else
                {
                    // quad[2]
                    if (objects[i].Position.Y < origin.Y + size.Y) quads[2].Objects.Add(objects[i]);
                    // quad[3]
                    if (objects[i].Position.Y > origin.Y + size.Y) quads[3].Objects.Add(objects[i]);
                }
            }
        }

        public void CheckForCollisions()
        {
            size = new Vector(World.Instance.Width, World.Instance.Height);

            int objectCount = objects.Count;

            if (objectCount > maxObjectCount && tier < maxTier)
            {
                Split();
                DivideObjects();
                quads[0].CheckForCollisions();
                quads[1].CheckForCollisions();
                quads[2].CheckForCollisions();
                quads[3].CheckForCollisions();
            }
            else
            {
                for (int i = 0; i < objectCount; ++i)
                {
                    for (int j = i + 1; j < objectCount; ++j)
                    {
                        // todo: implement the collision checks here
                        if (objects[i].Position.X - objects[i].Size.X / 2 <= objects[j].Position.X + objects[j].Size.X / 2 &&
                            objects[i].Position.Y - objects[i].Size.Y / 2 <= objects[j].Position.Y + objects[j].Size.Y / 2 &&
                            objects[i].Position.X + objects[i].Size.X / 2 >= objects[j].Position.X - objects[j].Size.X / 2 &&
                            objects[i].Position.Y + objects[i].Size.Y / 2 >= objects[j].Position.Y - objects[j].Size.Y / 2)
                        {
                            objects[i].OnCollision(objects[j]);
                            objects[j].OnCollision(objects[i]);
                        }
                        ++World.Instance.Collisions;
                    }
                }
            }
        }

        public CollisionQuad(Vector origin, Vector size, int tier)
        {
            this.tier = tier;
            this.origin = origin;
            this.size = size;
            objects = new List<GameObject>();
            quads = new CollisionQuad[4];
        }
    }
}
