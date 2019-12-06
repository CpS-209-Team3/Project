using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Boss5 : Enemy
    {
        private const double spinSpeed = 0.5;

        private Sensor sensor;
        private Vector avoid = new Vector(0, 0);
        private int nextDamageMarker;

        private Vector goal = new Vector(0, 0);
        private int minionsLeft = 0;

        public override void OnCollision(GameObject gameObject)
        {
            if (state != EnemyState.Sway && gameObject is Laser)
            {
                var laser = (Laser)gameObject;
                if (laser.IsFromPlayer)
                {
                    var velocity = laser.Position - position;
                    velocity.Magnitude = 800;
                    var newLaser = new Laser(laser.Position, velocity, 40, false);
                    newLaser.ImageSources.Clear();
                    newLaser.ImageSources.Add(Util.GetShipSpriteFolderPath("Projectiles\\projectile-red.png"));
                    World.Instance.AddObject(newLaser);
                }
            }
            else
            {
                base.OnCollision(gameObject);
            }
        }

        public void OnMinionDeath()
        {
            --minionsLeft;
            if (minionsLeft == 0)
            {
                state = EnemyState.Sway;
                nextDamageMarker = ((health - 1) / 1000) * 1000;
            }
        }

        public override void ShipLoop()
        {
            switch (state)
            {
                case EnemyState.Sway:
                    if (avoid.Magnitude > 1)
                    {
                        AddForce(avoid);
                        avoid /= 2;
                    }
                    else MoveTo(new Vector(World.Instance.EndX * 0.75, World.Instance.EndY / 2), 10);

                    angle = (World.Instance.Player.Position - position).Angle;
                    cannon.Fire();

                    if (health <= nextDamageMarker)
                    {
                        state = EnemyState.Ram;
                        clock = 0;
                    }
                    break;
                case EnemyState.Ram:
                    angle += spinSpeed;
                    MoveTo(World.Instance.Player.Position, 10);
                    ++clock;
                    if (clock >= 600)
                    {
                        state = EnemyState.Flee;
                        goal = new Vector(World.Instance.EndX, World.Instance.EndY / 2);
                    }
                    break;
                case EnemyState.Flee:
                    angle += spinSpeed;
                    MoveTo(goal, 10);
                    if ((goal - position).Magnitude < 100)
                    {
                        state = EnemyState.Pause;
                        clock = 0;
                        minionsLeft = 10;
                    }
                    break;
                case EnemyState.Pause:
                    angle += spinSpeed;
                    if (clock < 300 && clock % 30 == 0)
                    {
                        double angle = Math.PI * (World.Instance.Random.NextDouble() - 0.5) + Math.PI;
                        double x = Math.Cos(angle) * 100;
                        double y = Math.Sin(angle) * 100;
                        var enemy = new Enemy2(position - new Vector(x, y));
                        enemy.OnDeath = OnMinionDeath;
                        World.Instance.AddObject(enemy);
                    }
                    ++clock;
                    break;
            }

        }

        public void OnSense(GameObject gameObject)
        {
            if (gameObject is Laser)
            {
                var laser = (Laser)gameObject;
                if (laser.IsFromPlayer)
                {
                    var offset = position - laser.Position;
                    double dist = offset.Magnitude;
                    offset.Magnitude = 1400000000;
                    avoid = offset / (dist * dist);

                    if (avoid.Y < 0 && position.Y < 50)
                    {
                        avoid.Y = 100 * mass;
                    }
                    if (avoid.Y > 0 && position.Y > World.Instance.EndY - 50)
                    {
                        avoid.Y = -100 * mass;
                    }

                }
            }
        }

        public Boss5(Vector position)
            : base(position)
        {
            type = GameObjectType.Boss5;
            imageSources = new List<string> { Util.GetShipSpriteFolderPath("large_green_01.png") };
            size = new Vector(256, 256);
            mass = 400;
            health = 4000;
            maxHealth = 4000;

            nextDamageMarker = ((health / 1000) - 1) * 1000;
            state = EnemyState.Sway;

            cannon = new Boss2Cannon(this);

            sensor = new Sensor(this, OnSense, 200);

            worth = 500;
        }
    }
}
