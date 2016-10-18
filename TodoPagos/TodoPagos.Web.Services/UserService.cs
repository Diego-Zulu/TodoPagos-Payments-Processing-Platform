using System;
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

        public int CreateUser(User newUser, string signedInUserEmail)
        {
            MakeSureUserHasRequiredPrivilege(signedInUserEmail);
            MakeSureTargetUserIsReadyToBeCreated(newUser);
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
            MakeSureTargetIsNotAlreadyInRepository(targetUser);
            MakeSureUserIsComplete(targetUser);
            targetUser.HashPasswordIfCorrect();
            PutInTargetUserHisRolesThatAreAlreadyInRepository(targetUser);
        }

        private void PutInTargetUserHisRolesThatAreAlreadyInRepository(User targetUser)
        {
            ICollection<Role> rolesToBePutInTarget = new List<Role>();

            foreach (Role oneRole in targetUser.Roles)
            {
                IEnumerable<Role> roleWithEqualName = unitOfWork.RoleRepository.Get(
                    x => x.Name.Equals(oneRole.Name));

                if (roleWithEqualName.Count() > 0)
                {
                    rolesToBePutInTarget.Add(roleWithEqualName.First());
                } else
                {
                    PutInTargetRoleHisPrivilegesThatAreAlreadyInRepository(oneRole);
                    rolesToBePutInTarget.Add(oneRole);
                }
            }

            targetUser.Roles = rolesToBePutInTarget;
        }

        private void PutInTargetRoleHisPrivilegesThatAreAlreadyInRepository(Role targetRole)
        {
            ICollection<Privilege> privilegesToBePutInTarget = new List<Privilege>();

            foreach (Privilege onePrivilege in targetRole.Privileges)
            {
                IEnumerable<Privilege> privilegesWithEqualName = unitOfWork.PrivilegeRepository.Get(
                    x => x.Name.Equals(onePrivilege.Name));

                if (privilegesWithEqualName.Count() > 0)
                {
                    privilegesToBePutInTarget.Add(privilegesWithEqualName.First());
                }
                else
                {
                    privilegesToBePutInTarget.Add(onePrivilege);
                }
            }

            targetRole.Privileges = privilegesToBePutInTarget;
        }

        private void MakeSureUserIsComplete(User targetUser)
        {
            if (!targetUser.IsComplete())
            {
                throw new InvalidOperationException();
            }
        }

        private void MakeSureTargetIsNotAlreadyInRepository(User targetUser)
        {
            if (ExistsUser(targetUser))
            {
                throw new InvalidOperationException();
            }
        }

        private void MakeSureTargetUserIsNotNull(User targetUser)
        {
            if (targetUser == null)
            {
                throw new ArgumentNullException();
            }
        }

        public bool DeleteUser(int id, string signedInUserEmail)
        {
            MakeSureUserHasRequiredPrivilege(signedInUserEmail);
            if (ExistsUser(id) && !SameUser(id, signedInUserEmail))
            {
                unitOfWork.UserRepository.Delete(id);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        private bool SameUser(int id, string signedInUserEmail)
        {
            User userToBeDeleted = unitOfWork.UserRepository.GetByID(id);
            User signedInUser = unitOfWork.UserRepository.Get(us => us.Email.Equals(signedInUserEmail), null, "").First();
            return signedInUser.Equals(userToBeDeleted);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public IEnumerable<User> GetAllUsers(string signedInUserEmail)
        {
            MakeSureUserHasRequiredPrivilege(signedInUserEmail);
            return unitOfWork.UserRepository.Get(null, null, "");
        }

        public User GetSingleUser(int id, string signedInUserEmail)
        {
            MakeSureUserHasRequiredPrivilege(signedInUserEmail);
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

        public bool UpdateUser(int userId, User user, string signedInUserEmail)
        {
            MakeSureUserHasRequiredPrivilege(signedInUserEmail);
            if (user != null && userId == user.ID && ExistsUser(userId) && !AnotherDifferentUserAlreadyHasThisEmail(user))
            {
                User userEntity = unitOfWork.UserRepository.GetByID(userId);
                userEntity.UpdateInfoWithTargetUsersInfo(user);
                PutInTargetUserHisRolesThatAreAlreadyInRepository(userEntity);
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
                us => us.ID != userToBeChecked.ID && us.Email.Equals(userToBeChecked.Email), null, "");
            return usersThatExists.Count() > 0;
        }

        private bool ExistsUser(User userToBeChecked)
        {
            IEnumerable<User> usersThatExists = unitOfWork.UserRepository.Get(
             us => us.Email.Equals(userToBeChecked.Email) 
             || us.ID.Equals(userToBeChecked.ID), null, "");
            return usersThatExists.Count() > 0;
        }
    }
}
