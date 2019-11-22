//This Sprite Class is for the Xamarin Files!!!
//
//
//
//
//
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xamarin.Forms;
using Zenith.Library;

namespace Zenith.View
{
    class Sprite : Image
    {
        GameObject gameObject;
        
        public GameObject GameObject { get { return gameObject; } }

        public void Update()
        {
            Margin = new Thickness(gameObject.Position.X, gameObject.Position.Y, 0, 0);
        }

        public Sprite(GameObject gameObject)
        {
            this.gameObject = gameObject;
            Source = gameObject.ImageSource;
        }
    }
}
