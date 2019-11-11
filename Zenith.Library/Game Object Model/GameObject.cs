using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library.Game_Object_Model
{
    abstract class GameObject
    {
        protected Vector position, velocity;


        public GameObject()
        {
            position = new Vector(0, 0);
            velocity = new Vector(0, 0);
        }
    }
}
