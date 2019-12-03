using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Zenith.Library;

namespace Zenith.Desktop
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page, ViewManager
    {
        MainWindow main;
        DispatcherTimer timer;
        bool isCheating = false;
        public GamePage(MainWindow theMainOne)
        {
            main = theMainOne;
            InitializeComponent();
        }

        List<Sprite> sprites;

        //~~~~~~~~~~~~~~~~~~~~~~~~~ Method Zone ~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~ Add Sprite ~~~~~~~~~~~~~~~~~~~~
        public void AddSprite(GameObject obj)
        {
            Dispatcher.Invoke(() =>
            {
                var s = new Sprite(obj);
                sprites.Add(s);
                canGame.Children.Add(s);
            });
        }

        //~~~~~~~~~~~~~~~~~~~~ Remove Sprite ~~~~~~~~~~~~~~~~~~~~
        public void RemoveSprite(GameObject obj)
        {
            Dispatcher.Invoke(() =>
            {
                foreach (var s in sprites)
                {
                    if (s.GameObject == obj)
                    {
                        sprites.Remove(s);
                        canGame.Children.Remove(s);
                        break;
                    }
                }
            });
        }

        //~~~~~~~~~~~~~~~~~~~~ Game Loop ~~~~~~~~~~~~~~~~~~~~
        public void GameLoop(object sender, EventArgs e)
        {
            World.Instance.Update();

            Dispatcher.Invoke(() =>
            {
                for (int i = 0; i < sprites.Count; ++i)
                {
                    sprites[i].Update();
                }

                // Input handling
                World.Instance.PlayerController.Up = Keyboard.IsKeyDown(Key.Up);
                World.Instance.PlayerController.Down = Keyboard.IsKeyDown(Key.Down);
                World.Instance.PlayerController.Left = Keyboard.IsKeyDown(Key.Left);
                World.Instance.PlayerController.Right = Keyboard.IsKeyDown(Key.Right);
                World.Instance.PlayerController.Fire = Keyboard.IsKeyDown(Key.Space);

                int potentialCollisions = World.Instance.Objects.Count;
                potentialCollisions = (potentialCollisions * potentialCollisions - potentialCollisions) / 2;
                //txtTest.Text = World.Instance.Collisions.ToString() + '/' + potentialCollisions;
            });
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~ End Method Zone ~~~~~~~~~~~~~~~~~~~~~~~~~

        //~~~~~~~~~~~~~~~~~~~~~~~~~ Event Handling Zone ~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~ Window Loaded ~~~~~~~~~~~~~~~~~~~~
        private void Window_Loaded(object sender, RoutedEventArgs ev)
        {
            World.Instance.Directory = Directory.GetCurrentDirectory();
            sprites = new List<Sprite>();
            World.Instance.ViewManager = this;
            World.Instance.Reset();
            if (isCheating) World.Instance.EnableCheatMode();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            timer.Tick += GameLoop;
            timer.Start();
        }

        //~~~~~~~~~~~~~~~~~~~~ Popup: Continue Click ~~~~~~~~~~~~~~~~~~~~
        private void btn_Continue_Click(object sender, RoutedEventArgs e)
        {
            // Continue to play game => close the popup and continue the game
            if (StandardPopup.IsOpen == true)
                StandardPopup.IsOpen = false;

            // Write the `unpause` game here....
        }

        //~~~~~~~~~~~~~~~~~~~~ Popup: Save Click ~~~~~~~~~~~~~~~~~~~~
        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            //----------------------------------------------------------------//
            // I don't know how to make the game Save or Load...              //
            // Please help me. Thank you very much.                           //
            //----------------------------------------------------------------//
        }

        //~~~~~~~~~~~~~~~~~~~~ Popup: Main Menu Click ~~~~~~~~~~~~~~~~~~~~
        private void btn_MainMenu_Click(object sender, RoutedEventArgs e)
        {
            //Return to main menu as Back button works.
            main.Content = main.canView;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~ End Event Handling Zone ~~~~~~~~~~~~~~~~~~~~~~~~~
    }
}
