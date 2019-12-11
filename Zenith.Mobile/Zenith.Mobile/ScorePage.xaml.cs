//---------------------------------------------------------------
//File:   ScorePage.xaml.cs
//Desc:   Displays the high score page for the game
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

    //Manages the high score page values
    //Difficulty implementing high score lists for mobile due to difficulties with StreamReader/Writer.
    public partial class ScorePage : ContentPage
    {
        public ScorePage()
        {
            InitializeComponent();
        }

        //Returns to the Main Menu
        private void btn_Back_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}