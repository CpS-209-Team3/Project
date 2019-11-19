using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Zenith.Library;

namespace Zenith.Desktop
{
    class Sprite : Image
    {
        Library.Vector position;

        public void Update()
        {
            Margin = new Thickness(position.X, position.Y, 0, 0);
        }

        public Sprite(Library.Vector pos)
        {
            position = pos;
        }
    }
}
