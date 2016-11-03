using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain;

namespace Tests
{
    [TestClass]
    public class PointsManagerShould
    {
        [TestMethod]
        public void HaveEmptyBlackListAnd150MoneyToPointsConversion()
        {
            PointsManager newPointsManager = new PointsManager();

            Assert.AreEqual(150, newPointsManager.MoneyPerPoint);
            Assert.AreEqual(0, newPointsManager.Blacklist.Count);
        }
    }
}
