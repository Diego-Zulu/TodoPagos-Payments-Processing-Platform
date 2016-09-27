using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TodoPagos.UserAPI;

namespace TodoPagos.UserAPI.Tests
{
    [TestClass]
    public class CashierRoleShould
    {
        const int FIRST_POSITION = 0;

        [TestMethod]
        public void NotCreateMultipleInstancesOfSameRole()
        {
            CashierRole firstCashierRole = CashierRole.GetInstance();
            CashierRole secondCashierRole = CashierRole.GetInstance();

            Assert.AreSame(firstCashierRole, secondCashierRole);
        }

        [TestMethod]
        public void KnowIfItHasACertainPrivilege()
        {
            CashierRole cashierRole = CashierRole.GetInstance();

            ICollection<Privilege> cashierPrivileges = cashierRole.Privileges;
            Privilege firstPrivilege = cashierPrivileges.ElementAt(FIRST_POSITION);

            Assert.IsTrue(cashierRole.HasPrivilege(firstPrivilege));
        }

        [TestMethod]
        public void HaveItsNamesHashCode()
        {
            CashierRole cashierRole = CashierRole.GetInstance();
            int cashierRoleHashCode = cashierRole.GetHashCode();

            Assert.AreEqual(cashierRoleHashCode, cashierRole.Name.GetHashCode());
        }

        [TestMethod]
        public void ReturnFalseWhenComparedWithANonRoleObject()
        {
            CashierRole cashierRole = CashierRole.GetInstance();
            string nonRoleObject = "Hello World!";

            Assert.AreNotEqual(cashierRole, nonRoleObject);
        }

        [TestMethod]
        public void HaveAtLeastOnePrivilege()
        {
            CashierRole cashierRole = CashierRole.GetInstance();

            int numberOfPrivileges = cashierRole.GetPrivilegeCount();

            Assert.IsTrue(numberOfPrivileges > 0);
        }
    }
}
