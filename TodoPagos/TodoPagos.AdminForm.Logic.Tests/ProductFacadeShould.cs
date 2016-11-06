using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain.Repository;

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
    }
}
