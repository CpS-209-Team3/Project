//---------------------------------------------------------------
//File:   GameWindow.xaml.cs
//Desc:   Manages View Components of Mobile Gameplay in Xamarin
//---------------------------------------------------------------

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

    // Manages the entire mobile gameplay view
    public partial class GamePage : ContentPage, ViewManager
    {

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~ Instance Variables ~~~~~~~~~~~~~~~~~~~~~~~
        //Holds ship name before it is placed in World.Instance.PlayerName
        public string shipName;

        //Holds game difficulty before it is placed in World.Instance.Difficulty
        public int diffNum;

        //Holds cheat boolean before it is placed in World
        public bool isCheating;

        //Is left button on?
        bool left = false;

        //Is right button on?
        bool right = false;

        //Is up button on?
        bool up = false;

        //Is down button on?
        bool down = false;

        //Is fire button on?
        bool fire = false;

        //Is pause menu on?
        bool pause = false;

        //Manages new stackpanel menus (pause and gameOver)
        StackLayout stkWindow;

        public GamePage()
        {
            InitializeComponent();
        }

        //Holds the current sprites on the screen
        List<Sprite> sprites;

        //~~~~~~~~~~~~~~~~~~~~~~~~~ Method Zone ~~~~~~~~~~~~~~~~~~~~~~~~~

        //Creates the Mobile pause menu
        public void LoadPause()
        {
            stkWindow = new StackLayout();
            stkWindow.HorizontalOptions = LayoutOptions.Center;
            stkWindow.Margin = new Thickness(200, 100);
            stkWindow.BackgroundColor = Color.DarkCyan;
            stkWindow.Spacing = 10;
            stkWindow.Padding = 30;

            Button btnContinue = new Button();
            btnContinue.Text = "CONTINUE";
            btnContinue.TextColor = Color.DarkCyan;
            btnContinue.FontFamily = "Impact";
            btnContinue.FontSize = 30;
            btnContinue.Clicked += btnContinue_Clicked;
            btnContinue.SetBinding(Button.CommandProperty, new Binding("ViewModelProperty"));
            btnContinue.BindingContext = controlGrid;
            stkWindow.Children.Add(btnContinue);

            Button btnLoad = new Button();
            btnLoad.Text = "LOAD GAME";
            btnLoad.TextColor = Color.DarkCyan;
            btnLoad.FontFamily = "Impact";
            btnLoad.FontSize = 30;
            btnLoad.Clicked += btnLoad_Clicked;
            btnLoad.IsEnabled = false;
            stkWindow.Children.Add(btnLoad);

            Button btnSave = new Button();
            btnSave.Text = "SAVE GAME";
            btnSave.TextColor = Color.DarkCyan;
            btnSave.FontFamily = "Impact";
            btnSave.FontSize = 30;
            btnSave.Clicked += btnSave_Clicked;
            btnSave.IsEnabled = false;
            stkWindow.Children.Add(btnSave);

            Button btnBack = new Button();
            btnBack.Text = "BACK TO MENU";
            btnBack.TextColor = Color.DarkCyan;
            btnBack.FontFamily = "Impact";
            btnBack.FontSize = 30;
            btnBack.Clicked += btnBack_Clicked;
            stkWindow.Children.Add(btnBack);

            controlGrid.Children.Add(stkWindow);
        }

        //Creates the mobile Game Over menu
        public void LoadEndScreen(bool hiScore)
        {
            stkWindow = new StackLayout();
            stkWindow.HorizontalOptions = LayoutOptions.Center;
            stkWindow.Margin = new Thickness(200, 100);
            stkWindow.BackgroundColor = Color.DarkCyan;
            stkWindow.Spacing = 10;
            stkWindow.Padding = 30;

            Label lblGameOver = new Label();
            lblGameOver.Text = hiScore ? "NEW HIGH SCORE!" : "GAME OVER";
            lblGameOver.TextColor = hiScore ? Color.Gold : Color.DarkRed;
            lblGameOver.FontFamily = "Impact";
            lblGameOver.FontSize = 40;
            lblGameOver.HorizontalTextAlignment = TextAlignment.Center;
            stkWindow.Children.Add(lblGameOver);

            Label lblName = new Label();
            lblName.Text = World.Instance.PlayerName.ToUpper();
            lblName.TextColor = Color.Black;
            lblName.FontFamily = "Impact";
            lblName.FontSize = 30;
            lblName.HorizontalTextAlignment = TextAlignment.Center;
            stkWindow.Children.Add(lblName);

            Label lblScore = new Label();
            lblScore.Text = Convert.ToString(World.Instance.Score);
            lblScore.TextColor = Color.Black;
            lblScore.FontFamily = "Impact";
            lblScore.FontSize = 30;
            lblScore.HorizontalTextAlignment = TextAlignment.Center;
            stkWindow.Children.Add(lblScore);

            Button btnBack = new Button();
            btnBack.Text = "MAIN MENU";
            btnBack.TextColor = Color.DarkCyan;
            btnBack.FontFamily = "Impact";
            btnBack.FontSize = 30;
            btnBack.Clicked += btnBack_Clicked;
            stkWindow.Children.Add(btnBack);

            controlGrid.Children.Add(stkWindow);
        }

        //Closes the pause menu and continues the game
        public void ClosePause()
        {
            try
            {
                controlGrid.Children.Remove(stkWindow);
                GameTimerStart();
                pause = false;
            }
            catch { }
        }

        //~~~~~~~~~~~~~~~~~~~~ Add Sprite ~~~~~~~~~~~~~~~~~~~~
        //Adds the corresponding sprite to the view
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
        //Removes the corresponding sprite from the view
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

        //~~~~~~~~~~~~~~~~~~ Play Sound ~~~~~~~~~~~~~~~~~
        //Placeholder method for the ViewManager Interface
        //Unable to implement sounds for Xamarin
        public void PlaySound(string key)
        {

        }

        //~~~~~~~~~~~~~~~~~~ Trigger Endgame ~~~~~~~~~~~~~~~
        //Loads the end of game screen
        public void TriggerEndGame()
        {
            LoadEndScreen(false);
        }

        //~~~~~~~~~~~~~~~~~~~~Set Timer ~~~~~~~~~~~~~~~~~
        //Manages the game timer and calls the GameLoop() on the tick
        public void GameTimerStart()
        {
            TimeSpan time = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            Device.StartTimer(time, () =>
            {
                GameLoop();
                if (pause)
                {
                    LoadPause();
                    return false;
                }
                return true;
            });
        }

        //~~~~~~~~~~~~~~~~~~~~ Game Loop ~~~~~~~~~~~~~~~~~~~~
        //Called each game tick and manages each frame of the game
        public void GameLoop()
        {
            World.Instance.Update();
            
            Device.BeginInvokeOnMainThread(() =>
            {
                for (int i = 0; i < sprites.Count; ++i)
                {
                    sprites[i].Update();
                }

                // Input handling
                World.Instance.PlayerController.Up = up;
                World.Instance.PlayerController.Down = down;
                World.Instance.PlayerController.Left = left;
                World.Instance.PlayerController.Right = right;
                World.Instance.PlayerController.Fire = fire;

                lblScore.Text = Convert.ToString(World.Instance.Score);

                //int potentialCollisions = World.Instance.Objects.Count;
                //potentialCollisions = (potentialCollisions * potentialCollisions - potentialCollisions) / 2;
                //txtTest.Text = World.Instance.Collisions.ToString() + '/' + potentialCollisions;
            });
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~ End Method Zone ~~~~~~~~~~~~~~~~~~~~~~~~~

        //~~~~~~~~~~~~~~~~~~~~~~~~~ Event Handling Zone ~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~ Window Loaded ~~~~~~~~~~~~~~~~~~~~
        //Sets initial variables and values for the game page
        private void ContentPage_Appearing(object sender, EventArgs ev)
        {
            sprites = new List<Sprite>();
            World.Instance.ViewManager = this;
            World.Instance.Reset();
            if (isCheating) World.Instance.EnableCheatMode();

            World.Instance.SetScreenDimensions(-Width, -Height, Width, Height);
            World.Instance.StartX = -950;
            World.Instance.EndX = 950;
            World.Instance.StartY = -350;
            World.Instance.EndY = 500;

            if (isCheating)
            {
                World.Instance.EnableCheatMode();
            }
            else
            {
                World.Instance.DisableCheatMode();
            }

            // Source: https://stackoverflow.com/questions/29644200/how-get-mono-xamarin-android-app-path-folder
            World.Instance.Directory = System.Environment.CurrentDirectory;
            World.Instance.CreatePlayer();
            World.Instance.Difficulty = diffNum;
            World.Instance.PlayerName = shipName;
            lblName.Text = shipName;

            // DisplayAlert("Alert", System.Environment.CurrentDirectory, "OK");

            GameTimerStart();

            
        }


        //~~~~~~~~~~~~~~~~~~~Control Management Zone~~~~~~~~~~~~~~~~~~~~~

        //~~~~~~~~~~~~~~~~~~~Pressed~~~~~~~~~~~~~~~~~~~~~~~~~
        //The left button is pressed; begin going left
        private void btnLeft_Pressed(object sender, EventArgs e)
        {
            left = true;
        }

        //The right button is pressed; begin going right
        private void btnRight_Pressed(object sender, EventArgs e)
        {
            right = true;
        }

        //The up button is pressed; begin going up
        private void btnUp_Pressed(object sender, EventArgs e)
        {
            up = true;
        }

        //The down button is pressed; begin going down
        private void btnDown_Pressed(object sender, EventArgs e)
        {
            down = true;
        }

        //The fire button is pressed; begin firing
        private void btnFire_Pressed(object sender, EventArgs e)
        {
            fire = true;
        }

        //~~~~~~~~~~~~~~~~~~~~~~Released~~~~~~~~~~~~~~~~~~~~~~~~~
        //The left button is released; stop accelerating left
        private void btnLeft_Released(object sender, EventArgs e)
        {
            left = false;
        }

        //The right button is released; stop accelerating right
        private void btnRight_Released(object sender, EventArgs e)
        {
            right = false;
        }

        //The up button is released; stop accelerating up
        private void btnUp_Released(object sender, EventArgs e)
        {
            up = false;
        }

        //The down button is released; stop accelerating down
        private void btnDown_Released(object sender, EventArgs e)
        {
            down = false;
        }

        //The fire button is released; stop firing
        private void btnFire_Released(object sender, EventArgs e)
        {
            fire = false;
        }

        //~~~~~~~~~~~~~~~~~~~Pause Menu Buttons~~~~~~~~~~~~~~~~~~~~~~~~~
        //Toggles the pause menu
        private void btnPause_Clicked(object sender, EventArgs e)
        {
            if (pause)
            {
                pause = false;
                ClosePause();
            }
            else
            {
                pause = true;
            }
        }

        //Continues the game
        private void btnContinue_Clicked(object sender, EventArgs ev)
        {
            pause = false;
            ClosePause();
        }

        //Would have triggered the loading logic
        //Ran into issues involving StreamReader with Xamarin
        private void btnLoad_Clicked(object sender, EventArgs ev)
        {
            //Loading Here
        }

        //Would have triggered the save logic
        //Ran into issues involving StreamWriter with Xamarin
        private void btnSave_Clicked(object sender, EventArgs ev)
        {
            //SaveState Here
        }

        //Reloads the main menu and closes the gamepage
        private void btnBack_Clicked(object sender, EventArgs ev)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}