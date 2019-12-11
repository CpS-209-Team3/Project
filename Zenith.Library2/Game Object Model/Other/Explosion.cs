//-----------------------------------------------------------
//File:   .cs
//Desc:   
//----------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Explosion : GameObject
    {
        int clock = 0;

        public override void Loop()
        {
            imageIndex = 7;
            if (imageIndex == 109)
            {
                destroy = true;
            }
            ++clock;
        }

        public Explosion(Vector position)
            : base(position)
        {
            imageSources = new List<string>
            {
                Util.GetShipSpriteFolderPath("Explosion\\explosion-01.png"),
                Util.GetShipSpriteFolderPath("Explosion\\explosion-02.png"),
                Util.GetShipSpriteFolderPath("Explosion\\explosion-03.png"),
                Util.GetShipSpriteFolderPath("Explosion\\explosion-04.png"),
                Util.GetShipSpriteFolderPath("Explosion\\explosion-05.png"),
                Util.GetShipSpriteFolderPath("Explosion\\explosion-06.png"),
                Util.GetShipSpriteFolderPath("Explosion\\explosion-07.png"),
                Util.GetShipSpriteFolderPath("Explosion\\explosion-08.png"),
                Util.GetShipSpriteFolderPath("Explosion\\explosion-09.png"),
                Util.GetShipSpriteFolderPath("Explosion\\explosion-10.png"),
                Util.GetShipSpriteFolderPath("Explosion\\explosion-11.png")
            };

        }
    }
}
