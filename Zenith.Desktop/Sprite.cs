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
        private double currentAngle;

        public GameObject GameObject { get { return gameObject; } }

        public void Update()
        {
            if (currentAngle != gameObject.ImageRotation)
            {
                RenderTransform = new RotateTransform(gameObject.ImageRotation);
                currentAngle = gameObject.ImageRotation;
            }

            var offset = gameObject.Position - gameObject.Size * 0.5;

            Margin = new Thickness(offset.X, offset.Y, 0, 0);
        }

        public Sprite(GameObject gameObject)
        {
            this.gameObject = gameObject;

            RenderTransform = new RotateTransform(gameObject.ImageRotation);

            // Source: https://stackoverflow.com/questions/13034201/wpf-rotate-image-around-center
            RenderTransformOrigin = new Point(0.5, 0.5);

            currentAngle = gameObject.ImageRotation;

            Source = new BitmapImage(new Uri(gameObject.ImageSource, UriKind.Absolute));
        }
    }
}
