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
        public void ReceiveAUnitOfWorkOnCreation()
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
            UserService userService = new UserService(mockUnitOfWork.Object);

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
            UserService userService = new UserService(mockUnitOfWork.Object);

            User returnedUser = userService.GetSingleUser(5);
        }

        [TestMethod]
        public void BeAbleToCreateNewUserInRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.UserRepository.Insert(It.IsAny<User>()));
            mockUnitOfWork.Setup(un => un.Save());
            UserService userService = new UserService(mockUnitOfWork.Object);

            User singleUser = new User("Diego", "diego_i_zuluaga@outlook.com", "#ElBizagra1995", AdminRole.GetInstance());
            int id = userService.CreateUser(singleUser);

            mockUnitOfWork.VerifyAll(); 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfToBeCreatedNewUserIsNotComplete()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            UserService userService = new UserService(mockUnitOfWork.Object);

            User singleUser = new User();

            int id = userService.CreateUser(singleUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FailWithNullArgumentExceptionIfToBeCreatedNewUserIsNull()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            UserService userService = new UserService(mockUnitOfWork.Object);

            User singleUser = null;

            int id = userService.CreateUser(singleUser);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FailWithInvalidOperationExceptionIfToBeCreatedNewUserIsAlreadyInRepository()
        {
            User repeatedUser = new User("Diego", "diego_i_zuluaga@outlook.com", "#ElBizagra1995", AdminRole.GetInstance());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(un => un.UserRepository.Get(
                It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>(), null, ""))
                .Returns(new[] { repeatedUser });
            
            UserService userService = new UserService(mockUnitOfWork.Object);

            int id = userService.CreateUser(repeatedUser);
        }

        [TestMethod]
        public void BeAbleToUpdateExistingUser()
        {
            User toBeUpdatedUser = new User("Diego", "diego_i_zuluaga@outlook.com", "#ElBizagra1995", AdminRole.GetInstance());
            User updatedUser = new User("Diego Z", "dizg2695@hotmail.com", "#ElBizagra1995", AdminRole.GetInstance());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine1(mockUnitOfWork, toBeUpdatedUser);
            UserService userService = new UserService(mockUnitOfWork.Object);

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
            User updatedUser = new User("Diego", "dizg2695@hotmail.com", "#ElBizagra1995", AdminRole.GetInstance());
            updatedUser.Name = "";
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine2(mockUnitOfWork, toBeUpdatedUser);
            UserService userService = new UserService(mockUnitOfWork.Object);

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
        }

        [TestMethod]
        public void NotUpdateNonExistingUser()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine3(mockUnitOfWork);
            UserService userService = new UserService(mockUnitOfWork.Object);

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
        }

        [TestMethod]
        public void NotUpdateNullUser()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine4(mockUnitOfWork);
            UserService userService = new UserService(mockUnitOfWork.Object);

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
        }

        [TestMethod]
        public void NotUpdateUserWithDifferentIdThanSupplied()
        {
            User updatedUserInfo = new User("Diego", "diego_i_zuluaga@outlook.com", "#ElBizagra1995", AdminRole.GetInstance());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine5(mockUnitOfWork);
            UserService userService = new UserService(mockUnitOfWork.Object);

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
        }

        [TestMethod]
        public void NotUpdateUserWithEmailOfAnotherUserInRepository()
        {
            User updatedUserInfo = new User("Diego", "diego_i_zuluaga@outlook.com", "#ElBizagra1995", AdminRole.GetInstance());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine6(mockUnitOfWork);
            UserService userService = new UserService(mockUnitOfWork.Object);

            bool updated = userService.UpdateUser(updatedUserInfo.ID + 1, updatedUserInfo);

            mockUnitOfWork.Verify(un => un.UserRepository.Update(It.IsAny<User>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(updated);
        }

        private void SetMockUpdateRoutine6(Mock<IUnitOfWork> mockUnitOfWork)
        {
            User userWithSameEmail = new User("Bruno", "diego_i_zuluaga@outlook.com", "#ElBizagrita1996", AdminRole.GetInstance());
            userWithSameEmail.ID = 5;
            mockUnitOfWork
                .Setup(un => un.UserRepository.GetByID(It.IsAny<int>()))
                .Returns(() => new User());
            mockUnitOfWork
               .Setup(un => un.UserRepository.Get(
               It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>(), null, ""))
               .Returns(new[] { userWithSameEmail });
        }

        [TestMethod]
        public void BeAbleToDeleteUserFromRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockDeleteRoutine1(mockUnitOfWork);
            UserService userService = new UserService(mockUnitOfWork.Object);

            bool deleted = userService.DeleteUser(2);

            mockUnitOfWork.Verify(un => un.UserRepository.Delete(It.IsAny<int>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));
            Assert.IsTrue(deleted);
        }

        private void SetMockDeleteRoutine1(Mock<IUnitOfWork> mockUnitOfWork)
        {
            mockUnitOfWork
                .Setup(un => un.UserRepository.GetByID(It.IsAny<int>()))
                .Returns(() => new User());
            mockUnitOfWork.Setup(un => un.UserRepository.Delete(It.IsAny<int>()));
            mockUnitOfWork.Setup(un => un.Save());
        }

        [TestMethod]
        public void NotDeleteAnythingIfUserIdDoesntExistInRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockDeleteRoutine2(mockUnitOfWork);
            UserService userService = new UserService(mockUnitOfWork.Object);

            bool deleted = userService.DeleteUser(2);

            mockUnitOfWork.Verify(un => un.UserRepository.Delete(It.IsAny<int>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(deleted);
        }

        private void SetMockDeleteRoutine2(Mock<IUnitOfWork> mockUnitOfWork)
        {
            mockUnitOfWork
                .Setup(un => un.UserRepository.GetByID(It.IsAny<int>()))
                .Returns(() => null);
            mockUnitOfWork.Setup(un => un.UserRepository.Delete(It.IsAny<int>()));
            mockUnitOfWork.Setup(un => un.Save());
        }
    }
}
