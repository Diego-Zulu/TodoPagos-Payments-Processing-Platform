using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAPI
{
    public class CashierRole : Role
    {
        private static CashierRole instance;

        private CashierRole()
        {
            this.Name = "Cashier";
            AddCashierPrivileges();
        }

        private void AddCashierPrivileges()
        {
            this.Privileges.Add(RegisterPaymentPrivilege.GetInstance());
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
