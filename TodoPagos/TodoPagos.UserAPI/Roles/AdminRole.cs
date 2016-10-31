using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.UserAPI
{
    public class AdminRole : Role
    {
        private static AdminRole instance;

        protected AdminRole()
        {
            this.Name = "Admin";
        }

        public static AdminRole GetInstance()
        {
            if (instance == null)
            {
                instance = new AdminRole();
                instance.AddAdminPrivileges();
            }
            return instance;
        }
        private void AddAdminPrivileges()
        {
            this.Privileges.Add(UserManagementPrivilege.GetInstance());
            this.Privileges.Add(ProviderManagementPrivilege.GetInstance());
            this.Privileges.Add(EarningQueriesPrivilege.GetInstance());
        }

    }
}
