//---------------------------------------------------------------
//File:   SetupPage.xaml.cs
//Desc:   Manages the options for the game
//---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zenith.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    //Holds the variables and methods to setup the game options
    public partial class SetupPage : ContentPage
    {
        //Is the player wanting cheat mode on?
        bool cheat;
        public SetupPage()
        {
            InitializeComponent();
            cheat = true;
        }


        //Starts the game and initializes the options
        private void BtnStart_Clicked(object sender, EventArgs e)
        {
            //This function is still buggy
            GamePage game = new GamePage();
            game.shipName = EntName.Text;
            game.diffNum = PickerDiff.SelectedIndex + 1;
            game.isCheating = cheat;
            Application.Current.MainPage = game;
        }

        //Toggles cheat mode and the cheat variable
        private void BtnCheat_Clicked(object sender, EventArgs e)
        {
            if(BtnCheat.Text == "CHEAT ON")
            {
                cheat = false;
                BtnCheat.Text = "CHEAT OFF";
            }
            else
            {
                cheat = true;
                BtnCheat.Text = "CHEAT ON";
            }
        }

        //Returns to the main menu
        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

        //Selects the difficulty chosen by the player
        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnStart.IsEnabled = true;
        }
    }
}