using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.UserAPI
{
    public class EarningQueriesPrivilege : Privilege
    {
        private static EarningQueriesPrivilege instance;

        protected EarningQueriesPrivilege()
        {
            this.Name = "Earning Queries";
        }

        public static EarningQueriesPrivilege GetInstance()
        {
            if (instance == null)
            {
                instance = new EarningQueriesPrivilege();
            }
            return instance;
        }
    }
}
