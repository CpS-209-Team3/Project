using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Zenith.Library
{
    class TestZenithGame
    {
        [Test]
        public void Update_AllGood_NoException()
        {
            var zg = new ZenithGame();
            try
            {
                zg.Update();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsFalse(true);
            }
        }


    }
}
