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
    /// Interaction logic for HelpScreen.xaml
    /// </summary>
    public partial class HelpScreen : Page
    {
        ContentControl control;
        public HelpScreen(ContentControl ctrl);
        //MainWindow main;
        public HelpScreen(MainWindow theMainOne)
        {
            InitializeComponent();
            control = ctrl;
            //main = theMainOne;
        }

        //~~~~~~~~~~~~~~~~~~~~ Back Button Click ~~~~~~~~~~~~~~~~~~~~
        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            //Make the main window content to the canView(Canvas name in MainWindow)
            //main.Content = main.canView;
            // trying to fix this......
            MainWindow main = new MainWindow();
            control.Content = main;
        }
    }
}
