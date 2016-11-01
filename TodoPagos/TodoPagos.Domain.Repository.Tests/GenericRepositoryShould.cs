using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.UserAPI;
using TodoPagos.Domain.DataAccess;
using Moq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace TodoPagos.Domain.Repository.Tests
{
    [TestClass]
    public class GenericRepositoryShould
    {
        [TestMethod]
        public void ReceiveATodoPagosContextInCreation()
        {
            var mockContext = new Mock<TodoPagosContext>();

            GenericRepository<User> repository = new GenericRepository<User>(mockContext.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfTodoPagosContextOnCreationIsNull()
        {
            TodoPagosContext context = null;

            GenericRepository<User> repository = new GenericRepository<User>(context);
        }

        [TestMethod]
        public void BeAbleToGetAllObjectsWhileApplyingAFilterAndIncludingPropertiesPlusOrderingBy()
        {
            User user = new User("Bruno", "bferr42@gmail.com", "Hola111!!!", AdminRole.GetInstance());
            User anotherUser = new User("Alberto", "alberto@gmail.com", "Hola111!!!", AdminRole.GetInstance());
            List<User> data = new List<User>();
            data.Add(user);
            data.Add(anotherUser);
            var mockContext = new Mock<TodoPagosContext>();
            var set = new Mock<DbSet<User>>().SetupData(data);
            mockContext.Setup(ctx => ctx.Set<User>()).Returns(set.Object);
            GenericRepository<User> repository = new GenericRepository<User>(mockContext.Object);

            IEnumerable<User> resultingUsers = repository.Get((x => x.HasThisRole(AdminRole.GetInstance())),
                (x => x.AsQueryable().OrderBy(u => u.Name)), "Roles");

            Assert.AreEqual(2, resultingUsers.Count());
        }

        [TestMethod]
        public void BeAbleToGetAnObjectByID()
        {
            User user = new User("Bruno", "bferr42@gmail.com", "Hola111!!!", AdminRole.GetInstance());
            var mockContext = new Mock<TodoPagosContext>();
            mockContext.Setup(ctx => ctx.Set<User>().Find(It.IsAny<int>())).Returns(user);
            GenericRepository<User> repository = new GenericRepository<User>(mockContext.Object);

            User resultingUser = repository.GetByID(0);

            Assert.AreEqual(user, resultingUser);
        }

        [TestMethod]
        public void BeAbleToDeleteAnObjectByID()
        {
            User user = new User("Bruno", "bferr42@gmail.com", "Hola111!!!", AdminRole.GetInstance());
            TodoPagosContext context = new TodoPagosContext();
            GenericRepository<User> repository = new GenericRepository<User>(context);

            repository.Insert(user);
            repository.Delete(0);

            Assert.IsNull(repository.GetByID(0));
        }

        [TestMethod]
        public void BeAbleToDeleteAnObject()
        {
            User user = new User("Bruno", "bferr42@gmail.com", "Hola111!!!", AdminRole.GetInstance());
            TodoPagosContext context = new TodoPagosContext();
            GenericRepository<User> repository = new GenericRepository<User>(context);

            repository.Insert(user);
            repository.Delete(user);

            Assert.IsNull(repository.GetByID(0));
        }

        [TestMethod]
        public void BeAbleToUpdateAnObject()
        {
            User user = new User("Bruno", "bferr42@gmail.com", "Hola111!!!", AdminRole.GetInstance());
            User modifiedUser = new User("Anselmo", "bferr42@gmail.com", "Hola111!!!", AdminRole.GetInstance());
            TodoPagosContext context = new TodoPagosContext();
            GenericRepository<User> repository = new GenericRepository<User>(context);

            repository.Insert(user);
            repository.Update(modifiedUser);

            Assert.AreEqual("Anselmo", repository.GetByID(0).Name);
        }
    }
}
