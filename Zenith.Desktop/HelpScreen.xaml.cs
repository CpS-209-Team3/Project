using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

//--------------------------------------------------------------
//File:   HelpScreen.xaml.cs
//Desc:   Help page that let player know which button to play.
//--------------------------------------------------------------

namespace Zenith.Desktop
{
    /// <summary>
    /// Interaction logic for HelpScreen.xaml
    /// </summary>
    public partial class HelpScreen : Page
    {
        MainWindow main;
        public HelpScreen(MainWindow theMainOne)
        {
            InitializeComponent();
            main = theMainOne;
            
            // Uncomment to let the asteroid spin...
            // Just make the Asteroid spin for fun... Ignore it :D
            
            // Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        Thread.Sleep(16);
            //        Dispatcher.Invoke(() =>
            //       {
            //           if (img_angle.Angle == 360)
            //               img_angle.Angle = 0;
            //           img_angle.Angle += 3;
            //       });
            //    }
            //});
        }

        //~~~~~~~~~~~~~~~~~~~~ Back Button Click ~~~~~~~~~~~~~~~~~~~~
        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            //Make the main window content to the canView(Canvas name in MainWindow)
            //Return to canView by make Content = canView
            main.Content = main.canView;
        }
    }
}
