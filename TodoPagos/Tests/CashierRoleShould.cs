using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using UserAPI;

namespace Tests
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
    }
}
