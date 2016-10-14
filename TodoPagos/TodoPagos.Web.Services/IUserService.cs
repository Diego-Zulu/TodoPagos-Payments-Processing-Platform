using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.UserAPI;

namespace TodoPagos.Web.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();

        User GetSingleUser(int id);

        int CreateUser(User newUser, string password, string signedInUserEmail);

        bool UpdateUser(int userId, User user, string newPassword, string signedInUserEmail);

        bool DeleteUser(int id);

        void Dispose();
    }
}
