using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain.Repository;
using Moq;
using TodoPagos.ProductImporterLogic;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Collections;

namespace TodoPagos.AdminForm.Logic.Tests
{
    [TestClass]
    public class ProductFacadeShould
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfUnitOfWorkIsNullOnCreation()
        {
            IUnitOfWork mockUnitOfWork = null;
            ProductFacade facade = new ProductFacade(mockUnitOfWork);
        }

        [TestMethod]
        public void BeAbleToAddANewProduct()
        {
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            ProductFacade facade = new ProductFacade(mockUnitOfWork.Object);
            mockUnitOfWork.Setup(u => u.ProductsRepository.Get(It.IsAny<Expression<Func<Product, bool>>>(), null, "")).Returns(new List<Product>());
            mockUnitOfWork.Setup(u => u.ProductsRepository.Insert(It.IsAny<Product>()));
            Product productToBeAdded = new Product("Cocina", "Una cocina", 200);

            facade.AddProduct(productToBeAdded);

            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        public void BeAbleToGetAllProducts()
        {
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            ProductFacade facade = new ProductFacade(mockUnitOfWork.Object);
            Product productToBeAdded = new Product("Cocina", "Una cocina", 200);
            List<Product> allProducts = new List<Product>() { productToBeAdded };
            mockUnitOfWork.Setup(u => u.ProductsRepository.Get(It.IsAny<Expression<Func<Product, bool>>>(), null, "")).Returns(allProducts);

            IEnumerable<Product> returnedProduts = facade.GetProducts();

            mockUnitOfWork.VerifyAll();
            CollectionAssert.AreEqual(allProducts, (ICollection)returnedProduts);
        }
    }
}
