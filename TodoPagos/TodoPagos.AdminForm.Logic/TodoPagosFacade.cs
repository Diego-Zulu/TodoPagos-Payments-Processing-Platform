using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.Domain.Repository;
using TodoPagos.UserAPI;

namespace TodoPagos.AdminForm.Logic
{
    public class TodoPagosFacade
    {
        private IUnitOfWork unitOfWork;

        public TodoPagosFacade(IUnitOfWork aUnitOfWork)
        {
            unitOfWork = aUnitOfWork;
        }

        public void AdminLogin(string email, string password)
        {
            IEnumerable<User> relatedUser = unitOfWork.UserRepository.Get(u => u.Email.Equals(email) &&
                                            Hashing.VerifyHash(password, u.Salt, u.Password), null, "");
            CheckIfUserWasFound(relatedUser);
            CheckIfUserHasRightRole(relatedUser);
        }

        private void CheckIfUserWasFound(IEnumerable<User> relatedUser)
        {
            if (relatedUser.Count() == 0) throw new ArgumentException();
        }

        private void CheckIfUserHasRightRole(IEnumerable<User> relatedUser)
        {
            User userToLogin = relatedUser.First();
            if (!userToLogin.HasThisRole(AdminRole.GetInstance())) throw new UnauthorizedAccessException();
        }

    }
}
