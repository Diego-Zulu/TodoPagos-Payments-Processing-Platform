using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain.Repository;
using Moq;
using TodoPagos.UserAPI;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace TodoPagos.AdminForm.Logic.Tests
{
    [TestClass]
    public class TodoPagosFacadeShould
    {
        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void FailIfUserDoesntHaveTheRightRole()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            TodoPagosFacade facade = new TodoPagosFacade(mockUnitOfWork.Object);
            string email = "soycajero@hotmail.com";
            string password = "Hola1234!";
            User cashierUser = new User("Cajero", "soycajero@hotmail.com", "Hola1234!", CashierRole.GetInstance());
            IEnumerable<User> allUsers = new List<User> { cashierUser };
            mockUnitOfWork.Setup(u => u.UserRepository.Get(It.IsAny<Expression<Func<User,bool>>>(), null, "")).Returns(allUsers);

            facade.AdminLogin(email, password);
        }
    }
}
