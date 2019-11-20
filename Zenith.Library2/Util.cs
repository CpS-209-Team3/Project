using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Zenith.Library
{
    public class Util
    {
        public static string GetImagePath(string path)
        {
            var s = Directory.GetCurrentDirectory();
            return s + "\\Sprites\\Pixel_Spaceships_for_SHMUP_1.4\\Pixel_Spaceships_for_SHMUP_1.4\\" + path;
        }
    }
}
