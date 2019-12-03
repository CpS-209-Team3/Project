using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class HealthBar : GameObject
    {
        Ship host;
        double distance;

        public override void Loop()
        {
            distance = Math.Max(host.Size.X, host.Size.Y);
            position = host.Position - new Vector(0, distance);
            destroy = host.Destroy;

            imageIndex = (int)((double)host.Health / host.MaxHealth * 10);
            if (imageIndex < 0) imageIndex = 0;
        }

        public HealthBar(Ship host)
            : base(host.Position)
        {
            this.host = host;
            imageSources = new List<string> {
                Util.GetSpriteFolderPath("Health_Bar\\healthbar_0.png"),
                Util.GetSpriteFolderPath("Health_Bar\\healthbar_1.png"),
                Util.GetSpriteFolderPath("Health_Bar\\healthbar_2.png"),
                Util.GetSpriteFolderPath("Health_Bar\\healthbar_3.png"),
                Util.GetSpriteFolderPath("Health_Bar\\healthbar_4.png"),
                Util.GetSpriteFolderPath("Health_Bar\\healthbar_5.png"),
                Util.GetSpriteFolderPath("Health_Bar\\healthbar_6.png"),
                Util.GetSpriteFolderPath("Health_Bar\\healthbar_7.png"),
                Util.GetSpriteFolderPath("Health_Bar\\healthbar_8.png"),
                Util.GetSpriteFolderPath("Health_Bar\\healthbar_9.png"),
                Util.GetSpriteFolderPath("Health_Bar\\healthbar_10.png"),
            };
            imageRotation = 0;
            collidable = false;
            type = GameObjectType.HealthBar;
            size *= 100;
        }

        public override string Serialize()
        {
            return base.Serialize() + ',' + host.Position.ToString() + ',' + distance.ToString();
        }

        public override void Deserialize(string saveInfo)
        {
            int index = IndexOfNthOccurance(saveInfo, ",", 12);

            string gameObjectSaveInfo = saveInfo.Substring(0, index);
            base.Deserialize(gameObjectSaveInfo);

            string[] healthBarSaveInfo = saveInfo.Substring(index + 1, saveInfo.Length - index - 1).Split(',');

            string[] xNy = healthBarSaveInfo[0].Split(':');
            Vector pos = new Vector(Convert.ToDouble(xNy[0]), Convert.ToDouble(xNy[1]), false);

            host = (Ship)World.Instance.Objects.Find(obj => (obj.Position == pos && obj.Collidable == true));
            distance = Convert.ToDouble(healthBarSaveInfo[1]);
        }
    }
}
