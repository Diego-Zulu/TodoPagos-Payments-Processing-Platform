using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserAPI.Privileges;

namespace TodoPagos.UserAPI.Tests
{
    [TestClass]
    public class ProviderManagementPrivilegeShould
    {
        [TestMethod]
        public void HaveItsNamesHashCode()
        {
            Privilege providerManagement = ProviderManagementPrivilege.GetInstance();
            int privilegeHashCode = providerManagement.GetHashCode();

            Assert.AreEqual(privilegeHashCode, providerManagement.Name.GetHashCode());
        }
    }
}
