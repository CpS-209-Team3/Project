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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //~~~~~~~~~~~~~~~~~~~~ Event Handling Zone ~~~~~~~~~~~~~~~~~~~~

        //~~~~~~~~~~~~~~~~~~~~ Play Game ~~~~~~~~~~~~~~~~~~~~
        private void btn_Play_Click(object sender, RoutedEventArgs e)
        {

        }

        //~~~~~~~~~~~~~~~~~~~~ Load Game ~~~~~~~~~~~~~~~~~~~~
        private void btn_Load_Click(object sender, RoutedEventArgs e)
        {

        }

        //~~~~~~~~~~~~~~~~~~~~ Setting Page ~~~~~~~~~~~~~~~~~~~~
        private void btn_Setting_Click(object sender, RoutedEventArgs e)
        {

        }

        //~~~~~~~~~~~~~~~~~~~~ Help Screen ~~~~~~~~~~~~~~~~~~~~
        private void btn_Help_Click(object sender, RoutedEventArgs e)
        {
            HelpScreen help = new HelpScreen();
            this.Content = help;
        }

        //~~~~~~~~~~~~~~~~~~~~ About Screen ~~~~~~~~~~~~~~~~~~~~
        private void btn_About_Click(object sender, RoutedEventArgs e)
        {

        }
        //~~~~~~~~~~~~~~~~~~~~ End Event Handling Zone ~~~~~~~~~~~~~~~~~~~~
    }
}
