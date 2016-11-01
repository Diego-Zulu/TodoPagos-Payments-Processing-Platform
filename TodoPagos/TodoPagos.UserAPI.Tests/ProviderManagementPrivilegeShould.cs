using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        [TestMethod]
        public void ReturnFalseWhenComparedWithANonPrivilegeObject()
        {
            Privilege providerManagement = ProviderManagementPrivilege.GetInstance();
            string nonRoleObject = "Goodbye World!";

            Assert.AreNotEqual(providerManagement, nonRoleObject);
        }
    }
}
