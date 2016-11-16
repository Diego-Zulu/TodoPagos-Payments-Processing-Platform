using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TodoPagos.UserAPI.Tests
{
    [TestClass]
    public class ClientManagementPrivilegeShould
    {
        [TestMethod]
        public void HaveItsNamesHashCode()
        {
            Privilege clientManagement = ClientManagementPrivilege.GetInstance();
            int privilegeHashCode = clientManagement.GetHashCode();

            Assert.AreEqual(privilegeHashCode, clientManagement.Name.GetHashCode());
        }

        [TestMethod]
        public void ReturnFalseWhenComparedWithANonPrivilegeObject()
        {
            Privilege clientManagement = ClientManagementPrivilege.GetInstance();
            string nonRoleObject = "Goodbye World!";

            Assert.AreNotEqual(clientManagement, nonRoleObject);
        }
    }
}
