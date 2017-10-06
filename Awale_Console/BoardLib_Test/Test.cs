using NUnit.Framework;
using System;
using BoardLib;

namespace BoardLib_Test
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestCase()
        {
            Board myBoard = new Board();
            Assert.AreEqual(myBoard.seed, 44);
            Assert.AreEqual(myBoard.seed, 42);
        }


    }
}

