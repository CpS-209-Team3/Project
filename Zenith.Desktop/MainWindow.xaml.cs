//-----------------------------------------------------------
//File:   MainWindow.xaml.cs
//Desc:   Main Menu Screen of Zenith game.
//-----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;
using Zenith.Library;

namespace Zenith.Desktop
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~ Event Handling Zone ~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~ Window Loaded ~~~~~~~~~~~~~~~~~~~~
        private void Window_Loaded(object sender, RoutedEventArgs ev)
        {

        }

        //~~~~~~~~~~~~~~~~~~~~ Help Screen ~~~~~~~~~~~~~~~~~~~~
        private void btn_Help_Click(object sender, RoutedEventArgs e)
        {
            HelpScreen help = new HelpScreen(this);
            this.Content = help;
        }

        //~~~~~~~~~~~~~~~~~~~~ High Score Page ~~~~~~~~~~~~~~~~~~~~
        private void btn_HighScore_Click(object sender, RoutedEventArgs e)
        {
            HighScorePage highscore = new HighScorePage(this);
            this.Content = highscore;
        }

        //~~~~~~~~~~~~~~~~~~~~ Load Game ~~~~~~~~~~~~~~~~~~~~
        private void btn_Load_Click(object sender, RoutedEventArgs e)
        {
            // Disable button if no save file is available.
            string filename = "Zenith.txt";
            if (File.Exists(filename))
            {
                GamePage game = new GamePage(this, true, filename);
                this.Content = game;
            }
        }

        //~~~~~~~~~~~~~~~~~~~~ Play Game ~~~~~~~~~~~~~~~~~~~~
        private void btn_Play_Click(object sender, RoutedEventArgs e)
        {
            OptionPage option = new OptionPage(this);
            this.Content = option;
        }

        //~~~~~~~~~~~~~~~~~~~~ Credits Page ~~~~~~~~~~~~~~~~~~~~
        private void btn_Credits_Click(object sender, RoutedEventArgs e)
        {
            CreditsPage credits = new CreditsPage(this);
            this.Content = credits;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~ End Event Handling ~~~~~~~~~~~~~~~~~~~~~~~~~
    }
}
