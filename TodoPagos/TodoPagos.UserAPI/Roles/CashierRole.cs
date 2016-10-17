using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.UserAPI
{
    public class CashierRole : Role
    {
        private static CashierRole instance;

        protected CashierRole()
        {
            this.Name = "Cashier";
            AddCashierPrivileges();
        }

        private void AddCashierPrivileges()
        {

        }

        public static CashierRole GetInstance()
        {
            if (instance == null)
            {
                instance = new CashierRole();
            }
            return instance;
        }
    }
}
