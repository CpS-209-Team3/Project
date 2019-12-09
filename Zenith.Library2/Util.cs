using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Zenith.Library
{
    public class Util
    {
        public static string GetShipSpriteFolderPath(string path)
        {
            var s = World.Instance.Directory;
            return s + "\\Sprites\\Pixel_Spaceships_for_SHMUP_1.4\\Pixel_Spaceships_for_SHMUP_1.4\\" + path;
        }
        public static string GetSpriteFolderPath(string path)
        {
            var s = World.Instance.Directory;
            return s + "\\Sprites\\" + path;
        }
        public static string GetSoundFolderPath(string path)
        {
            var s = World.Instance.Directory;
            return s + "\\Sounds\\" + path;
        }
    }
}
