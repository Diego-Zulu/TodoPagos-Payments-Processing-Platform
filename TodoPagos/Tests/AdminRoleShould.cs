using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserAPI;
using System.Collections.Generic;
using System.Linq;

namespace Tests
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
    }
}
