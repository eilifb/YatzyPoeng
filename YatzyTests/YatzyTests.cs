using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YatzyPoengNS;

namespace YatzyTestsNS
{
    [TestClass]
    public class YatzyTests
    {
        public YatzyPoeng yp;

        public YatzyTests()
        {
            yp = new YatzyPoeng();
        }

        [TestMethod]
        public void TestSorter()
        {
            Assert.AreEqual(5, yp.BeregnPoeng("1,1,1,1,1", "Enere"));
            Assert.AreEqual(10, yp.BeregnPoeng("1,2,4,5,5", "Femere"));
        }

        [TestMethod]
        public void TestPar()
        {
            Assert.AreEqual(0, yp.BeregnPoeng("1,2,3,4,5", "Par"));
            Assert.AreEqual(4, yp.BeregnPoeng("2,1,2,3,5", "Par"));
            Assert.AreEqual(10, yp.BeregnPoeng("1,1,2,5,5", "Par"));
            Assert.AreEqual(4, yp.BeregnPoeng("2,2,2,2,2", "Par"));
        }

        [TestMethod]
        public void TestToPar()
        {
            Assert.AreEqual(0, yp.BeregnPoeng("1,3,5,6,1", "ToPar"));
            Assert.AreEqual(10, yp.BeregnPoeng("3,1,4,1,4", "ToPar"));
            Assert.AreEqual(20, yp.BeregnPoeng("5,5,5,5,5", "ToPar"));
        }

        [TestMethod]
        public void TestTreLike()
        {
            Assert.AreEqual(0, yp.BeregnPoeng("1,2,3,4,5", "TreLike"));
            Assert.AreEqual(9, yp.BeregnPoeng("3,3,3,5,1", "TreLike"));
            Assert.AreEqual(15, yp.BeregnPoeng("5,5,5,5,5", "TreLike"));
        }

        [TestMethod]
        public void TestFireLike()
        {
            Assert.AreEqual(0, yp.BeregnPoeng("1,2,3,4,5", "FireLike"));
            Assert.AreEqual(12, yp.BeregnPoeng("3,3,3,3,1", "FireLike"));
            Assert.AreEqual(20, yp.BeregnPoeng("5,5,5,5,5", "FireLike"));
        }

        [TestMethod]
        public void TestLitenStraight()
        {
            Assert.AreEqual(15, yp.BeregnPoeng("1,2,3,4,5", "LitenStraight"));
            Assert.AreEqual(0, yp.BeregnPoeng("1,2,2,3,4", "LitenStraight"));
            Assert.AreEqual(15, yp.BeregnPoeng("1,5,3,4,2", "LitenStraight"));
        }

        [TestMethod]
        public void TestStorStraight()
        {
            Assert.AreEqual(20, yp.BeregnPoeng("2,3,4,5,6", "StorStraight"));
            Assert.AreEqual(0, yp.BeregnPoeng("1,2,2,3,4", "StorStraight"));
            Assert.AreEqual(20, yp.BeregnPoeng("2,5,3,4,6", "StorStraight"));
        }

        [TestMethod]
        public void TestFultHus()
        {
            Assert.AreEqual(13, yp.BeregnPoeng("2,2,3,3,3", "FultHus"));
            Assert.AreEqual(0, yp.BeregnPoeng("1,2,3,3,3", "FultHus"));
            Assert.AreEqual(10, yp.BeregnPoeng("2,2,2,2,2", "FultHus"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "For mange terninger")]
        public void TestArgumentForMangeTerninger()
        {
            int poeng = yp.BeregnPoeng("1,2,3,4,5,6", "Enere");
        }

    }
}
