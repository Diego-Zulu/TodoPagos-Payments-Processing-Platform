using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.UserAPI;
using System.Collections.Generic;
using System.Linq;

namespace TodoPagos.UserAPI.Tests
{
    [TestClass]
    public class AdminRoleShould
    {
        const int FIRST_POSITION = 0;

        [TestMethod]
        public void NotCreateMultipleInstancesOfSameRole()
        {
            AdminRole firstAdminRole = AdminRole.GetInstance();
            AdminRole secondAdminRole = AdminRole.GetInstance();

            Assert.AreSame(firstAdminRole, secondAdminRole);
        }

        [TestMethod]
        public void KnowIfItHasACertainPrivilege()
        {
            AdminRole adminRole = AdminRole.GetInstance();

            ICollection<Privilege> adminPrivileges = adminRole.Privileges;
            Privilege firstPrivilege = adminPrivileges.ElementAt(FIRST_POSITION);

            Assert.IsTrue(adminRole.HasPrivilege(firstPrivilege));
        }

        [TestMethod]
        public void BeAbleToTellItDoesNotHaveANullPrivilege()
        {
            AdminRole adminRole = AdminRole.GetInstance();

            Assert.IsFalse(adminRole.HasPrivilege(null));
        }

        [TestMethod]
        public void HaveItsNamesHashCode()
        {
            AdminRole adminRole = AdminRole.GetInstance();
            int adminRoleHashCode = adminRole.GetHashCode();

            Assert.AreEqual(adminRoleHashCode, adminRole.Name.GetHashCode());
        }

        [TestMethod]
        public void ReturnFalseWhenComparedWithANonRoleObject()
        {
            AdminRole adminRole = AdminRole.GetInstance();
            string nonRoleObject = "Hello World!";

            Assert.AreNotEqual(adminRole, nonRoleObject);
        }

        [TestMethod]
        public void HaveAtLeastOnePrivilege()
        {
            AdminRole adminRole = AdminRole.GetInstance();

            int numberOfPrivileges = adminRole.GetPrivilegeCount();

            Assert.IsTrue(numberOfPrivileges > 0);
        }
    }
}
