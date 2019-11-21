using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Library;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zenith.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        public GamePage()
        {
            InitializeComponent();
        }

        List<Sprite> sprites;

        public void AddSprite(GameObject obj)
        {
            var s = new Sprite(obj);
            sprites.Add(s);
            gameGrid.Children.Add(s);
        }

        public void RemoveSprite(GameObject obj)
        {
            foreach (var s in sprites)
            {
                if (s.GameObject == obj)
                {
                    sprites.Remove(s);
                    gameGrid.Children.Remove(s);
                    break;
                }
            }
        }

        public void GameLoop()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    World.Instance.Update();
                    ///////////////////////////////////////////////////////////////////////////e.Update();

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        for (int i = 0; i < sprites.Count; ++i)
                        {
                            sprites[i].Update();
                        }
                    });
                    Task.Delay(1000 / 60);
                }
            });
        }

        //private void Window_Loaded(object sender, RoutedEventArgs ev)
        //{
        //    sprites = new List<Sprite>();
        //    World.Instance.ViewManager = this;

        //    /*var i = new BitmapImage(new Uri(Util.GetImagePath("blue_01.png"), UriKind.Absolute));
            
        //    var img = new Image();
        //    img.Source = i;
        //    canView.Children.Add(img);
        //    */
        //    var txt = new Entry();
        //    txt.Text = Util.GetImagePath("blue_01.png");
        //    gameGrid.Children.Add(txt);

        //    e = new Enemy1(new Library.Vector(70, 70));
        //    AddSprite(e);
        //    e.Update();
        //    GameLoop();
        //}
    }
}