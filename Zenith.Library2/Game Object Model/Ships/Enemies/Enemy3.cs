//-----------------------------------------------------------
//File:   Enemy3.cs
//Desc:   This file holds the class dealing with the hardest
//        "small-tier" enemy of the game.
//----------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    // This enemy is a swarming enemy. It will
    // approach the player until it is within
    // a 200 unit radius. It will attempt to 
    // maintain this distance while it constantly
    // fires at the player.
    class Enemy3 : Enemy
    {
        // This method is in charge of maintaining a 200 unit
        // padding between the ship and the player. It aims the ship
        // towards the player and fires as fast as possible.
        public override void ShipLoop()
        {
            var playerOffset = World.Instance.Player.Position - position;
            cannon.Fire();

            if (playerOffset.Magnitude > 200)
            {
                playerOffset.Magnitude = 500;
                AddForce(playerOffset);
            }
            else
            {
                playerOffset.Magnitude = 500;
                AddForce(playerOffset * -1);
            }
            angle = playerOffset.Angle;
        }

        // Constructor
        public Enemy3(Vector position)
            : base(position)
        {
            type = GameObjectType.Enemy3;
            imageSources = new List<string> { Util.GetShipSpriteFolderPath("purple_06.png") };
            imageRotation = 90;
            angle = Math.PI;
            cannon = new BasicCannon(this, 120);
            cannon.ProjectileColor = ProjectileColor.Red;
            worth = 50;
        }
    }
}
