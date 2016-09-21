using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RegisterPaymentPrivilege : Privilege
    {
        private static RegisterPaymentPrivilege instance;

        private RegisterPaymentPrivilege()
        {
            this.Name = "Register Payment";
        }

        public static RegisterPaymentPrivilege GetInstance()
        {
            if (instance == null)
            {
                instance = new RegisterPaymentPrivilege();
            }
            return instance;
        }
    }
}
