using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Zenith.Desktop
{
    /// <summary>
    /// Interaction logic for OptionPage.xaml
    /// </summary>
    public partial class OptionPage : Page
    {
        MainWindow main;
        public OptionPage(MainWindow theMainOne)
        {
            InitializeComponent();
            main = theMainOne;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Event Handler Zone ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~ Back Button Click ~~~~~~~~~~~~~~~~~~~~
        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            //Make the main window content to the canView(Canvas name in MainWindow)
            main.Content = main.canView;
        }

        //~~~~~~~~~~~~~~~~~~~~ Cheat Button Click ~~~~~~~~~~~~~~~~~~~~
        private void btn_Cheat_Click(object sender, RoutedEventArgs e)
        {
            if (lblCheat.Text == "ON")
                lblCheat.Text = "OFF";
            else
                lblCheat.Text = "ON";
        }

        //~~~~~~~~~~~~~~~~~~~~ Difficult Button Click ~~~~~~~~~~~~~~~~~~~~
        private void btn_Difficult_Click(object sender, RoutedEventArgs e)
        {

        }

        //~~~~~~~~~~~~~~~~~~~~ Start Button Click ~~~~~~~~~~~~~~~~~~~~
        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {

        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ End Event Handler Zone ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    }
}
