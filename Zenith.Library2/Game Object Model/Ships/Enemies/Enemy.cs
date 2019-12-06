using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public enum EnemyState
    {
        Sway,
        Ram,
        Flee
    }

    public class Enemy : Ship
    {

        protected EnemyState state;
        protected double swayRadius = 20;
        protected double swayStatus;
        protected int clock = 0;

        public override void ShipLoop() { }


        public Enemy(Vector position)
            : base(position)
        {
            type = GameObjectType.Enemy;
            state = EnemyState.Sway;
            swayStatus = 0;
        }

        


        

    }
}
