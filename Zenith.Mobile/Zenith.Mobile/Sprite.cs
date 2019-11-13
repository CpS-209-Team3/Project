using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Zenith.Library;

namespace Zenith.View
{
    public class Sprite
    {
        Image image;
        Vector position;
        
        public Image Image { get { return image; } }

        public void Update()
        {
            image.Margin = new Thickness(position.X, position.Y, 0, 0);
        }
        
        public Sprite(string src, GameObject followObject)
        {
            image = new Image { Source = src };
            position = followObject.Position;

        }
    }
}
