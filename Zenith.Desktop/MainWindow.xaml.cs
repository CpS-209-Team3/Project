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
using Zenith.Library;

namespace Zenith.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ViewManager
    {

        Enemy1 e;

        public MainWindow()
        {
            InitializeComponent();
        }

        List<Sprite> sprites;

        //~~~~~~~~~~~~~~~~~~~~~~~~~ Method Zone ~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~ Add Sprite ~~~~~~~~~~~~~~~~~~~~
        public void AddSprite(GameObject obj)
        {
            var s = new Sprite(obj);
            sprites.Add(s);
            canView.Children.Add(s);
        }

        //~~~~~~~~~~~~~~~~~~~~ Remove Sprite ~~~~~~~~~~~~~~~~~~~~
        public void RemoveSprite(GameObject obj)
        {
            foreach (var s in sprites)
            {
                if (s.GameObject == obj)
                {
                    sprites.Remove(s);
                    canView.Children.Remove(s);
                    break;
                }
            }
        }

        //~~~~~~~~~~~~~~~~~~~~ Game Loop ~~~~~~~~~~~~~~~~~~~~
        public void GameLoop()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    World.Instance.Update();
                    e.Update();

                    Dispatcher.Invoke(() =>
                    {
                        for (int i = 0; i < sprites.Count; ++i)
                        {
                            sprites[i].Update();
                        }
                    });
                    Task.Delay(1000 / 60);
                }
            });
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~ End Method Zone ~~~~~~~~~~~~~~~~~~~~~~~~~

        //~~~~~~~~~~~~~~~~~~~~~~~~~ Event Handling Zone ~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~ Window Loaded ~~~~~~~~~~~~~~~~~~~~
        private void Window_Loaded(object sender, RoutedEventArgs ev)
        {
            sprites = new List<Sprite>();
            World.Instance.ViewManager = this;

            /*var i = new BitmapImage(new Uri(Util.GetImagePath("blue_01.png"), UriKind.Absolute));
            
            var img = new Image();
            img.Source = i;
            canView.Children.Add(img);
            */
            var txt = new TextBox();
            txt.Text = Util.GetImagePath("blue_01.png");
            canView.Children.Add(txt);

            e = new Enemy1(new Library.Vector(70, 70));
            AddSprite(e);
            e.Update();
            GameLoop();
        }

        //~~~~~~~~~~~~~~~~~~~~ Help Screen ~~~~~~~~~~~~~~~~~~~~
        private void btn_Help_Click(object sender, RoutedEventArgs e)
        {
            HelpScreen help = new HelpScreen();
            this.Content = help;
        }

        //~~~~~~~~~~~~~~~~~~~~ About Page ~~~~~~~~~~~~~~~~~~~~
        private void btn_About_Click(object sender, RoutedEventArgs e)
        {
            AboutPage about = new AboutPage();
            this.Content = about;
        }

        //~~~~~~~~~~~~~~~~~~~~ Setting Page ~~~~~~~~~~~~~~~~~~~~
        private void btn_Setting_Click(object sender, RoutedEventArgs e)
        {

        }

        //~~~~~~~~~~~~~~~~~~~~ Load Game ~~~~~~~~~~~~~~~~~~~~
        private void btn_Load_Click(object sender, RoutedEventArgs e)
        {

        }

        //~~~~~~~~~~~~~~~~~~~~ Play Game ~~~~~~~~~~~~~~~~~~~~
        private void btn_Play_Click(object sender, RoutedEventArgs e)
        {

        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~ End Event Handling ~~~~~~~~~~~~~~~~~~~~~~~~~
    }
}
