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

        public override void ShipLoop() { }


        public Enemy(Vector position)
            : base(position)
        {
            type = GameObjectType.Enemy;
            state = EnemyState.Sway;
        }

       /* public void Do(EnemyState state)
        {
            switch(state)
            {
                case "Sway":
                    break;
                case "Ram":

                case 
            }
        }
*/
    }
}
