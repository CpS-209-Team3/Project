using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    enum GameObjectType
    {
        Player,
        Enemy,
        Item,
        Asteroid,
        Laser,
        Background
    }

    abstract class GameObject
    {
        protected Vector position, velocity, size;
        protected GameObjectType type;
        protected bool dynamic;

        public Vector Position { get { return position; } }

        public Vector Size { get { return size; } }

        public bool Dynamic { get { return dynamic; } }

        public abstract void Loop();

        public void update()
        {
            position += velocity;

            Loop();
        }


        public GameObject(Vector position)
        {
            this.position = position;
            velocity = new Vector(0, 0);
            size = new Vector(0, 0);
        }
    }
}
