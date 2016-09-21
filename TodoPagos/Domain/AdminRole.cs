using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AdminRole : Role
    {
        private static AdminRole instance;

        private AdminRole()
        {
            this.Name = "Admin";
        }

        public static Role GetInstance()
        {
            if (instance == null)
            {
                instance = new AdminRole();
            }
            return instance;
        }
    }
}
