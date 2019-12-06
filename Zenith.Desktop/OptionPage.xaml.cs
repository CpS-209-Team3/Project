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
            //Return to canView by make Content = canView
            main.Content = main.canView;
        }

        //~~~~~~~~~~~~~~~~~~~~ Cheat Button Click ~~~~~~~~~~~~~~~~~~~~
        private void btn_Cheat_Click(object sender, RoutedEventArgs e)
        {
            if (btn_Cheat.Content == "OFF")
                btn_Cheat.Content = "ON";
            else
                btn_Cheat.Content = "OFF";
        }

        //~~~~~~~~~~~~~~~~~~~~ Start Button Click ~~~~~~~~~~~~~~~~~~~~
        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            GamePage gamepg = new GamePage(main);
            main.Content = gamepg;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ End Event Handler Zone ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    }
}
