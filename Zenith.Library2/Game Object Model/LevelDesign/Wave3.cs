using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library.Game_Object_Model.LevelDesign
{
    class Wave3
    {
        public Wave3(int difficulty, int level, Vector startingPos)
        {
            for (int i = 0; i < difficulty + level; i++)
            {
                World.Instance.AddObject(new Enemy3(startingPos));
            }
        }
    }
}
