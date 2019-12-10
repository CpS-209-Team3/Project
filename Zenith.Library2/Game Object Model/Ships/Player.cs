using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Player : Ship
    {
        private const double acceleration = 2000;

        public override void ShipLoop()
        {
            if (World.Instance.PlayerController.Up) AddForce(new Vector(0, -acceleration));
            if (World.Instance.PlayerController.Down) AddForce(new Vector(0, acceleration));
            if (World.Instance.PlayerController.Left) AddForce(new Vector(-acceleration, 0));
            if (World.Instance.PlayerController.Right) AddForce(new Vector(acceleration, 0));

            if (World.Instance.PlayerController.Fire) cannon.Fire();
        }

        public Player(Vector position)
            : base(position)
        {
            if (World.Instance.CheatsOn)
            {
                health = 0x7FFFFFFF;
                maxHealth = 0x7FFFFFFF;
            }
            else
            {
                health = 1000;
                maxHealth = 1000;
            }
            
            type = GameObjectType.Player;
            imageSources = new List<string> { 
                Util.GetShipSpriteFolderPath("blue_01.png")
            };
            cannon = new BasicCannon(this, 15);
            worth = 0;

            onDeath = World.Instance.EndGame;
        }
    }
}
