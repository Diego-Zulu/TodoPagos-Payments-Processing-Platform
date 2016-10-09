using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain.Repository;
using Moq;
using System.Collections.Generic;
using TodoPagos.UserAPI;

namespace TodoPagos.Web.Services.Test
{
    [TestClass]
    public class UserServiceShould
    {
        [TestMethod]
        public void RecieveAUnitOfWorkOnCreation()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            UserService service = new UserService(mockUnitOfWork.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfUnitOfWorkOnCreationIsNull()
        {
            IUnitOfWork mockUnitOfWork = null;

            UserService service = new UserService(mockUnitOfWork);
        }

        [TestMethod]
        public void BeAbleToGetAllUsersFromRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.UserRepository.Get(null, null, ""));
            UserService userService = new UserService(mockUnitOfWork.Object);

            IEnumerable<User> returnedUsers = userService.GetAllUsers();

            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        public void BeAbleToReturnSingleUserInRepository()
        {
            User singleUser = new User("Diego", "diego_i_zuluaga@outlook.com", "#ElBizagra1995", AdminRole.GetInstance());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.UserRepository.GetByID(singleUser.ID)).Returns(singleUser);
            IUserService userService = new UserService(mockUnitOfWork.Object);

            User returnedUser = userService.GetSingleUser(singleUser.ID);

            mockUnitOfWork.VerifyAll();
            Assert.AreSame(singleUser, returnedUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfSingleUserIdDoesntExistInRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.UserRepository.GetByID(It.IsAny<int>()));
            IUserService userService = new UserService(mockUnitOfWork.Object);

            User returnedUser = userService.GetSingleUser(5);
        }

