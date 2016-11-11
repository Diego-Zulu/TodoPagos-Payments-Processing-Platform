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
        IEnumerable<User> GetAllUsers(string signedInUserEmail);

        User GetSingleUser(int id, string signedInUserEmail);

        IEnumerable<string> GetRolesOfUser(string emailOfUser, string signedInUserEmail);

        int CreateUser(User newUser, string signedInUserEmail);

        bool UpdateUser(int userId, User user, string signedInUserEmail);

        bool DeleteUser(int id, string signedInUserEmail);

        void Dispose();
    }
}
