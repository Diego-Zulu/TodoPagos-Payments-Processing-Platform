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
    public class LoginFacadeShould
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfUnitOfWorkIsNullOnCreation()
        {
            IUnitOfWork mockUnitOfWork = null;
            LoginFacade facade = new LoginFacade(mockUnitOfWork);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void FailIfUserDoesntHaveTheRightRole()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            LoginFacade facade = new LoginFacade(mockUnitOfWork.Object);
            string email = "soycajero@hotmail.com";
            string password = "Hola1234!";
            User cashierUser = new User("Cajero", "soycajero@hotmail.com", "Hola1234!", CashierRole.GetInstance());
            IEnumerable<User> allUsers = new List<User> { cashierUser };
            mockUnitOfWork.Setup(u => u.UserRepository.Get(It.IsAny<Expression<Func<User,bool>>>(), null, "")).Returns(allUsers);

            facade.AdminLogin(email, password);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfEmailIsIncorrect()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            LoginFacade facade = new LoginFacade(mockUnitOfWork.Object);
            string email = "hotmail.com";
            string password = "Hola1234!";
            User cashierUser = new User("Cajero", "soycajero@hotmail.com", "Hola1234!", CashierRole.GetInstance());
            IEnumerable<User> allUsers = new List<User> { cashierUser };
            mockUnitOfWork.Setup(u => u.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>(), null, "")).Returns(new List<User>());

            facade.AdminLogin(email, password);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfPasswordIsIncorrect()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            LoginFacade facade = new LoginFacade(mockUnitOfWork.Object);
            string email = "soycajero@hotmail.com";
            string password = "hola1234!";
            User cashierUser = new User("Cajero", "soycajero@hotmail.com", "Hola1234!", CashierRole.GetInstance());
            IEnumerable<User> allUsers = new List<User> { cashierUser };
            mockUnitOfWork.Setup(u => u.UserRepository.Get(It.IsAny<Expression<Func<User, bool>>>(), null, "")).Returns(new List<User>());

            facade.AdminLogin(email, password);
        }
    }
}
