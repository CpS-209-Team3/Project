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
using Zenith.Library.Highscores;

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
        private void HighScorePage_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> nameList = new List<string> { "Darth Vader", "Luke Skywalker", "Han Solo", "Yoda", "Obi-Wan Kenobi", "Rey", "Chewbacca", "Finn", "Superman", "Son Goku", "Stephen Schaub" };
            int PresetScore = 10000;
            int RankNum = 0;
            var HiScrBoard = lbl_top1to5;
            for (int i = 1; i < 11; ++i)
            {
                try
                {
                    RankNum = i;
                    //~~~~~ Change board if not in rank 5 ~~~~~
                    if (i > 5)
                        HiScrBoard = lbl_top6to10;
                    // List start with 0 but i start with 1 => i - 1
                    HiScrBoard.Text += " " + Convert.ToString(RankNum) + ". " + Convert.ToString(hiscrs.LeaderList[i - 1].Name) + " - " + Convert.ToString(hiscrs.LeaderList[i - 1].Score) + "\n\n";
                }
                catch (ArgumentOutOfRangeException)
                {
                    int NewPreset = PresetScore - (i - 1)*1000;
                    Random r = new Random();
                    int randnum = r.Next(11);
                    HiScrBoard.Text += " " + Convert.ToString(RankNum) + ". " + nameList[randnum] + " - " + NewPreset + "\n\n";
                }
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
