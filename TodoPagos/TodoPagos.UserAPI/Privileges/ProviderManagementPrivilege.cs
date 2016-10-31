using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.UserAPI
{
    public class ProviderManagementPrivilege : Privilege
    {
        private static ProviderManagementPrivilege instance;

        protected ProviderManagementPrivilege()
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
