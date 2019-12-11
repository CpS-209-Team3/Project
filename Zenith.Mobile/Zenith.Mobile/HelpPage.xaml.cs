//---------------------------------------------------------------
//File:   HelpPage.xaml.cs
//Desc:   Displays the moblie game instructions
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

    //Describes the instructions for the mobile version of the game
    public partial class HelpPage : ContentPage
    {
        public HelpPage()
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