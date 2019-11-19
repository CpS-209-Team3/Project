/*using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Zenith.Library
{
    [TestFixture]
    public class SerializationTests
    {
        [Test]
        public void Test_Save()
        {
            GameModel g = new GameModel("Georgy");
            Starship s = new Starship();
            g.GameObjects.Add(s);
            g.Save("Georgy");
            g.Quit();
            g.Load("Georgy");
            Assert.IsTrue(g.GameObjects[0].Type() == "starship");
       
        }

        [Test]
        public void Test_Load()
        {
            GameModel g = new GameModel("Georgy");
            g.Load(testfile);
            Assert.IsTrue(g.GameObjects[0].Serialize() == "testfile");
            Assert.IsTrue(g.GameObjects[3].Serialize() == "3100");
            Assert.IsTrue(g.GameObjects[1].Serialize() == "LVL2");
            Assert.IsTrue(g.GameObjects[6].Type() == Laser);
        }
    }
}
*/