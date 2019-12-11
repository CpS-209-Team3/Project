//-----------------------------------------------------------
//File:   .cs
//Desc:   
//----------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library.Game_Object_Model.Other.Projectiles
{
    class Missile : GameObject
    {
        GameObject target;

        public override void Loop()
        {
            angle = (target.Position - position).Angle;
            position += target.Position - position;
        }

        public Missile(Vector position, GameObject target)
            : base(position)
        {
            imageSources = new List<string>() { "" };
            this.target = target;
        }
    }
}
