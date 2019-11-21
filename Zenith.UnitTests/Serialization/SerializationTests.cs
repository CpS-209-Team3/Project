using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Zenith.Library;

namespace Zenith.UnitTests
{
    [TestFixture]
    public class SerializationTests
    {
        [Test]
        public void Test_Save_Load_Reset_Methods()
        {
            // refer to the singleton world. Try saving its values into a file named Georgy.txt, and then load them back to see what they are.
            World w = World.Instance;

            w.PlayerName = "Georgy";
            w.Level = 2;
            w.Score = 3;
            w.GameTick = 112;

            Vector v = new Vector(3, 3);
            Asteroid a = new Asteroid(v, 3);
            w.Objects.Add(a);

            Vector v1 = new Vector(4, 4);
            Enemy e = new Enemy(v1);
            w.Objects.Add(e);

            Vector v2 = new Vector(5, 5);
            Enemy1 e1 = new Enemy1(v2);
            w.Objects.Add(e1);

            // The world should now be populated with instance variables and game objects
            Assert.IsTrue(w.PlayerName == "Georgy");
            Assert.IsTrue(w.Objects.Contains(a));
            Assert.IsTrue(w.Objects.Contains(e) && (e.Position == v1));
            
            w.Save("Georgy.txt");

            w.Reset();

            // The world's instance variables should be reset to their default values and the game objects list should be empty
            Assert.IsTrue(w.PlayerName == "");
            Assert.IsTrue(w.GameTick == 0);
            Assert.IsTrue(!w.Objects.Any());

            w.Load("Georgy.txt");

            // All the information we added before should have repopulated the world
            Assert.IsTrue(w.PlayerName == "Georgy");
            Assert.IsTrue(w.Objects.Contains(a));
            Assert.IsTrue(w.Objects.Contains(e) && (e.Position == v1));
        }

    }
}
