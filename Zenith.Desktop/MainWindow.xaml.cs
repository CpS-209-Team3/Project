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
using System.Windows.Input;

namespace Zenith.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ViewManager
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<Sprite> sprites;

        public void AddSprite(GameObject obj)
        {
            var s = new Sprite(obj);
            sprites.Add(s);
            canView.Children.Add(s);
        }

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

        public void GameLoop()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    World.Instance.Update();

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

        private void Window_Loaded(object sender, RoutedEventArgs ev)
        {
            sprites = new List<Sprite>();
            World.Instance.ViewManager = this;
            World.Instance.AddObject(new Player(new Library.Vector(40, 40)));
            GameLoop();
        }
    }
}
