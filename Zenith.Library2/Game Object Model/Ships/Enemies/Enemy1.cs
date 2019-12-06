﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Enemy1 : Enemy
    {
        public override void ShipLoop() {
            
            cannon.Fire();
            MoveTo(World.Instance.Player.Position, 10);
        }

        public Enemy1(Vector position)
           : base(position)
        {
            type = GameObjectType.Enemy1;

            imageSources = new List<string> { Util.GetShipSpriteFolderPath("tankbase_01.png") };
            angle = Math.PI;
            velocity.X = -50;
            this.position.X = World.Instance.Width;
            cannon = new BasicCannon(this, 300);
            swayRadius = 10;
            cannon.ProjectileColor = ProjectileColor.Red;
            worth = 30;
        }
    }
}
