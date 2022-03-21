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
        [ExpectedException(typeof(ArgumentException), "For mange terninger")]
        public void TestArgumentForMangeTerninger()
        {
            int poeng = yp.BeregnPoeng("1,2,3,4,5,6", "Enere");
        }

    }
}
