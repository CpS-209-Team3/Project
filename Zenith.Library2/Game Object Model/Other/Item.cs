using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Item : GameObject
    {
        public enum shopItems
        { 
            Selling, 
            Sold
        }
        public override void Loop() { }

        public Item(Vector position)
            : base(position)
        {

        }

        protected int startMoney = 0;
    }
}
