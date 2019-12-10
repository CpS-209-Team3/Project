using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Media;

namespace Zenith.Desktop
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page, ViewManager
    {
        MainWindow main;
        DispatcherTimer timer;
        public bool isCheating = true;
        List<Button> ItemsOnSale;
        public string shipName;
        public int diffNum;
        public Dictionary<string, SoundPlayer> gameSounds;

        private bool loadingGame;
        private string filename;
        public GamePage(MainWindow theMainOne, bool loadingGame, string filename)
        {
            this.loadingGame = loadingGame;
            this.filename = filename;
            ItemsOnSale = new List<Button>();
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

        //~~~~~~~~~~~~~~~~~~~~~~~ Play Sound ~~~~~~~~~~~~~~~~~~~
        public void PlaySound(string key)
        {
            Dispatcher.Invoke(() => { gameSounds[key].Play(); });
        }

        //~~~~~~~~~~~~~~~~ Trigger Endgame ~~~~~~~~~~~~~~~~~~~~
        public void TriggerEndGame(bool isPlayerAlive)
        {
            //timer.Stop();
            HighScores scores = HighScores.Load("highScores.txt");
            HiScore thisScore = new HiScore(World.Instance.PlayerName, World.Instance.Score);
            if (scores.IsNewHighScore(thisScore))
            {
                scores.AddHighScore(thisScore);
                scores.Save("highScores.txt");
                //Put the window that says that you have a new high score here
            }
            else
            {
                //Put the window that says just says the game is over, no high score
            }
        }

        //~~~~~~~~~~~~~~~~~~~~ Game Loop ~~~~~~~~~~~~~~~~~~~~
        public void GameLoop(object sender, EventArgs e)
        {
            World.Instance.Update();
            Dispatcher.Invoke(() =>
            {
                lbl_Popup_Pause_CurrentScore.Text = lbl_CurrentScore.Text = Convert.ToString(World.Instance.Score);
                for (int i = 0; i < sprites.Count; ++i)
                {
                    sprites[i].Update();
                }

                // Health Bar
                progressbar_PlayerHealthBar.Value = (double)World.Instance.Player.Health * 1000 / World.Instance.Player.MaxHealth;

                // Just for fun here, no offense....
                if (lbl_CurrentScore.Text.Contains("2") && lbl_CurrentScore.Text.Contains("0") && lbl_CurrentScore.Text.Contains("9"))
                {
                    lbl_Popup_Shop.Text = "SCHAUB";
                }
                else
                    lbl_Popup_Shop.Text = "SHOP";

                // Input handling
                World.Instance.PlayerController.Up = Keyboard.IsKeyDown(Key.Up);
                World.Instance.PlayerController.Down = Keyboard.IsKeyDown(Key.Down);
                World.Instance.PlayerController.Left = Keyboard.IsKeyDown(Key.Left);
                World.Instance.PlayerController.Right = Keyboard.IsKeyDown(Key.Right);
                World.Instance.PlayerController.Fire = Keyboard.IsKeyDown(Key.Space);
                //World.Instance.PlayerController.Pause = Keyboard.IsKeyDown(Key.P);
                if (Keyboard.IsKeyDown(Key.P)) World.Instance.PlayerController.Pause = true;
                World.Instance.PlayerController.Save = Keyboard.IsKeyDown(Key.S);
                World.Instance.PlayerController.Load = Keyboard.IsKeyDown(Key.L);

                if (World.Instance.PlayerController.Pause == true)
                    Popup_Pause.IsOpen = true;

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
        
            World.Instance.PlayerName = shipName;
            World.Instance.Difficulty = diffNum;

            World.Instance.CreatePlayer();
            lbl_PlayerName.Text = World.Instance.PlayerName;

            World.Instance.StartX = 20;
            World.Instance.StartY = 60;
            World.Instance.EndX = main.Width - 75;
            World.Instance.EndY = main.Height - 75;

            gameSounds = new Dictionary<string, SoundPlayer>();
            gameSounds.Add("Laser", new SoundPlayer(Util.GetSoundFolderPath("laser.wav")));
            gameSounds.Add("Explode", new SoundPlayer(Util.GetSoundFolderPath("smallExplode.wav")));
            foreach (KeyValuePair<string, SoundPlayer> i in gameSounds)
            {
                i.Value.Load();
            }
            if (loadingGame)
            {
                World.Instance.Load(filename);
                loadingGame = false;
            }
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            timer.Tick += GameLoop;
            timer.Start();
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~POPUP_PAUSE EVENT HANDLING~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~ Popup: Pause Load ~~~~~~~~~~~~~~~~~~~~
        private void Popup_Pause_Loaded(object sender, RoutedEventArgs e)
        {
            lbl_Popup_Pause_PlayerName.Text = lbl_PlayerName.Text;
        }
        //~~~~~~~~~~~~~~~~~~~~ Popup: Continue Click ~~~~~~~~~~~~~~~~~~~~
        private void btn_Pause_Continue_Click(object sender, RoutedEventArgs e)
        {
            // Write the `unpause` game here....
            World.Instance.PlayerController.Pause = false;

            // Continue to play game => close the popup and continue the game
            if (Popup_Pause.IsOpen == true)
            {
                Popup_Pause.IsOpen = false;
            }

        }

        //~~~~~~~~~~~~~~~~~~~~ Popup: Save Click ~~~~~~~~~~~~~~~~~~~~
        private void btn_Pause_Save_Click(object sender, RoutedEventArgs e)
        {
            World.Instance.Save(World.Instance.PlayerName + ".txt");
        }

        //~~~~~~~~~~~~~~~~~~~~ Popup: Main Menu Click ~~~~~~~~~~~~~~~~~~~~
        private void btn_Pause_MainMenu_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            //Close popup then go back to Main Menu
            if (Popup_Pause.IsOpen == true)
                Popup_Pause.IsOpen = false;
            //Return to main menu as Back button works.
            main.Content = main.canView;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~END POPUP_PAUSE EVENT HANDLING~~~~~~~~~~~~~~~~~~~~~~~~~

        //~~~~~~~~~~~~~~~~~~~~~~~~~POPUP_NewHighScore EVENT HANDLING~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~ Popup: Close ~~~~~~~~~~~~~~~~~~~~
        private void btn_NewHighScore_Close_Click(object sender, RoutedEventArgs e)
        {
            if (Popup_NewHighScore.IsOpen == true)
                Popup_NewHighScore.IsOpen = false;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~END POPUP_NewHighScore EVENT HANDLING~~~~~~~~~~~~~~~~~~~~~~~~~

        //~~~~~~~~~~~~~~~~~~~~~~~~~POPUP_SHOP EVENT HANDLING (In Development)~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~ Popup: Shop Buy Button ~~~~~~~~~~~~~~~~~~~~
        private void btn_Shop_Buy_Click(object sender, RoutedEventArgs e)
        {

        }
        //~~~~~~~~~~~~~~~~~~~~ Popup: Shop Loaded ~~~~~~~~~~~~~~~~~~~~
        private void Popup_Shop_Loaded(object sender, RoutedEventArgs e)
        {
            // Copy from the Good Old BattleShip
            // Display shelf
            var ItemLines = new StackPanel();
            ItemLines.Orientation = Orientation.Horizontal;
            ItemLines.HorizontalAlignment = HorizontalAlignment.Left;
            layout_Shop_Items.Children.Add(ItemLines);

            //~~~~~~~~~~~~~~~~~~~~ Put items on the shelf ~~~~~~~~~~~~~~~~~~~~
            for (int i = 1; i <= 18; ++i)
            {
                var ShopItem = new Button();

                //~~~~~~~~~~~~~~~~~~~~ How big and color of the place of an item ~~~~~~~~~~~~~~~~~~~~
                ShopItem.Height = 50;
                ShopItem.Width = 50;
                ShopItem.Background = Brushes.Brown;
                ShopItem.Margin = new Thickness(5);
                ShopItem.Click += ShopItem_Click;
                ItemLines.Children.Add(ShopItem);
                ItemsOnSale.Add(ShopItem);

                //~~~~~~~~~~~~~~~~~~~~ The line is full put on new line ~~~~~~~~~~~~~~~~~~~~
                if (i % 6 == 0)
                {
                    ItemLines = new StackPanel();
                    ItemLines.Orientation = Orientation.Horizontal;
                    ItemLines.HorizontalAlignment = HorizontalAlignment.Left;
                    layout_Shop_Items.Children.Add(ItemLines);
                }
            }
        }

        //~~~~~~~~~~~~~~~~~~~~ Popup: Shop Item Click ~~~~~~~~~~~~~~~~~~~~
        private void ShopItem_Click(object sender, RoutedEventArgs e)
        {
            Color bgcolor = Color.FromRgb(150, 111, 51);
            BrushConverter brush = new BrushConverter();
            // My idea just highlight the item as we choose
            // then click buy to confirm to buy it.
            Button ItemChose = sender as Button;
            int i = 0;
            bool chooseThis = false;
            while (chooseThis != true)
            {
                if (ItemsOnSale[i] == ItemChose)
                {
                    for (int n = 0; n < ItemsOnSale.Count; ++n)
                    {
                        ItemsOnSale[n].Background = Brushes.Brown;
                    }
                    ItemsOnSale[i].Background = Brushes.LightBlue;
                    chooseThis = true;
                }
                else
                    ++i;
            }
        }

        //~~~~~~~~~~~~~~~~~~~~ Test Shop View Button ~~~~~~~~~~~~~~~~~~~~
        private void btn_TestShop_Click(object sender, RoutedEventArgs e)
        {
            Popup_Shop.IsOpen = true;
        }

        //~~~~~~~~~~~~~~~~~~~~ Popup: Shop Close Button ~~~~~~~~~~~~~~~~~~~~
        private void btn_Shop_Close_Click(object sender, RoutedEventArgs e)
        {
            Popup_Shop.IsOpen = false;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~END POPUP_SHOP EVENT HANDLING~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~ End Event Handling Zone ~~~~~~~~~~~~~~~~~~~~~~~~~
    }
}
