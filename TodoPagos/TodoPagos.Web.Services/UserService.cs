using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.UserAPI;
using TodoPagos.Domain.Repository;

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

        public int CreateUser(User newUser)
        {
            MakeSureTargetUserIsReadyToBeCreated(newUser);
            unitOfWork.UserRepository.Insert(newUser);
            unitOfWork.Save();
            return newUser.ID;
        }

        private void MakeSureTargetUserIsReadyToBeCreated(User targetUser)
        {
            MakeSureTargetUserIsNotNull(targetUser);
            MakeSureTargetUserIsComplete(targetUser);
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
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
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

        public bool UpdateUser(int id, User toBeUpdatedUser)
        {
            throw new NotImplementedException();
        }
    }
}
