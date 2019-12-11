//---------------------------------------------------------------
//File:   MainWindow.xaml.cs
//Desc:   Manages the menu view and buttons
//---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Zenith.View
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    //Partial class that powers the menu buttons
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        //~~~~~~~~~~~~~~~~~~~~ Event Handling Zone ~~~~~~~~~~~~~~~~~~~~

        //~~~~~~~~~~~~~~~~~~~~ Play Button ~~~~~~~~~~~~~~~~~~~~
        //Opens the setup page
        private void BtnNew_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SetupPage();
        }

        //~~~~~~~~~~~~~~~~~~~~ Load Game ~~~~~~~~~~~~~~~~~~~~
        //Would have opened the game with a loaded copy
        private void BtnLoad_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new GamePage();
        }

        //~~~~~~~~~~~~~~~~~~~~ High Scores Page ~~~~~~~~~~~~~~~~~~~~
        //Opens the high scores screen
        private void BtnScores_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ScorePage();
        }

        //~~~~~~~~~~~~~~~~~~~~ Help Page ~~~~~~~~~~~~~~~~~~~~
        //Opens the How to Play screen HelpPage
        private void BtnHelp_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new HelpPage();
        }

        //~~~~~~~~~~~~~~~~~~~~ Credits Page ~~~~~~~~~~~~~~~~~~~~
        //Opens the Credits page 
        private void BtnCredits_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new CreditPage();
        }

        //~~~~~~~~~~~~~~~~~~~~ Event Handling End ~~~~~~~~~~~~~~~~~~~~
    }
}
