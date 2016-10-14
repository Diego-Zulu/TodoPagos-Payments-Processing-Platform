﻿using System;
using System.Collections.Generic;
using TodoPagos.UserAPI;
using TodoPagos.Domain.Repository;
using System.Linq;
using System.Web;

namespace TodoPagos.Web.Services
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork oneUnitOfWork)
        {
            MakeSureTargetUnitOfWorkIsNotNull(oneUnitOfWork);
            unitOfWork = oneUnitOfWork;
        }

        private void MakeSureTargetUnitOfWorkIsNotNull(IUnitOfWork oneUnitOfWork)
        {
            if (oneUnitOfWork == null)
            {
                throw new ArgumentException();
            }
        }

        public int CreateUser(User newUser, string password, string signedInUserEmail)
        {
            MakeSureUserHasRequiredPrivilege(signedInUserEmail);
            MakeSureTargetUserIsReadyToBeCreated(newUser);
            MakeSureTargetUserIsWithCorrectPassword(newUser, password);
            unitOfWork.UserRepository.Insert(newUser);
            unitOfWork.Save();
            return newUser.ID;
        }

        private void MakeSureUserHasRequiredPrivilege(string signedInUserEmail)
        {
            if (!unitOfWork.CurrentSignedInUserHasRequiredPrivilege(signedInUserEmail, UserManagementPrivilege.GetInstance()))
            {
                throw new UnauthorizedAccessException();
            }
        }

        private void MakeSureTargetUserIsReadyToBeCreated(User targetUser)
        {
            MakeSureTargetUserIsNotNull(targetUser);
            MakeSureTargetUserIsComplete(targetUser);
            MakeSureTargetIsNotAlreadyInRepository(targetUser);
        }

        private void MakeSureTargetIsNotAlreadyInRepository(User targetUser)
        {
            if (ExistsUser(targetUser))
            {
                throw new InvalidOperationException();
            }
        }

        private void MakeSureTargetUserIsWithCorrectPassword(User targetUser, string password)
        {
            targetUser.SetPasswordIfCorrect(password);
        }

        private void MakeSureTargetUserIsNotNull(User targetUser)
        {
            if (targetUser == null)
            {
                throw new ArgumentNullException();
            }
        }

        private void MakeSureTargetUserIsComplete(User targetUser)
        {
            if (!targetUser.IsComplete())
            {
                throw new ArgumentException();
            }
        }

        public bool DeleteUser(int id)
        {
            if (ExistsUser(id))
            {
                unitOfWork.UserRepository.Delete(id);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return unitOfWork.UserRepository.Get(null, null, "");
        }

        public User GetSingleUser(int id)
        {
            User foundUser = unitOfWork.UserRepository.GetByID(id);
            ThrowArgumentExceptionIfUserWasntFound(foundUser);
            return foundUser;
        }

        private void ThrowArgumentExceptionIfUserWasntFound(User foundUser)
        {
            if (foundUser == null)
            {
                throw new ArgumentException();
            }
        }

        public bool UpdateUser(int userId, User user, string newPassword, string signedInUserEmail)
        {
            if (user != null && userId == user.ID && ExistsUser(userId) && !AnotherDifferentUserAlreadyHasThisEmail(user))
            {
                User userEntity = unitOfWork.UserRepository.GetByID(userId);
                userEntity.UpdateInfoWithTargetUsersInfo(user, newPassword);
                unitOfWork.UserRepository.Update(userEntity);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        private bool ExistsUser(int userId)
        {
            User user = unitOfWork.UserRepository.GetByID(userId);
            return user != null;
        }

        private bool AnotherDifferentUserAlreadyHasThisEmail(User userToBeChecked)
        {
            IEnumerable<User> usersThatExists = unitOfWork.UserRepository.Get(
                us => us.ID != userToBeChecked.ID && us.Email.Equals(userToBeChecked), null, "");
            return usersThatExists.Count() > 0;
        }

        private bool ExistsUser(User userToBeChecked)
        {
            IEnumerable<User> usersThatExists = unitOfWork.UserRepository.Get(
                us => us.Equals(userToBeChecked), null, "");
            return usersThatExists.Count() > 0;
        }
    }
}
