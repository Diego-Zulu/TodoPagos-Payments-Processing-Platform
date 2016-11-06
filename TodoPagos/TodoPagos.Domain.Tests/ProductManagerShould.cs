using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace Tests
{
    [TestClass]
    public class ProductManagerShould
    {
        [TestMethod]
        public void AlwaysHaveOnlyOneInstance()
        {
            ProductManager firstProductManager = ProductManager.GetInstance();
            ProductManager secondProductManager = ProductManager.GetInstance();

            Assert.AreSame(firstProductManager, secondProductManager);
        }
    }
}
