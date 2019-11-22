using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Zenith.Library
{
    class VectorTest
    {
        [Test]
        public void Vector_CallConstructor_AllGood()
        {
            var v = new Vector(6, 7);
            Assert.IsTrue(v.X == 6);
            Assert.IsTrue(v.Y == 6);
        }

        [Test]
        public void Vector_CallConstructorPolar_AllGood()
        {
            var v = new Vector(0, 7, true);
            Assert.IsTrue(v.X == 7);
            Assert.IsTrue(v.Y == 0);
        }

        [Test]
        public void OperatorAdd_GoodSituation_AllGood()
        {
            var v1 = new Vector(6, 7);
            var v2 = new Vector(7, 8);

            var v3 = v1 + v2;
            Assert.IsTrue(v3.X == 13);
            Assert.IsTrue(v3.Y == 15);
        }
    }
}
