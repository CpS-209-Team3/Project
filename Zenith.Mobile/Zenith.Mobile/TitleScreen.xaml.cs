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
    public partial class TitleScreen : ContentPage
    {
        public TitleScreen()
        {
            InitializeComponent();
        }

        //~~~~~~~~~~~~~~~~~~~~ Event Handling Zone ~~~~~~~~~~~~~~~~~~~~

        //~~~~~~~~~~~~~~~~~~~~ Play Button ~~~~~~~~~~~~~~~~~~~~
        private void btn_Play_Clicked(object sender, EventArgs e)
        {
        }

        //~~~~~~~~~~~~~~~~~~~~ Load Game ~~~~~~~~~~~~~~~~~~~~
        private void btn_Load_Clicked(object sender, EventArgs e)
        {

        }

        //~~~~~~~~~~~~~~~~~~~~ Setting Page ~~~~~~~~~~~~~~~~~~~~
        private void btn_Setting_Clicked(object sender, EventArgs e)
        {

        }

        //~~~~~~~~~~~~~~~~~~~~ Help Page ~~~~~~~~~~~~~~~~~~~~
        private void btn_Help_Clicked(object sender, EventArgs e)
        {

        }

        //~~~~~~~~~~~~~~~~~~~~ About Page ~~~~~~~~~~~~~~~~~~~~
        private void btn_About_Clicked(object sender, EventArgs e)
        {
        }
        //~~~~~~~~~~~~~~~~~~~~ Event Handling End ~~~~~~~~~~~~~~~~~~~~
    }
}