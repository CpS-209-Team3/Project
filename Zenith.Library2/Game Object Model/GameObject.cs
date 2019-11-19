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

    public abstract class GameObject : ISerialize
    {
        protected Vector position, velocity, size;
        protected GameObjectType type;
        protected bool collidable;
        

        // Properties

        public Vector Position { get { return position; } }

        public Vector Size { get { return size; } }

        public GameObjectType Type { get { return type; } }

        public bool Collidable { get { return collidable; } }

        public bool Destroy { get; set; }

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

        public virtual string Serialize()
        {
            return type.ToString() + ',' + collidable.ToString() + ',' + position.ToString() + ',' + velocity.ToString() + ',' + size.ToString() + ',' + Destroy.ToString();
        }

        public virtual void Deserialize(string saveInfo)
        {
            // saveInfo includes everything but the gameObjectType
            string[] savedValues = saveInfo.Split(',');
            collidable = Convert.ToBoolean(savedValues[0]);
            string[] xNy1 = savedValues[1].Split(':');
            position = new Vector(Convert.ToDouble(xNy1[0]), Convert.ToDouble(xNy1[1]), false);
            string[] xNy2 = savedValues[2].Split(':');
            velocity = new Vector(Convert.ToDouble(xNy2[0]), Convert.ToDouble(xNy2[1]), false);
            string[] xNy3 = savedValues[3].Split(':');
            size = new Vector(Convert.ToDouble(xNy3[0]), Convert.ToDouble(xNy3[1]), false);
            Destroy = Convert.ToBoolean(savedValues[4]);
        }
    }
}
