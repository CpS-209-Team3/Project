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
    public partial class SetupPage : ContentPage
    {
        bool cheat;
        public SetupPage()
        {
            InitializeComponent();
            cheat = true;
        }

        private void BtnStart_Clicked(object sender, EventArgs e)
        {
            //This function is still buggy
            GamePage game = new GamePage();
            game.shipName = "Schaubian";
            game.diffNum = PickerDiff.SelectedIndex;
            game.isCheating = cheat;
            Application.Current.MainPage = game;
        }


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

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnStart.IsEnabled = true;
        }
    }
}