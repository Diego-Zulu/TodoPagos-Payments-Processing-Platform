using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.Domain.Repository;
using TodoPagos.UserAPI;

namespace TodoPagos.AdminForm.Logic
{
    public class LoginFacade
    {
        private IUnitOfWork unitOfWork;

        public LoginFacade(IUnitOfWork aUnitOfWork)
        {
            CheckForNullUnitOfWork(aUnitOfWork);
            unitOfWork = aUnitOfWork;
        }

        private void CheckForNullUnitOfWork(IUnitOfWork aUnitOfWork)
        {
            if (aUnitOfWork == null) throw new ArgumentException();
        }

        public void AdminLogin(string email, string password)
        {
            IEnumerable<User> relatedUser = unitOfWork.UserRepository.Get(u => u.Email.Equals(email), null, "");
            CheckIfUserWasFound(relatedUser);
            CheckForCorrectPassword(relatedUser.First(), password);
            CheckIfUserHasRightRole(relatedUser.First());
        }

        private void CheckIfUserWasFound(IEnumerable<User> relatedUser)
        {
            if (relatedUser.Count() == 0) throw new ArgumentException();
        }

        private void CheckForCorrectPassword(User userToLogin, string password)
        {
             if(!Hashing.VerifyHash(password, userToLogin.Salt, userToLogin.Password))
            {
                throw new ArgumentException();
            }
        }

        private void CheckIfUserHasRightRole(User userToLogin)
        {
            if (!userToLogin.HasThisRole(AdminRole.GetInstance())) throw new UnauthorizedAccessException();
        }

    }
}
