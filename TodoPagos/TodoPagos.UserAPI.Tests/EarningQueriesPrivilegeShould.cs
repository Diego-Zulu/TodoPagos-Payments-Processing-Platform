using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.UserAPI.Tests
{
    [TestClass]
    public class EarningQueriesPrivilegeShould
    {
        [TestMethod]
        public void HaveItsNamesHashCode()
        {
            Privilege privilege = EarningQueriesPrivilege.GetInstance();
            int privilegeHashCode = privilege.GetHashCode();

            Assert.AreEqual(privilegeHashCode, privilege.Name.GetHashCode());
        }

        [TestMethod]
        public void ReturnFalseWhenComparedWithANonPrivilegeObject()
        {
            Privilege privilege = EarningQueriesPrivilege.GetInstance();
            string nonRoleObject = "Goodbye World!";

            Assert.AreNotEqual(privilege, nonRoleObject);
        }
    }
}
