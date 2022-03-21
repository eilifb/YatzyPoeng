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
            Assert.AreEqual(yp.BeregnPoeng("1,1,1,1,1", "Enere"), 5);
            Assert.AreEqual(yp.BeregnPoeng("1,2,4,5,5", "Femere"), 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "For mange terninger")]
        public void TestArgumentForMangeTerninger()
        {
            int poeng = yp.BeregnPoeng("1,2,3,4,5,6", "Enere");
        }

    }
}
