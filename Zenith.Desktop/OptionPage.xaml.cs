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
using Zenith.Library;

namespace Zenith.Desktop
{
    /// <summary>
    /// Interaction logic for OptionPage.xaml
    /// </summary>
    public partial class OptionPage : Page
    {
        bool cheat;
        MainWindow main;
        public OptionPage(MainWindow theMainOne)
        {
            cheat = true;
            InitializeComponent();
            main = theMainOne;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Event Handler Zone ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Back Button Click ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            //Make the main window content to the canView(Canvas name in MainWindow)
            //Return to canView by make Content = canView
            main.Content = main.canView;
        }

        //~~~~~~~~~~~~~~~~~~~~ Cheat Button Click ~~~~~~~~~~~~~~~~~~~~
        private void btn_Cheat_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(btn_Cheat.Content) == "OFF")
            {
                btn_Cheat.Content = "ON";
                cheat = true;
            }
            else
            {
                btn_Cheat.Content = "OFF";
                cheat = false;
            }  
        }

        //~~~~~~~~~~~~~~~~~~~~ Start Button Click ~~~~~~~~~~~~~~~~~~~~
        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            GamePage gamepg = new GamePage(main);
            gamepg.shipName = Txt_shipName.Text;
            gamepg.diffNum = difficult_Dropdown.SelectedIndex + 1;
            gamepg.isCheating = cheat;
            main.Content = gamepg;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ End Event Handler Zone ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    }
}
