//--------------------------------------------------------------
//File:   HighScorePage.xaml.cs
//Desc:   Astronaut of the game ranking is here.
//--------------------------------------------------------------

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
    /// Interaction logic for HighScorePage.xaml
    /// </summary>
    public partial class HighScorePage : Page
    {
        HighScores hiscrs = new HighScores();
        MainWindow main;
        public HighScorePage(MainWindow theMainOne)
        {
            InitializeComponent();
            main = theMainOne;

        }

        //~~~~~~~~~~~~~~~~~~~~ High Score Page Load ~~~~~~~~~~~~~~~~~~~~
        private void HighScorePage_Loaded(object sender, RoutedEventArgs e)
        {
            HighScores highScores = HighScores.Load("highScores.txt");
            int RankNum = 0;
            var HiScrBoard = lbl_top1to5;
            for (int i = 0; i < highScores.LeaderList.Count; ++i)
            {
                //try
                //{
                    RankNum = i + 1;
                    //~~~~~ Change board if not in rank 5 ~~~~~
                    if (i >= 5)
                        HiScrBoard = lbl_top6to10;
                    // List start with 0 but i start with 1 => i - 1
                    HiScrBoard.Text += " " + Convert.ToString(RankNum) + ". " + Convert.ToString(highScores.LeaderList[i].Name) + " - " + Convert.ToString(highScores.LeaderList[i].Score) + "\n\n";
                //}
                //catch (ArgumentOutOfRangeException)
                //{

                //}
            }
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
