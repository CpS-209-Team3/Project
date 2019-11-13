using System;
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
            GameModel g = new GameModel("george");
            g.
            g.Save(g.GameName);
            g.Load(g.GameName);
            Assert.IsTrue(g.GameObjects[0].ToString() == g.GameName);
        }

        [Test]
        public void Test_Load()
        {
            GameModel g = new GameModel("george");
            g.Load(g.GameName);
            Assert.IsTrue(g.GameObjects[0].ToString() == g.GameName);
        }
    }
}
