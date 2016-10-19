using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain.DataAccess;
using Moq;
using TodoPagos.UserAPI;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace TodoPagos.Domain.Repository.Tests
{
    [TestClass]
    public class UnitOfWorkShould
    {
        [TestMethod]
        public void ReceiveATodoPagosContextInCreation()
        {
            var mockContext = new Mock<TodoPagosContext>();

            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfTodoPagosContextOnCreationIsNull()
        {
            TodoPagosContext context = null;

            UnitOfWork unitOfWork = new UnitOfWork(context);
        }

        [TestMethod]
        public void ReturnANewUserRepositoryInCaseThereIsNotOneAlready()
        {
            var mockContext = new Mock<TodoPagosContext>();
            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);

            IRepository<User> repository = unitOfWork.UserRepository;

            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void ReturnTheSameUserRepositoryInCaseThereIsOneAlready()
        {
            var mockContext = new Mock<TodoPagosContext>();
            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);

            IRepository<User> repository = unitOfWork.UserRepository;

            Assert.AreSame(unitOfWork.UserRepository, repository);
        }

        [TestMethod]
        public void ReturnANewReceiptRepositoryInCaseThereIsNotOneAlready()
        {
            var mockContext = new Mock<TodoPagosContext>();
            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);

            IRepository<Receipt> repository = unitOfWork.ReceiptRepository;

            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void ReturnTheSameReceiptRepositoryInCaseThereIsOneAlready()
        {
            var mockContext = new Mock<TodoPagosContext>();
            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);

            IRepository<Receipt> repository = unitOfWork.ReceiptRepository;

            Assert.AreSame(unitOfWork.ReceiptRepository, repository);
        }

        [TestMethod]
        public void ReturnANewProviderRepositoryInCaseThereIsNotOneAlready()
        {
            var mockContext = new Mock<TodoPagosContext>();
            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);

            IRepository<Provider> repository = unitOfWork.ProviderRepository;

            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void ReturnTheSameProviderRepositoryInCaseThereIsOneAlready()
        {
            var mockContext = new Mock<TodoPagosContext>();
            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);

            IRepository<Provider> repository = unitOfWork.ProviderRepository;

            Assert.AreSame(unitOfWork.ProviderRepository, repository);
        }

        [TestMethod]
        public void ReturnANewPaymentRepositoryInCaseThereIsNotOneAlready()
        {
            var mockContext = new Mock<TodoPagosContext>();
            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);

            IRepository<Payment> repository = unitOfWork.PaymentRepository;

            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void ReturnTheSamePaymentRepositoryInCaseThereIsOneAlready()
        {
            var mockContext = new Mock<TodoPagosContext>();
            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);

            IRepository<Payment> repository = unitOfWork.PaymentRepository;

            Assert.AreSame(unitOfWork.PaymentRepository, repository);
        }

        [TestMethod]
        public void ReturnANewRoleRepositoryInCaseThereIsNotOneAlready()
        {
            var mockContext = new Mock<TodoPagosContext>();
            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);

            IRepository<Role> repository = unitOfWork.RoleRepository;

            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void ReturnTheSameRoleRepositoryInCaseThereIsOneAlready()
        {
            var mockContext = new Mock<TodoPagosContext>();
            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);

            IRepository<Role> repository = unitOfWork.RoleRepository;

            Assert.AreSame(unitOfWork.RoleRepository, repository);
        }

        [TestMethod]
        public void ReturnANewPrivilegeRepositoryInCaseThereIsNotOneAlready()
        {
            var mockContext = new Mock<TodoPagosContext>();
            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);

            IRepository<Privilege> repository = unitOfWork.PrivilegeRepository;

            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void ReturnTheSamePrivilegeRepositoryInCaseThereIsOneAlready()
        {
            var mockContext = new Mock<TodoPagosContext>();
            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);

            IRepository<Privilege> repository = unitOfWork.PrivilegeRepository;

            Assert.AreSame(unitOfWork.PrivilegeRepository, repository);
        }

        [TestMethod]
        public void BeAbleToDiposeItself()
        {
            List<User> data = new List<User>();
            var mockContext = new Mock<TodoPagosContext>();
            var set = new Mock<DbSet<User>>().SetupData(data);
            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);
            mockContext.Setup(ctx => ctx.Set<User>()).Returns(set.Object);

            unitOfWork.Dispose();

            Assert.AreEqual(0, unitOfWork.UserRepository.Get().Count());
        }

        [TestMethod]
        public void BeAbleToSaveChangesInContextOnCommand()
        {
            var mockContext = new Mock<TodoPagosContext>();
            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);

            unitOfWork.Save();

            mockContext.Verify(cx => cx.SaveChanges(), Times.Exactly(1));
        }

        [TestMethod]
        public void BeAbleToTellCurrentSignedInUserHasRequiredPrivileges()
        {
            User currentSignedInUser = new User("Bruno", "bferr42@gmail.com", "Hola111!!!", AdminRole.GetInstance());
            List<User> data = new List<User>();
            data.Add(currentSignedInUser);
            var mockContext = new Mock<TodoPagosContext>();
            var set = new Mock<DbSet<User>>().SetupData(data);
            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);
            mockContext.Setup(ctx => ctx.Set<User>()).Returns(set.Object);

            Assert.IsTrue(unitOfWork.CurrentSignedInUserHasRequiredPrivilege("bferr42@gmail.com", UserManagementPrivilege.GetInstance()));
        }

        [TestMethod]
        public void BeAbleToThatCurrentSignedInUserDoesNotHaveRequiredPrivilegesIfEmailDoesNotMatch()
        {
            User currentSignedInUser = new User("Bruno", "bferr42@gmail.com", "Hola111!!!", AdminRole.GetInstance());
            List<User> data = new List<User>();
            data.Add(currentSignedInUser);
            var mockContext = new Mock<TodoPagosContext>();
            var set = new Mock<DbSet<User>>().SetupData(data);
            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);
            mockContext.Setup(ctx => ctx.Set<User>()).Returns(set.Object);

            Assert.IsFalse(unitOfWork.CurrentSignedInUserHasRequiredPrivilege("hola@gmail.com", UserManagementPrivilege.GetInstance()));
        }

        [TestMethod]
        public void BeAbleToThatCurrentSignedInUserDoesNotHaveRequiredPrivilegesIfThereAreNoUsers()
        {
            List<User> data = new List<User>();
            var mockContext = new Mock<TodoPagosContext>();
            var set = new Mock<DbSet<User>>().SetupData(data);
            UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);
            mockContext.Setup(ctx => ctx.Set<User>()).Returns(set.Object);

            Assert.IsFalse(unitOfWork.CurrentSignedInUserHasRequiredPrivilege("bferr42@gmail.com", UserManagementPrivilege.GetInstance()));
        }
    }
}
