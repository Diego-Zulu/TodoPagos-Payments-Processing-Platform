using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain;

namespace Tests
{
    [TestClass]
    public class ProductShould
    {
        [TestMethod]
        public void ReceiveNameDescriptionAndNeededPointsOnCreation()
        {
            string name = "Manzana Roja";
            string description = "Son mas frescas por la tarde";
            int neededPoints = 10;

            Product newProduct = new Product(name, description, neededPoints);
        }

        [TestMethod]
        public void HaveStock0OnCreation()
        {
            string name = "Manzana Roja";
            string description = "Son mas frescas por la tarde";
            int neededPoints = 10;

            Product newProduct = new Product(name, description, neededPoints);

            Assert.AreEqual(0, newProduct.Stock);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfNameIsNullOrWhiteSpaceOnCreation()
        {
            string name = null;
            string description = "Son mas frescas por la tarde";
            int neededPoints = 10;

            Product newProduct = new Product(name, description, neededPoints);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfDescriptionIsNullOnCreation()
        {
            string name = "Manzana Roja";
            string description = null;
            int neededPoints = 10;

            Product newProduct = new Product(name, description, neededPoints);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfNeededPointsAreNegativeOnCreation()
        {
            string name = "Manzana Roja";
            string description = "Son mas frescas por la tarde";
            int neededPoints = -10;

            Product newProduct = new Product(name, description, neededPoints);
        }
    }
}
