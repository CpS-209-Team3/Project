using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Zenith.Library;

namespace Zenith.Desktop
{
    class Sprite : Image
    {
        private GameObject gameObject;
        
        public GameObject GameObject { get { return gameObject; } }

        public void Update()
        {
            var offset = gameObject.Position - gameObject.Size * 0.5;
            Margin = new Thickness(offset.X, offset.Y, 0, 0);
        }

        public Sprite(GameObject gameObject)
        {
            this.gameObject = gameObject;

            RenderTransform = new RotateTransform(gameObject.ImageRotation);

            Source = new BitmapImage(new Uri(gameObject.ImageSource, UriKind.Absolute));
        }
    }
}
