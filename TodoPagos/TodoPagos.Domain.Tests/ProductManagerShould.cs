using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace Tests
{
    [TestClass]
    public class ProductManagerShould
    {
        [TestMethod]
        public void HaveEmptyProductsListAndAvailableProductList()
        {
            ProductManager newProductManager = ProductManager.GetInstance();

            Assert.AreEqual(0, newProductManager.Products.Count);
            Assert.AreEqual(0, newProductManager.AvailableProducts.Count);
        }

        [TestMethod]
        public void AlwaysHaveOnlyOneInstance()
        {
            ProductManager firstProductManager = ProductManager.GetInstance();
            ProductManager secondProductManager = ProductManager.GetInstance();

            Assert.AreSame(firstProductManager, secondProductManager);
        }
    }
}
