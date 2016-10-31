using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.UserAPI
{
    public class UserManagementPrivilege : Privilege
    {
        private static UserManagementPrivilege instance;

        protected UserManagementPrivilege()
        {
            this.Name = "User Managment";
        }

        public static UserManagementPrivilege GetInstance()
        {
            if (instance == null)
            {
                instance = new UserManagementPrivilege();
            }
            return instance;
        }
    }
}
