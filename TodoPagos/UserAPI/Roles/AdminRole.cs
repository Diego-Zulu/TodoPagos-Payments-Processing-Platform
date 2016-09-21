using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAPI
{
    public class AdminRole : Role
    {
        private static AdminRole instance;

        private AdminRole()
        {
            this.Name = "Admin";
            AddAdminPrivileges();
        }

        private void AddAdminPrivileges()
        {
            this.Privileges.Add(RegisterPaymentPrivilege.GetInstance());
        }

        public static AdminRole GetInstance()
        {
            if (instance == null)
            {
                instance = new AdminRole();
            }
            return instance;
        }
    }
}
