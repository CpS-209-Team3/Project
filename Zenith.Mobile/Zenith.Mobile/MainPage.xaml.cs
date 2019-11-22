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
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        //~~~~~~~~~~~~~~~~~~~~ Event Handling Zone ~~~~~~~~~~~~~~~~~~~~

        //~~~~~~~~~~~~~~~~~~~~ Play Button ~~~~~~~~~~~~~~~~~~~~
        private void BtnNew_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SetupPage();
        }

        //~~~~~~~~~~~~~~~~~~~~ Load Game ~~~~~~~~~~~~~~~~~~~~
        private void btn_Load_Clicked(object sender, EventArgs e)
        {

        }

        //~~~~~~~~~~~~~~~~~~~~ High Scores Page ~~~~~~~~~~~~~~~~~~~~
        private void BtnScores_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ScorePage();
        }

        //~~~~~~~~~~~~~~~~~~~~ Help Page ~~~~~~~~~~~~~~~~~~~~
        private void BtnHelp_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new HelpPage();
        }

        //~~~~~~~~~~~~~~~~~~~~ Credits Page ~~~~~~~~~~~~~~~~~~~~
        private void BtnCredits_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new CreditPage();
        }
        //~~~~~~~~~~~~~~~~~~~~ Event Handling End ~~~~~~~~~~~~~~~~~~~~
    }
}
