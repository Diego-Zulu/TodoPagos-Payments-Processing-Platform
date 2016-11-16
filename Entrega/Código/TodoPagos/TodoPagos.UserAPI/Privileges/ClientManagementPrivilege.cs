using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.UserAPI;

namespace TodoPagos.UserAPI
{
    public class ClientManagementPrivilege : Privilege
    {
        private static ClientManagementPrivilege instance;

        protected ClientManagementPrivilege()
        {
            this.Name = "Client Manager";
        }

        public static ClientManagementPrivilege GetInstance()
        {
            if (instance == null)
            {
                instance = new ClientManagementPrivilege();
            }
            return instance;
        }
    }
}
