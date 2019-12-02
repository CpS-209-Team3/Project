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
    public partial class GamePage : ContentPage, ViewManager
    {
        bool isCheating = false;

        public GamePage()
        {
            InitializeComponent();
        }

        List<Sprite> sprites;

        //~~~~~~~~~~~~~~~~~~~~~~~~~ Method Zone ~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~ Add Sprite ~~~~~~~~~~~~~~~~~~~~
        public void AddSprite(GameObject obj)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var s = new Sprite(obj);
                sprites.Add(s);
                gameGrid.Children.Add(s);
            });
        }

        //~~~~~~~~~~~~~~~~~~~~ Remove Sprite ~~~~~~~~~~~~~~~~~~~~
        public void RemoveSprite(GameObject obj)
        {
            Device.BeginInvokeOnMainThread(() =>
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
            });
        }

        //~~~~~~~~~~~~~~~~~~~~ Game Loop ~~~~~~~~~~~~~~~~~~~~
        public void GameLoop(object sender, EventArgs e)
        {
            World.Instance.Update();

            Device.BeginInvokeOnMainThread(() =>
            {
                for (int i = 0; i < sprites.Count; ++i)
                {
                    sprites[i].Update();
                }

                // Input handling
                //World.Instance.PlayerController.Up = Keyboard.IsKeyDown(Key.Up);
                //World.Instance.PlayerController.Down = Keyboard.IsKeyDown(Key.Down);
                //World.Instance.PlayerController.Left = Keyboard.IsKeyDown(Key.Left);
                //World.Instance.PlayerController.Right = Keyboard.IsKeyDown(Key.Right);
                //World.Instance.PlayerController.Fire = Keyboard.IsKeyDown(Key.Space);

                int potentialCollisions = World.Instance.Objects.Count;
                potentialCollisions = (potentialCollisions * potentialCollisions - potentialCollisions) / 2;
                //txtTest.Text = World.Instance.Collisions.ToString() + '/' + potentialCollisions;
            });
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~ End Method Zone ~~~~~~~~~~~~~~~~~~~~~~~~~

        //~~~~~~~~~~~~~~~~~~~~~~~~~ Event Handling Zone ~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~ Window Loaded ~~~~~~~~~~~~~~~~~~~~
        private void ContentPage_Appearing(object sender, EventArgs ev)
        {
            //World.Instance.Directory = Directory.GetCurrentDirectory();
            sprites = new List<Sprite>();
            World.Instance.ViewManager = this;
            World.Instance.Reset();
            if (isCheating) World.Instance.EnableCheatMode();

            World.Instance.Width = Width;
            World.Instance.Height = Height;


            TimeSpan time = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            Device.StartTimer(time, () => 
            {
                return true;
            });
        }
    }
}