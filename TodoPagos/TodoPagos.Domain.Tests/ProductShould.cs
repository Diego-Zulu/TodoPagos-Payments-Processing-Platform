using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
