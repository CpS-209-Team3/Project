﻿//-----------------------------------------------------------
//File:   Boss1.cs
//Desc:   This file holds the class responsible for controlling
//        Boss1.
//----------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    // This class controls Boss1. It also initializes the specialized cannon for Boss1
    // and controls the movement for the object as well.
    public class Boss1 : Enemy
    {
        // Fires the cannon as fast as possible (no laser will be fired if
        // the internal state of the cannon is not ready for a laser to be
        // fired). It also updates the movement of Boss1 to allow it to
        // sway back and forth.
        public override void ShipLoop()
        {
            cannon.Fire();
            ++clock;
            double goalY = (Math.Cos((double)clock / 100) + 1) / 2 * World.Instance.EndY - position.Y;
            AddForce(new Vector(0, goalY) * 100);
            var offset = new Vector(World.Instance.EndX * 0.75 - position.X, 0);
            AddForce(offset);

            angle = (World.Instance.Player.Position - position).Angle;
        }

        // Constructor
        public Boss1(Vector position)
            : base(position)
        {
            imageSources = new List<string> { Util.GetShipSpriteFolderPath("large_grey_01.png") };
            angle = Math.PI;
            type = GameObjectType.Boss1;
            size = new Vector(256, 256);
            health = 4000;
            maxHealth = 4000;
            mass = 400;
            cannon = new Boss1Cannon(this);
            worth = 100;
        }
    }
}
