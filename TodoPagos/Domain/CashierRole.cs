using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CashierRole : Role
    {
        private static CashierRole instance;

        private CashierRole()
        {
            this.Name = "Cashier";
        }

        public static Role GetInstance()
        {
            if (instance == null)
            {
                instance = new CashierRole();
            }
            return instance;
        }
    }
}
