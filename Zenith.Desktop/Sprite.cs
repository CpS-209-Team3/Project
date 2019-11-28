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
        private Image[] healthBar = { new Image() };

        public GameObject GameObject { get { return gameObject; } }

        public void Update()
        {
            if (currentAngle != gameObject.Angle)
            {
                RenderTransform = new RotateTransform(gameObject.Angle * 180 / Math.PI + gameObject.ImageRotation);
                currentAngle = gameObject.Angle;
            }

            var offset = gameObject.Position - (gameObject.Size / 2);
            //var offset = gameObject.Position;

            Margin = new Thickness(offset.X, offset.Y, 0, 0);
        }

        public Sprite(GameObject gameObject)
        {
            this.gameObject = gameObject;

            RenderTransform = new RotateTransform(gameObject.Angle * 180 / Math.PI + gameObject.ImageRotation);

            // Source: https://stackoverflow.com/questions/13034201/wpf-rotate-image-around-center
            RenderTransformOrigin = new Point(0.5, 0.5);

            currentAngle = gameObject.Angle;

            Width = gameObject.Size.X;
            Height = gameObject.Size.Y;

            try
            {
                Source = new BitmapImage(new Uri(gameObject.ImageSource, UriKind.Absolute));
            }
            catch(Exception e)
            {
                MessageBox.Show("Error retrieving image for " + gameObject.Type.ToString());
            }
        }
    }
}
