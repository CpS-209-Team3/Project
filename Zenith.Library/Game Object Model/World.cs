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
        private static List<GameObject> objects;
        public static Random random;

        private static double width;
        private static double height;

        // Properties

        public double Width { get { return width; } }

        public double Height { get { return height; } }

        public static Random Random
        {
            get { return Random; }
        }

        // Methods

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
