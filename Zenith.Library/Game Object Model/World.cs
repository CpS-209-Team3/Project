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

        private static Ship player;

        // Properties

        public static double Width { get { return width; } }

        public static double Height { get { return height; } }

        public static Random Random { get { return random; } }

        public static Ship Player { get { return player; } }

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
