using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.ProductImporterLogic;

namespace TodoPagos.ProductImporterLogic.Tests
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfChangeInStockResultsInNegativeStock()
        {
            string name = "Manzana Roja";
            string description = "Son mas frescas por la tarde";
            int neededPoints = 10;

            Product newProduct = new Product(name, description, neededPoints);

            newProduct.AddTargetQuantityToStock(-10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfToBeUpdatedNameIsNullOrWhiteSpace()
        {
            string name = "Manzana Roja";
            string description = "Son mas frescas por la tarde";
            int neededPoints = 10;

            Product newProduct = new Product(name, description, neededPoints);

            newProduct.UpdateName("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfToBeUpdatedDescriptionIsNull()
        {
            string name = "Manzana Roja";
            string description = "Son mas frescas por la tarde";
            int neededPoints = 10;

            Product newProduct = new Product(name, description, neededPoints);

            newProduct.UpdateDescription(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfToBeUpdatedPointsNeededAreNegative()
        {
            string name = "Manzana Roja";
            string description = "Son mas frescas por la tarde";
            int neededPoints = 10;

            Product newProduct = new Product(name, description, neededPoints);

            newProduct.UpdateNeededPoints(-10);
        }

        [TestMethod]
        public void NotModifyProductDataThatIsNotValidFromTargetProduct()
        {
            string name = "Manzana Roja";
            string description = "Son mas frescas por la tarde";
            int neededPoints = 10;

            Product baseProduct = new Product(name, description, neededPoints);
            Product updatedInfoProduct = new Product(name, "", 12);

            updatedInfoProduct.Name = null;

            baseProduct.UpdateWithValidInfoFromTargetProduct(updatedInfoProduct);

            Assert.AreEqual("Manzana Roja", baseProduct.Name);
            Assert.AreEqual(updatedInfoProduct.Description, baseProduct.Description);
            Assert.AreEqual(updatedInfoProduct.NeededPoints, baseProduct.NeededPoints);
            Assert.AreEqual(updatedInfoProduct.Stock, baseProduct.Stock);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfUpdatedInfoProductIsNull()
        {
            string name = "Manzana Roja";
            string description = "Son mas frescas por la tarde";
            int neededPoints = 10;

            Product baseProduct = new Product(name, description, neededPoints);

            baseProduct.UpdateWithValidInfoFromTargetProduct(null);
        }

        [TestMethod]
        public void KnowIfItIsComplete()
        {
            string name = "Manzana Roja";
            string description = "Son mas frescas por la tarde";
            int neededPoints = 10;

            Product baseProduct = new Product(name, description, neededPoints);

            Assert.IsTrue(baseProduct.IsComplete());
        }

        [TestMethod]
        public void BeEqualToAnotherProductWithEqualNameOrEqualID()
        {
            string firstName = "Manzana Roja";
            string secondName = "Manzana Verde";
            string firstDescription = "Son mas frescas por la tarde";
            string secondDescription = "Hola";
            int firstNeededPoints = 10;
            int secondNeededPoints = 0;

            Product firstProduct = new Product(firstName, firstDescription, firstNeededPoints);
            Product secondProduct = new Product(secondName, secondDescription, secondNeededPoints);

            firstProduct.ID = secondProduct.ID;

            Assert.AreEqual(firstProduct, secondProduct);
        }

        [TestMethod]
        public void HaveItsNamesHashcodeAsHashcode()
        {
            string name = "Manzana Roja";
            string description = "Son mas frescas por la tarde";
            int neededPoints = 10;

            Product baseProduct = new Product(name, description, neededPoints);

            Assert.AreEqual(name.GetHashCode(), baseProduct.GetHashCode());
        }
    }
}