        [TestMethod]
        public void BeAbleToCreateNewUserInRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.UserRepository.Insert(It.IsAny<User>()));
            mockUnitOfWork.Setup(un => un.Save());
            IUserService userService = new UserService(mockUnitOfWork.Object);

            User singleUser = new User("Diego", "diego_i_zuluaga@outlook.com", "#ElBizagra1995", AdminRole.GetInstance());
            int id = userService.CreateUser(singleUser);

            mockUnitOfWork.VerifyAll(); 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfToBeCreatedNewUserIsNotComplete()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.UserRepository.Insert(It.IsAny<User>()));
            mockUnitOfWork.Setup(un => un.Save());
            IUserService userService = new UserService(mockUnitOfWork.Object);

            User singleUser = new User();

            int id = userService.CreateUser(singleUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FailWithNullArgumentExceptionIfToBeCreatedNewUserIsNull()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.UserRepository.Insert(It.IsAny<User>()));
            mockUnitOfWork.Setup(un => un.Save());
            IUserService userService = new UserService(mockUnitOfWork.Object);

            User singleUser = null;

            int id = userService.CreateUser(singleUser);
        }

        [TestMethod]
        public void BeAbleToUpdatesExistingUser()
        {
            User toBeUpdatedUser = new User("Diego", "diego_i_zuluaga@outlook.com", "#ElBizagra1995", AdminRole.GetInstance());
            User updatedUser = new User("Diego Z", "dizg2695@hotmail.com", "#ElBizagra1995", AdminRole.GetInstance());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine1(mockUnitOfWork, toBeUpdatedUser);
            IUserService userService = new UserService(mockUnitOfWork.Object);

            bool updated = userService.UpdateUser(toBeUpdatedUser.ID, updatedUser);

            mockUnitOfWork.Verify(un => un.UserRepository.Update(It.IsAny<User>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));
            Assert.IsTrue(updated);
        }

        private void SetMockUpdateRoutine1(Mock<IUnitOfWork> mockUnitOfWork, User toBeUpdatedUser)
        {
            mockUnitOfWork
                .Setup(un => un.UserRepository.GetByID(It.IsAny<int>()))
                .Returns(() => toBeUpdatedUser);
            mockUnitOfWork.Setup(un => un.UserRepository.Update(It.IsAny<User>()));
            mockUnitOfWork.Setup(un => un.Save());
        }

        [TestMethod]
        public void NotUpdateNotFilledInformation()
        {
            User toBeUpdatedUser = new User("Diego", "diego_i_zuluaga@outlook.com", "#ElBizagra1995", AdminRole.GetInstance());
            User updatedUser = new User("", "dizg2695@hotmail.com", "#ElBizagra1995", AdminRole.GetInstance());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine2(mockUnitOfWork, toBeUpdatedUser);
            IUserService userService = new UserService(mockUnitOfWork.Object);

            bool updated = userService.UpdateUser(toBeUpdatedUser.ID, updatedUser);

            mockUnitOfWork.Verify(un => un.UserRepository.Update(It.IsAny<User>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));
            Assert.AreEqual(toBeUpdatedUser.Name, updatedUser.Name);
        }

        private void SetMockUpdateRoutine2(Mock<IUnitOfWork> mockUnitOfWork, User toBeUpdatedUser)
        {
            mockUnitOfWork
                .Setup(un => un.UserRepository.GetByID(It.IsAny<int>()))
                .Returns(() => toBeUpdatedUser);
            mockUnitOfWork.Setup(un => un.UserRepository.Update(It.IsAny<User>()));
            mockUnitOfWork.Setup(un => un.Save());
        }

        [TestMethod]
        public void NotUpdateNonExistingUser()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine3(mockUnitOfWork);
            IUserService userService = new UserService(mockUnitOfWork.Object);

            bool updated = userService.UpdateUser(0, new User() { });

            mockUnitOfWork.Verify(un => un.UserRepository.Update(It.IsAny<User>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(updated);
        }

        private void SetMockUpdateRoutine3(Mock<IUnitOfWork> mockUnitOfWork)
        {
            mockUnitOfWork
                .Setup(un => un.UserRepository.GetByID(It.IsAny<int>()))
                .Returns(() => null);
            mockUnitOfWork.Setup(un => un.UserRepository.Update(It.IsAny<User>()));
            mockUnitOfWork.Setup(un => un.Save());
        }

        [TestMethod]
        public void NotUpdateNullUser()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine4(mockUnitOfWork);
            IUserService userService = new UserService(mockUnitOfWork.Object);

            bool updated = userService.UpdateUser(0, null);

            mockUnitOfWork.Verify(un => un.UserRepository.Update(It.IsAny<User>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(updated);
        }

        private void SetMockUpdateRoutine4(Mock<IUnitOfWork> mockUnitOfWork)
        {
            mockUnitOfWork
                .Setup(un => un.UserRepository.GetByID(It.IsAny<int>()))
                .Returns(() => null);
            mockUnitOfWork.Setup(un => un.UserRepository.Update(It.IsAny<User>()));
            mockUnitOfWork.Setup(un => un.Save());
        }

        [TestMethod]
        public void NotUpdateUserWithDifferentIdThanSupplied()
        {
            User updatedUserInfo = new User("Diego", "diego_i_zuluaga@outlook.com", "#ElBizagra1995", AdminRole.GetInstance());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine5(mockUnitOfWork);
            IUserService userService = new UserService(mockUnitOfWork.Object);

            bool updated = userService.UpdateUser(updatedUserInfo.ID + 1, updatedUserInfo);

            mockUnitOfWork.Verify(un => un.UserRepository.Update(It.IsAny<User>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(updated);
        }

        private void SetMockUpdateRoutine5(Mock<IUnitOfWork> mockUnitOfWork)
        {
            mockUnitOfWork
                .Setup(un => un.UserRepository.GetByID(It.IsAny<int>()))
                .Returns(() => new User());
            mockUnitOfWork.Setup(un => un.UserRepository.Update(It.IsAny<User>()));
            mockUnitOfWork.Setup(un => un.Save());
        }
    }
}
