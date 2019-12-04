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
        bool left = false;
        bool right = false;
        bool up = false;
        bool down = false;
        bool fire = false;
        bool pause = false;
        StackLayout stkPause;

        public GamePage()
        {
            InitializeComponent();
        }

        List<Sprite> sprites;

        //~~~~~~~~~~~~~~~~~~~~~~~~~ Method Zone ~~~~~~~~~~~~~~~~~~~~~~~~~

        public void LoadPause()
        {
            stkPause = new StackLayout();
            stkPause.HorizontalOptions = LayoutOptions.Center;
            stkPause.Margin = new Thickness(200, 100);
            stkPause.BackgroundColor = Color.DarkCyan;
            stkPause.Spacing = 10;
            stkPause.Padding = 30;

            Button btnContinue = new Button();
            btnContinue.Text = "CONTINUE";
            btnContinue.TextColor = Color.DarkCyan;
            btnContinue.FontFamily = "Impact";
            btnContinue.FontSize = 30;
            btnContinue.Clicked += btnContinue_Clicked;
            btnContinue.SetBinding(Button.CommandProperty, new Binding("ViewModelProperty"));
            btnContinue.BindingContext = controlGrid;
            stkPause.Children.Add(btnContinue);

            Button btnLoad = new Button();
            btnLoad.Text = "LOAD GAME";
            btnLoad.TextColor = Color.DarkCyan;
            btnLoad.FontFamily = "Impact";
            btnLoad.FontSize = 30;
            btnLoad.Clicked += btnLoad_Clicked;
            stkPause.Children.Add(btnLoad);

            Button btnSave = new Button();
            btnSave.Text = "SAVE GAME";
            btnSave.TextColor = Color.DarkCyan;
            btnSave.FontFamily = "Impact";
            btnSave.FontSize = 30;
            btnSave.Clicked += btnSave_Clicked;
            stkPause.Children.Add(btnSave);

            Button btnBack = new Button();
            btnBack.Text = "BACK TO MENU";
            btnBack.TextColor = Color.DarkCyan;
            btnBack.FontFamily = "Impact";
            btnBack.FontSize = 30;
            btnBack.Clicked += btnBack_Clicked;
            stkPause.Children.Add(btnBack);

            controlGrid.Children.Add(stkPause);
        }

        public void ClosePause()
        {
            try
            {
                controlGrid.Children.Remove(stkPause);
                GameTimerStart();
                pause = false;
            }
            catch { }
        }

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

        //~~~~~~~~~~~~~~~~~~~~Set Timer ~~~~~~~~~~~~~~~~~
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

            World.Instance.CreatePlayer();

            GameTimerStart();
        }


        //~~~~~~~~~~~~~~~~~~~Control Management Zone~~~~~~~~~~~~~~~~~~~~~

        //~~~~~~~~~~~~~~~~~~~Pressed~~~~~~~~~~~~~~~~~~~~~~~~~
        private void btnLeft_Pressed(object sender, EventArgs e)
        {
            left = true;
            lblScore.Text = "left";






















































































































































































































































































































































































































































































































































































































        }

        private void btnRight_Pressed(object sender, EventArgs e)
        {
            right = true;
            lblScore.Text = "right";
        }

        private void btnUp_Pressed(object sender, EventArgs e)
        {
            up = true;
            lblScore.Text = "up";
        }

        private void btnDown_Pressed(object sender, EventArgs e)
        {
            down = true;
            lblScore.Text = "down";
        }


        private void btnFire_Pressed(object sender, EventArgs e)
        {
            fire = true;
            lblName.Text = "fire";
        }

        //~~~~~~~~~~~~~~~~~~~~~~Released~~~~~~~~~~~~~~~~~~~~~~~~~
        private void btnLeft_Released(object sender, EventArgs e)
        {
            left = false;
            lblScore.Text = "000000000";
        }


        private void btnRight_Released(object sender, EventArgs e)
        {
            right = false;
            lblScore.Text = "000000000";
        }

        private void btnUp_Released(object sender, EventArgs e)
        {
            up = false;
            lblScore.Text = "000000000";
        }

        private void btnDown_Released(object sender, EventArgs e)
        {
            down = false;
            lblScore.Text = "000000000";
        }

        private void btnFire_Released(object sender, EventArgs e)
        {
            fire = false;
            lblName.Text = "Splattian";
        }

        //~~~~~~~~~~~~~~~~~~~Pause Menu Buttons~~~~~~~~~~~~~~~~~~~~~~~~~
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
        private void btnContinue_Clicked(object sender, EventArgs ev)
        {
            ClosePause();
        }

        private void btnLoad_Clicked(object sender, EventArgs ev)
        {
            //Jame's Loading Here
        }

        private void btnSave_Clicked(object sender, EventArgs ev)
        {
            //Jame's SaveState Here
        }

        private void btnBack_Clicked(object sender, EventArgs ev)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}