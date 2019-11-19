using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public enum GameObjectType
    {
        Generic,
        Ship,
        Item,
        Asteroid,
        Laser,
        Background
    }

    public abstract class GameObject
    {
        protected Vector position, velocity, size;
        protected GameObjectType type;
        protected bool dynamic;
        protected string imageSource;

        // Properties

        public Vector Position { get { return position; } }

        public Vector Size { get { return size; } }

        public GameObjectType Type { get { return type; } }

        public bool Dynamic { get { return dynamic; } }

        public bool Destroy { get; set; }

        public string ImageSource { get { return imageSource; } }

        // Methods

        public virtual void OnCollision(GameObject gameObject) { }

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
            type = GameObjectType.Generic;
        }
    }
}
