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
    class Sprite : Label
    {
        private GameObject gameObject;
        private double currentAngle;
        private Image[] images;
        private int lastImageIndex = 0;

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
            // Load images for sprite
            images = new Image[gameObject.ImageSources.Length];

            this.gameObject = gameObject;

            RenderTransform = new RotateTransform(gameObject.Angle * 180 / Math.PI + gameObject.ImageRotation);

            // Source: https://stackoverflow.com/questions/13034201/wpf-rotate-image-around-center
            RenderTransformOrigin = new Point(0.5, 0.5);

            // Source: https://stackoverflow.com/questions/19302061/resize-image-in-xaml-without-losing-quality
            RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.NearestNeighbor);

            currentAngle = gameObject.Angle;

            try
            {
                for (int i = 0; i < gameObject.ImageSources.Length; ++i)
                {
                    images[i] = new Image();
                    images[i].Source = new BitmapImage(new Uri(gameObject.ImageSources[i], UriKind.Absolute));
                    images[i].Width = gameObject.Size.X;
                    images[i].Height = gameObject.Size.Y;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Error retrieving image for " + gameObject.Type.ToString());
            }

            Content = images[0];
        }
    }
}
