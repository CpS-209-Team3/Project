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

        // Properties

        public Vector Position { get { return position; } }

        public Vector Size { get { return size; } }

        public bool Dynamic { get { return dynamic; } }

        public bool Destroy { get; set; }

        // Methods

        public virtual void OnCollision(GameObject gameOjbect) { }

        public abstract void Loop();

        public void Update()
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
