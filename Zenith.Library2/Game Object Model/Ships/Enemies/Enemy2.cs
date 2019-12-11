//-----------------------------------------------------------
//File:   Enemy2.cs
//Desc:   Holds the class that controls the enemy with
//        medium difficulty.
//----------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    // This enemy is the mid-difficulty type. It acts like a guard
    // by initially choosing a position to guard at. If the player
    // comes to close, then the ship will back away.
    class Enemy2 : Enemy
    {
        // The preferred position to take a stance as when the
        // player is not within 500 units.
        Vector guardPosition;

        // This fires at the player as fast as possible.
        // If the player comes within a 500 unit radius of the ship,
        // then the enemy will flee the player. Once the player is out
        // of that range, then the enemy ship will resume its
        // guarding position.
        public override void ShipLoop()
        {
            cannon.Fire();
            var playerOffset = World.Instance.Player.Position - position;
            angle = playerOffset.Angle;

            if (playerOffset.Magnitude < 500)
            {
                playerOffset.Magnitude = 500;
                AddForce(playerOffset * -1);
            }
            else
            {
                MoveTo(guardPosition, 10);
            }
        }

        // Constructor
        public Enemy2(Vector position)
           : base(position)
        {
            imageSources = new List<string> { Util.GetShipSpriteFolderPath("darkgrey_01.png") };
            type = GameObjectType.Enemy2;
            double x = (World.Instance.Random.NextDouble() * World.Instance.EndX / 2) + World.Instance.EndX / 2;
            double y = World.Instance.Random.NextDouble() * World.Instance.EndY;
            guardPosition = new Vector(x, y);
            cannon = new BasicCannon(this, 200);
            cannon.ProjectileColor = ProjectileColor.Red;
            worth = 40;
        }
    }
}
