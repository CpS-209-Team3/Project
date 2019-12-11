//-----------------------------------------------------------
//File:   Player.cs
//Desc:   Holds the code required to run the player ship.
//----------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    // This class is responsible for reading the inputs from
    // the user and controlling the player ship to respond
    // appropriately.
    public class Player : Ship
    {
        // Defines the amount to accelerate the player ship by.
        private const double acceleration = 2000;

        // Reads the inputs from the PlayerController and adds the
        // correct accerlation or attempts to fire the cannon.
        public override void ShipLoop()
        {
            if (World.Instance.PlayerController.Up) AddForce(new Vector(0, -acceleration));
            if (World.Instance.PlayerController.Down) AddForce(new Vector(0, acceleration));
            if (World.Instance.PlayerController.Left) AddForce(new Vector(-acceleration, 0));
            if (World.Instance.PlayerController.Right) AddForce(new Vector(acceleration, 0));

            if (World.Instance.PlayerController.Fire) cannon.Fire();
        }

        // Constructor
        // Checks if the game is in cheat mode, then it sets the player's health accordingly.
        // It also sets Ship.onDeath to World's OnPlayerDeath method.
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
