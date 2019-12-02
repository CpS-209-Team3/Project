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
    // Source: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/layouts/contentview
    class Sprite : ContentView
    {
        private GameObject gameObject;
        private double currentAngle;
        private Image[] images;
        private int currentIndex = 0;

        public GameObject GameObject { get { return gameObject; } }

        public void Update()
        {
            if (currentIndex != gameObject.ImageIndex)
            {
                currentIndex = gameObject.ImageIndex;
                Content = images[currentIndex];
            }

            if (currentAngle != gameObject.Angle)
            {
                Rotation = gameObject.Angle * 180 / Math.PI + gameObject.ImageRotation;
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

            // Source: https://github.com/xamarin/docs-archive/tree/master/Recipes/xamarin-forms/Controls/rotation
            Rotation = gameObject.Angle * 180 / Math.PI + gameObject.ImageRotation;

            // Source: https://github.com/xamarin/docs-archive/tree/master/Recipes/xamarin-forms/Controls/rotation
            AnchorX = 0.5;
            AnchorY = 0.5;

            // Source: https://stackoverflow.com/questions/19302061/resize-image-in-xaml-without-losing-quality
            // RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.NearestNeighbor);

            currentAngle = gameObject.Angle;

            try
            {
                for (int i = 0; i < gameObject.ImageSources.Length; ++i)
                {
                    images[i] = new Image();
                    images[i].Source = gameObject.ImageSources[i];
                    images[i].WidthRequest = gameObject.Size.X;
                    images[i].HeightRequest = gameObject.Size.Y;
                }
            }
            catch (Exception e)
            {
                // MessageBox.Show("Error retrieving image for " + gameObject.Type.ToString());
            }

            Content = images[0];
        }
    }
}
