using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.UserAPI;

namespace UserAPI.Privileges
{
    public class ProviderManagementPrivilege : Privilege
    {
        private static ProviderManagementPrivilege instance;

        private ProviderManagementPrivilege()
        {
            this.Name = "Register Provider";
        }

        public static ProviderManagementPrivilege GetInstance()
        {
            if (instance == null)
            {
                instance = new ProviderManagementPrivilege();
            }
            return instance;
        }
    }
}
