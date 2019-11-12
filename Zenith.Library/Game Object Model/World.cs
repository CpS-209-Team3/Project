using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class World
    {
        private static List<GameObject> objects;
        public static Random random;

        public static Random Random
        {
            get { return Random; }
        }

        public static void AddObject(GameObject gameObject)
        {
            objects.Add(gameObject);
        }

        public static void RemoveObject(GameObject gameObject)
        {
            objects.Add(gameObject);
        }
    }
}
