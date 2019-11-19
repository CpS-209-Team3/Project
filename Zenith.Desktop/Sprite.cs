using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Zenith.Library;

namespace Zenith.Desktop
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
            Source = new BitmapImage(new Uri(gameObject.ImageSource, UriKind.Absolute));
        }
    }
}
