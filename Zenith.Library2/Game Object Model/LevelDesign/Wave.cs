﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public abstract class Wave
    {
        protected int difficulty;
        protected int level;
        protected int waveCount = 0;
        protected Vector startingPos;
        protected double size;

        public int WaveCount { get { return waveCount; } set { waveCount = value; } }

        public List<Enemy> enemies;


        public virtual void Spawn()
        {

        }
        public Wave()
        {
            this.difficulty = World.Instance.Difficulty;
            this.level = World.Instance.Level;
        }

        public void AddEnemy(Ship type)
        {
            type.OnDeath = World.Instance.DeathAction;
            waveCount++;
            World.Instance.AddObject(type);
        }
    }
}
