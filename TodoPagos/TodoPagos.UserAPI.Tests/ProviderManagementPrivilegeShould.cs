using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TodoPagos.UserAPI.Tests
{
    [TestClass]
    public class ProviderManagementPrivilegeShould
    {
        [TestMethod]
        public void HaveItsNamesHashCode()
        {
            Privilege providerManagement = ProviderManagement.GetInstance();
            int privilegeHashCode = providerManagement.GetHashCode();

            Assert.AreEqual(privilegeHashCode, providerManagement.Name.GetHashCode());
        }
    }
}
