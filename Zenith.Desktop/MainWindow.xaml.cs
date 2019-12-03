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
    public partial class MainWindow : Window, ViewManager
    {
        DispatcherTimer timer;
        bool isCheating = false;

        public MainWindow()
        {
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
                canView.Children.Add(s);
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
                        canView.Children.Remove(s);
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
                World.Instance.PlayerController.Pause = Keyboard.IsKeyToggled(Key.P);

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

            World.Instance.Width = Width;
            World.Instance.Height = Height;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            timer.Tick += GameLoop;
            timer.Start();
        }

        //~~~~~~~~~~~~~~~~~~~~ Help Screen ~~~~~~~~~~~~~~~~~~~~
        private void btn_Help_Click(object sender, RoutedEventArgs e)
        {
            HelpScreen help = new HelpScreen(this);
            this.Content = help;
        }

        //~~~~~~~~~~~~~~~~~~~~ High Score Page ~~~~~~~~~~~~~~~~~~~~
        private void btn_HighScore_Click(object sender, RoutedEventArgs e)
        {
            HighScorePage highscore = new HighScorePage(this);
            this.Content = highscore;
        }

        //~~~~~~~~~~~~~~~~~~~~ Load Game ~~~~~~~~~~~~~~~~~~~~
        private void btn_Load_Click(object sender, RoutedEventArgs e)
        {
            GamePage game = new GamePage(this);
            this.Content = game;
        }

        //~~~~~~~~~~~~~~~~~~~~ Play Game ~~~~~~~~~~~~~~~~~~~~
        private void btn_Play_Click(object sender, RoutedEventArgs e)
        {
            OptionPage option = new OptionPage(this);
            this.Content = option;
        }

        //~~~~~~~~~~~~~~~~~~~~ Credits Page ~~~~~~~~~~~~~~~~~~~~
        private void btn_Credits_Click(object sender, RoutedEventArgs e)
        {
            CreditsPage credits = new CreditsPage(this);
            this.Content = credits;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~ End Event Handling ~~~~~~~~~~~~~~~~~~~~~~~~~
    }
}
