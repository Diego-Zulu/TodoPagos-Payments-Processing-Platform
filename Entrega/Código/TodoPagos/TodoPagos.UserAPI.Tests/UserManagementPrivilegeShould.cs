using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.UserAPI;

namespace TodoPagos.UserAPI.Tests
{
    [TestClass]
    public class UserManagementPrivilegeShould
    {
        [TestMethod]
        public void HaveItsNamesHashCode()
        {
            Privilege userManagementPrivilege = UserManagementPrivilege.GetInstance();
            int registerPaymentPrivilegeHashCode = userManagementPrivilege.GetHashCode();

            Assert.AreEqual(registerPaymentPrivilegeHashCode, userManagementPrivilege.Name.GetHashCode());
        }

        [TestMethod]
        public void ReturnFalseWhenComparedWithANonPrivilegeObject()
        {
            Privilege userManagementPrivilege = UserManagementPrivilege.GetInstance();
            string nonRoleObject = "Goodbye World!";

            Assert.AreNotEqual(userManagementPrivilege, nonRoleObject);
        }
    }
}
